using System;
using Gtk;
using GtkGL;

using Tao.OpenGl;
using gl=Tao.OpenGl.Gl;
using Gl=Tao.OpenGl.Gl;

public class foo
{
  static GLArea glarea;
  static double beginX = 0;
  static double beginY = 0;
  static bool button1Pressed = false;
  static float[] quat = new float[4];
  static Trackball tb = new Trackball();
	
  static int[] attrlist = {
    (int)GtkGL._GDK_GL_CONFIGS.Rgba,
    (int)GtkGL._GDK_GL_CONFIGS.RedSize,1,
    (int)GtkGL._GDK_GL_CONFIGS.GreenSize,1,
    (int)GtkGL._GDK_GL_CONFIGS.BlueSize,1,
    (int)GtkGL._GDK_GL_CONFIGS.DepthSize,1,
    (int)GtkGL._GDK_GL_CONFIGS.Doublebuffer,
    (int)GtkGL._GDK_GL_CONFIGS.None,
  };

	public static void Main ()
	{
		Gtk.Application.Init ();

		// Initialize our quaternion.  Without this, our object doesn't rotate.
		tb.trackball(ref quat, 0.0f, 0.0f, 0.0f, 0.0f);

		glarea = new GLArea (attrlist);
		glarea.SetSizeRequest (300, 300);

		glarea.Events |= Gdk.EventMask.Button1MotionMask | Gdk.EventMask.Button2MotionMask | Gdk.EventMask.ButtonPressMask | Gdk.EventMask.ButtonReleaseMask | Gdk.EventMask.VisibilityNotifyMask;
		
		glarea.ExposeEvent += OnExposed;
		glarea.Realized += OnRealized;
		glarea.Unrealized += OnUnrealized;
		glarea.ConfigureEvent += OnConfigure;
		glarea.ButtonPressEvent += OnButtonPress;
		glarea.ButtonReleaseEvent += OnButtonRelease;
		glarea.MotionNotifyEvent += OnMotionNotify;

		Button btn = new Button ("Exit");
		
		btn.KeyPressEvent += OnKeyPress;
		btn.Clicked += OnUnrealized;
		
		VBox vb = new VBox (false, 0);
		vb.PackStart (glarea, true, true, 0);
		vb.PackStart (btn, false, false, 0);

		Window win = new Window ("GtkGL#");
		win.ReallocateRedraws = true;
		win.Add (vb);

		win.ShowAll ();

		Gtk.Application.Run ();
	}
	
	static void OnKeyPress (object o, Gtk.KeyPressEventArgs e)
	{
		if(e.Event.Key == Gdk.Key.Return ){
			Application.Quit();			
		}
	}
	
	static void OnMotionNotify (object o, Gtk.MotionNotifyEventArgs e)
	{
		// We only want to spin the object if we're dragging on the glarea
		if(o != glarea)
			return;
		
		int ix, iy;
		double x, y;
		Gdk.ModifierType m;
		
		// Find the current mouse X and Y positions
		if (e.Event.IsHint) {
			e.Event.Window.GetPointer(out ix, out iy, out m);
			x = (double)ix;
			y = (double)iy;
		} else {
    		x = e.Event.X;
    		y = e.Event.Y;
  		}

			
		if(button1Pressed == true){
			Console.WriteLine("Dragging...");
			
			// Create a quaternion based on the mouse movement
			float[] spinQuat = new float[4];
			tb.trackball(ref spinQuat,
				(float) ((glarea.Allocation.Width  - 2.0 * beginX)        / glarea.Allocation.Width),
				(float) ((2.0 * beginY - glarea.Allocation.Height)        / glarea.Allocation.Height),
				(float) ((glarea.Allocation.Width  - 2.0 * x)             / glarea.Allocation.Width),
				(float) ((2.0 * y - glarea.Allocation.Height)             / glarea.Allocation.Height));
				
			// Add created quaternion to the current quat to get the new spin
			tb.add_quats(spinQuat, quat, ref quat);
			
			// Re-draw object with new spin
			glarea.QueueDraw();
			
		}
		
		// Reset the "old" X and Y positions
		beginX = x;
		beginY = y;
	}
	
	
	static void OnButtonPress (object o, Gtk.ButtonPressEventArgs e)
	{
		if(e.Event.Button == 1){
			button1Pressed = true;
			
			/* potential beginning of drag, reset mouse position */
			beginX = e.Event.X;
			beginY = e.Event.Y;
			return;
		}
	}
	
	static void OnButtonRelease (object o, Gtk.ButtonReleaseEventArgs e)
	{
		if(e.Event.Button == 1){
			button1Pressed = false;
		}
	}

	static void OnExposed (object o, EventArgs e)
	{
		Console.WriteLine ("expose");
		
		// Find the rotation matrix based on our quaternion
		float[] rotMatrix = new float[16];
		tb.build_rotmatrix(ref rotMatrix, quat);

		if (glarea.MakeCurrent() == 0)
		  return;

		gl.glClear (gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);
		
		gl.glLoadIdentity ();
		gl.glClearColor (0.0f, 0.0f, 0.0f, 0.0f);
		
		// Rotate the matrix to the angle described by the quaternion
		gl.glMultMatrixf(rotMatrix);

		gl.glCallList (shapeList);
				
		glarea.SwapBuffers ();

		//gld.GlEnd ();
	}

	static int shapeList;

	//[GLib.ConnectBefore]
	static void OnRealized (object o, EventArgs e)
	{
		if (glarea.MakeCurrent() == 0)
		  return;

		gl.glClearColor (0.0f, 0.0f, 0.0f, 0.0f);
		gl.glClearDepth (1.0f);

		float[] materialSpecular = {1.0f, 1.0f, 1.0f, 0.15f};
		float[] materialShininess = {100.0f};
		float[] position = {1.5f, 1.5f, -3.5f, 0.5f};

		Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, materialSpecular);
		Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, materialShininess);
		Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, position);

		Gl.glFrontFace (Gl.GL_CW);

		Gl.glEnable (Gl.GL_LIGHTING);
		Gl.glEnable (Gl.GL_LIGHT0);
		Gl.glEnable (Gl.GL_AUTO_NORMAL);
		Gl.glEnable (Gl.GL_NORMALIZE);
		Gl.glEnable (Gl.GL_DEPTH_TEST);
		Gl.glDepthFunc (Gl.GL_LESS);

		shapeList = gl.glGenLists (1);

		gl.glNewList (shapeList, gl.GL_COMPILE);
			Teapot.Teapot.DrawTeapot (true, 0.5f);
		gl.glEndList ();

	}

	static void OnUnrealized (object o, EventArgs e)
	{
		Application.Quit();
	}

	static void OnConfigure (object o, EventArgs e)
	{
		if( glarea.MakeCurrent() == 0)
			return;
			
		gl.glViewport (0, 0, glarea.Allocation.Width, glarea.Allocation.Height);

	}



}
