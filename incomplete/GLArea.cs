// Managed GLArea for Gtk#
// Alp Toker <alp@atoker.com>

using System;
using System.Runtime.InteropServices;
using Gtk;

namespace NDesk.Graphics
{
	[StructLayout(LayoutKind.Sequential)]
		class GdkVisual
		{
			IntPtr parent_instance;

			//public int type;
			public Gdk.VisualType type;
			public int depth;
			//public int byte_order;
			public Gdk.ByteOrder byte_order;
			public int colormap_size;
			public int bits_per_rgb;

			public uint red_mask;
			public int red_shift;
			public int red_prec;

			public uint green_mask;
			public int green_shift;
			public int green_prec;

			public uint blue_mask;
			public int blue_shift;
			public int blue_prec;
		}

	[StructLayout(LayoutKind.Sequential)]
		class XVisualInfo
		{
			public IntPtr visual;
			public IntPtr visualid;
			public int screen;
			public uint depth;
			public int @class;
			public uint red_mask; //ulong
			public uint green_mask; //ulong
			public uint blue_mask; //ulong
			public int colormap_size;
			public int bits_per_rgb;
		}

	public class GLArea : DrawingArea
	{
		[DllImport("gdk-x11-2.0")]
			internal static extern IntPtr gdk_x11_drawable_get_xdisplay (IntPtr raw);
		[DllImport("gdk-x11-2.0")]
			internal static extern IntPtr gdk_x11_drawable_get_xid (IntPtr raw);
		[DllImport ("gdk-x11-2.0")]
			static extern IntPtr gdk_x11_display_get_xdisplay (IntPtr raw);
		[DllImport ("gdk-x11-2.0")]
			static extern IntPtr gdk_x11_visual_get_xvisual (IntPtr raw);

		[DllImport("X11")]
			internal static extern IntPtr XVisualIDFromVisual(IntPtr visual);
		[DllImport("X11")]
			//vinfo_mask was long
			internal static extern XVisualInfo XGetVisualInfo(IntPtr display, int vinfo_mask, XVisualInfo vinfo_template, out int nitems_return);

		[DllImport("GL")]
			internal static extern IntPtr glXCreateContext(IntPtr dpy, XVisualInfo vis, IntPtr shareList, bool direct);
		[DllImport("GL")]
			internal static extern int glXMakeCurrent (IntPtr dpy, IntPtr drawable, IntPtr ctx);

		[Flags]
			enum VisualMask
			{	
				NoMask = 0x0,
				IDMask = 0x1,
				ScreenMask = 0x2,
				DepthMask = 0x4,
				ClassMask = 0x8,
				RedMaskMask = 0x10,
				GreenMaskMask = 0x20,
				BlueMaskMask = 0x40,
				ColormapSizeMask = 0x80,
				BitsPerRGBMask = 0x100,
				AllMask = 0x1FF,
			}

		Gdk.Visual ChooseVisual (int[] attrlist)
		{
			//FIXME: implement properly

			return Gdk.Visual.Best;
		}

		IntPtr dpy, ctx;

		static int[] default_attrlist = {
			4, 8, 1, 9, 1, 10, 1, 12, 1, 5, 0,
		};

		public bool AutoMakeCurrent = false;
		
		public bool AutoSwapBuffers = false;

		public GLArea () : this (default_attrlist) {}

		public GLArea (int[] attrlist)
		{
			DoubleBuffered = false;

			dpy = gdk_x11_display_get_xdisplay (Gdk.Display.Default.Handle);

			Gdk.Visual visual = ChooseVisual (attrlist);

			XVisualInfo vi_template = new XVisualInfo ();

			vi_template.visual = gdk_x11_visual_get_xvisual (visual.Handle);
			vi_template.visualid = XVisualIDFromVisual (vi_template.visual);
			
			GdkVisual gvisual = Marshal.PtrToStructure(visual.Handle, typeof(GdkVisual)) as GdkVisual;
			Console.WriteLine (gvisual.depth);
			//vi_template.depth = (uint)GdkWindow.Depth; //TODO: correct?
			vi_template.depth = 16; //TODO: don't hardcode
			vi_template.screen = Gdk.Display.Default.DefaultScreen.Number;

			int nret = 0;
			XVisualInfo vi = XGetVisualInfo (dpy, (int) (VisualMask.IDMask | VisualMask.DepthMask | VisualMask.ScreenMask), vi_template, out nret);

			if (vi == null || nret != 1)
				Console.WriteLine ("Error");

			ctx = glXCreateContext (dpy, vi, IntPtr.Zero, false);
		}

		//TODO: override expose
		/*
		{
			if(AutoMakeCurrent)
				MakeCurrent ();

			base.foo ();

			if(AutoSwapBuffers)
				SwapBuffers ();
		}
		*/

		public int MakeCurrent ()
		{
			//TODO: cache xid for performance
			return glXMakeCurrent (dpy, gdk_x11_drawable_get_xid (GdkWindow.Handle), ctx);
		}

		[DllImport("GL")]
			internal static extern void glXSwapBuffers (IntPtr dpy, IntPtr drawable);
		public void SwapBuffers ()
		{
			//TODO: cache xid for performance
			glXSwapBuffers(dpy, gdk_x11_drawable_get_xid (GdkWindow.Handle));
		}
	}
}
