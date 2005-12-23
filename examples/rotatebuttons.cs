// created on 12/13/2005 at 1:51 PM
using System;

using Gtk;
using GtkSharp;

using GtkGL;

using Tao.OpenGl;

using gl=Tao.OpenGl.Gl;
using glu=Tao.OpenGl.Glu;

// This code is based on work from Alp Toker and the NeHe lesson here:
// http://nehe.gamedev.net/data/lessons/lesson.asp?lesson=01
namespace GtkGl
{
	public class RotationTest
	{
		/* GlArea is the widget defined in the GtkGL namespace and is a
		 * specialized GtkDrawingArea with GL cow powers.
		 */
		
		GLArea glarea;
		
		/*	attrList
	     *	    Specifies a list of Boolean attributes and enum/integer
		 *      attribute/value pairs. The last attribute must be zero or
		 *      _GDK_GL_CONFIGS.None.
		 *      
		 *      See glXChooseVisual man page for explanation of
		 *      attrList.
		 *      
		 *      http://www.xfree86.org/4.4.0/glXChooseVisual.3.html
		 */
		   
		int[] attrlist = {
	    	(int)GtkGL._GDK_GL_CONFIGS.Rgba,
	    	(int)GtkGL._GDK_GL_CONFIGS.RedSize,1,
	    	(int)GtkGL._GDK_GL_CONFIGS.GreenSize,1,
	    	(int)GtkGL._GDK_GL_CONFIGS.BlueSize,1,
	    	(int)GtkGL._GDK_GL_CONFIGS.DepthSize,1,
	    	(int)GtkGL._GDK_GL_CONFIGS.Doublebuffer,
	    	(int)GtkGL._GDK_GL_CONFIGS.None,
	  	};	

		// This flag tells whether to rotate the scene or not
		bool doRotate = false;
		
		double rotAngle = 0.0;
		
		double xRot = 0.0;
		double yRot = 1.0;
		double zRot = 0.0;
		
		// Used to rotate either left or right
		int rotMult = 0;
		
		// This is the id of the opengl shape list:
		int shapeList;
		
		// Create a rotation of 2 degrees around the Y axis (left- and right-hand rotation)		
		// GtkGL.Rotation lRot;
		// GtkGL.Rotation rRot;
		
		// This method is called "ReSizeGLScene" in the original lesson
		void OnConfigure (object o, EventArgs e)
		{
			if( glarea.MakeCurrent() == 0)
				return;
			
			int height = glarea.Allocation.Height,
				width  = glarea.Allocation.Width;
				
			Console.WriteLine("Width: {0}", width);
			Console.WriteLine("Height: {0}", height);
			
			
			if (height==0)									// Prevent A Divide By Zero By
			{
				height=1;									// Making Height Equal One
			}
				
			gl.glViewport (0, 0, width, height);
			/*
			gl.glMatrixMode(gl.GL_PROJECTION);				// Select The Projection Matrix
			gl.glLoadIdentity();							// Reset The Projection Matrix

			// Calculate The Aspect Ratio Of The Window
			glu.gluPerspective(45.0f,(float)width/(float)height,0.1f,100.0f);

			gl.glMatrixMode(gl.GL_MODELVIEW);				// Select The Modelview Matrix
			gl.glLoadIdentity();
			*/
		}

		bool InitGL()
		{
			if (glarea.MakeCurrent() == 0)
				return false;

			gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);			// Black Background
			gl.glClearDepth(1.0f);								// Depth Buffer Setup

			Gl.glDepthFunc (Gl.GL_LEQUAL);						// The Type Of Depth Test To Do
			
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
			
			shapeList = gl.glGenLists (1);

			gl.glNewList (shapeList, gl.GL_COMPILE);
			Teapot.Teapot.DrawTeapot (true, 0.5f);
			gl.glEndList ();
			
			return true;
		}
		
		// The correct time to init the gl window is at Realize time
		void OnRealized (object o, EventArgs e)
		{
			Console.WriteLine("Realized...");
			if(!InitGL())
				Console.WriteLine("Couldn't InitGL()");
		}

