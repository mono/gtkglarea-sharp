// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GtkGL {

	using System;
	using System.Collections;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public class GdkGLContext : GLib.Opaque {

		[DllImport("libgtkglarea-win32-2.0-0.dll")]
		static extern IntPtr gdk_gl_context_share_new(IntPtr visual, IntPtr sharelist, int direct);

		public static GtkGL.GdkGLContext ShareNew(Gdk.Visual visual, GtkGL.GdkGLContext sharelist, int direct) {
			IntPtr raw_ret = gdk_gl_context_share_new(visual == null ? IntPtr.Zero : visual.Handle, sharelist == null ? IntPtr.Zero : sharelist.Handle, direct);
			GtkGL.GdkGLContext ret = raw_ret == IntPtr.Zero ? null : (GtkGL.GdkGLContext) GLib.Opaque.GetOpaque (raw_ret, typeof (GtkGL.GdkGLContext), false);
			return ret;
		}

		[DllImport("libgtkglarea-win32-2.0-0.dll")]
		static extern IntPtr gdk_gl_context_attrlist_share_new(out int attrlist, IntPtr sharelist, int direct);

		public static GtkGL.GdkGLContext AttrlistShareNew(out int attrlist, GtkGL.GdkGLContext sharelist, int direct) {
			IntPtr raw_ret = gdk_gl_context_attrlist_share_new(out attrlist, sharelist == null ? IntPtr.Zero : sharelist.Handle, direct);
			GtkGL.GdkGLContext ret = raw_ret == IntPtr.Zero ? null : (GtkGL.GdkGLContext) GLib.Opaque.GetOpaque (raw_ret, typeof (GtkGL.GdkGLContext), false);
			return ret;
		}

		public GdkGLContext(IntPtr raw) : base(raw) {}

		[DllImport("libgtkglarea-win32-2.0-0.dll")]
		static extern IntPtr gdk_gl_context_new(IntPtr visual);

		public GdkGLContext (Gdk.Visual visual) 
		{
			Raw = gdk_gl_context_new(visual == null ? IntPtr.Zero : visual.Handle);
		}

		[DllImport("libgtkglarea-win32-2.0-0.dll")]
		static extern IntPtr gdk_gl_context_ref(IntPtr raw);

		protected override void Ref (IntPtr raw)
		{
			if (!Owned) {
				gdk_gl_context_ref (raw);
				Owned = true;
			}
		}

		[DllImport("libgtkglarea-win32-2.0-0.dll")]
		static extern void gdk_gl_context_unref(IntPtr raw);

		protected override void Unref (IntPtr raw)
		{
			if (Owned) {
				gdk_gl_context_unref (raw);
				Owned = false;
			}
		}

		class FinalizerInfo {
			IntPtr handle;

			public FinalizerInfo (IntPtr handle)
			{
				this.handle = handle;
			}

			public bool Handler ()
			{
				gdk_gl_context_unref (handle);
				return false;
			}
		}

		~GdkGLContext ()
		{
			if (!Owned)
				return;
			FinalizerInfo info = new FinalizerInfo (Handle);
			GLib.Timeout.Add (50, new GLib.TimeoutHandler (info.Handler));
		}

#endregion
	}
}