		// This method is called "DrawGLScene" in the original lesson 		
		void OnExposed (object o, EventArgs e)
		{
			if (glarea.MakeCurrent() == 0)
				return;
			
			// Clear The Screen And The Depth Buffer
			gl.glClear(gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);

			gl.glLoadIdentity();							// Reset The Current Modelview Matrix
			
			// Rotate the scene as specified by the member vars
			gl.glRotatef((float)rotAngle, (float)xRot, (float)yRot, (float)zRot);
			
			// Draw the Teapot
			gl.glCallList (shapeList);
			
			glarea.SwapBuffers ();							// Show the newly displayed contents

		}

		public static int Main (string[] argc)
		{
			Gtk.Application.Init ();
			
			RotationTest r = new RotationTest();

			// Go, dog, go!
			Gtk.Application.Run ();

			return 1;
		}
		
		static void OnKeyPress (object o, Gtk.KeyPressEventArgs e)
		{
			if(e.Event.Key == Gdk.Key.Return ){
				Application.Quit();			
			}
		}
		
		private bool RotateScene()
		{
			rotAngle = (rotAngle % 360) + (1.5 * rotMult);
		
			if( glarea.MakeCurrent() == 0)
				return true;
			
			glarea.QueueDraw();
			
			return doRotate;
		}

		public RotationTest()
		{
			// Create a new GLArea widget and request a size
			glarea = new GLArea (attrlist);
			glarea.SetSizeRequest (300, 300);
			
			// Set some event handlers
			glarea.ExposeEvent += OnExposed;
			glarea.Realized += OnRealized;
			glarea.Unrealized += OnUnrealized;
			glarea.ConfigureEvent += OnConfigure;
			
			// This button quits the program
			Button btn = new Button ("Exit");		
		
			// Bind the key press event to the OnKeyPress method
			btn.KeyPressEvent += OnKeyPress;
			btn.Clicked += OnUnrealized;
			
			// Create a new Vertical Box that the GLArea can live in
			VBox vb = new VBox (false, 0);
			
			// Pack the GLArea widget into the VBox
			vb.PackStart (glarea, true, true, 0);
			
			// Create a horizontal box that holds <, exit and > buttons
			HBox hb = new HBox (false, 0);
		
			// This button will rotate the scene left
			Button btnRotL = new Button(" < ");
			
			// Attach some event handlers to the left rotation button
			btnRotL.Pressed += OnRotLPress;
			btnRotL.Released += OnRotLRelease;
						
			// Pack the left rotation button into the horizontal box
			hb.PackStart (btnRotL, false, false, 0);
		
			// This is the exit button
			hb.PackStart (btn, true, true, 0);
		
			// This button will rotate the scene right
			Button btnRotR = new Button(" > ");
			
			// Attach some event handlers to the right rotation button
			btnRotR.Pressed += OnRotRPress;
			btnRotR.Released += OnRotRRelease;

			// Pack the right rotation button into the horizontal box
			hb.PackStart (btnRotR, false, false, 0);
				
			// put the hbox in the vbox
			vb.PackStart (hb);
			
			// Create rotation objects, right and left hand rotations on the y axis
			// lRot = new Rotation(2.0, 0.0, 1.0, 0.0);
			// rRot = new Rotation(-2.0, 0.0, 1.0, 0.0);
			
			// Create a new window and name it appropriately
			Window win = new Window ("Rotation using Buttons!");
			
			// Pack the VBox into the window
			win.Add (vb);

			// Show all of win's contained widgets
			win.ShowAll ();
		}

		void OnRotLPress (object o, System.EventArgs e)
		{
			Console.WriteLine("Rotating Left!");

			rotMult = 1;
			
			doRotate = true;
			
			GLib.Timeout.Add (50, new GLib.TimeoutHandler (this.RotateScene));
		}

		void OnRotLRelease (object o, EventArgs e)
		{
			Console.WriteLine("Halting Left Rotation!");
			doRotate = false;
		}

		void OnRotRPress (object o, System.EventArgs e)
		{
			Console.WriteLine("Rotating Right!");	
			
			rotMult = -1;
			doRotate = true;

			GLib.Timeout.Add (50, new GLib.TimeoutHandler (this.RotateScene));
		}

		void OnRotRRelease (object o, EventArgs e)
		{
			Console.WriteLine("Halting Right Rotation!");
			doRotate = false;
		}
		

		void OnUnrealized (object o, EventArgs e)
		{
			Application.Quit();
		}
		

	}
}
