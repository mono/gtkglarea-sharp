// created on 12/13/2005 at 1:51 PM
using System;
using System.Timers;

using Gtk;
using GtkSharp;

using GtkGL;

using Tao.OpenGl;

using gl=Tao.OpenGl.Gl;
using glu=Tao.OpenGl.Glu;

// This code is based on work from Alp Toker and the NeHe lesson here:
// http://nehe.gamedev.net/data/lessons/lesson.asp?lesson=01
namespace NeHe.Lesson04
{
	public class Lesson04
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
		
		float		rtri;						// Angle For The Triangle (NEW)
		float		rquad;						// Angle For The Quad (NEW)
		
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
			
			gl.glMatrixMode(gl.GL_PROJECTION);				// Select The Projection Matrix
			gl.glLoadIdentity();							// Reset The Projection Matrix

			// Calculate The Aspect Ratio Of The Window
			glu.gluPerspective(45.0f,(float)width/(float)height,0.1f,100.0f);

			gl.glMatrixMode(gl.GL_MODELVIEW);				// Select The Modelview Matrix
			gl.glLoadIdentity();		
		}

		bool InitGL()
		{
			if (glarea.MakeCurrent() == 0)
				return false;

			gl.glShadeModel(gl.GL_SMOOTH);						// Enables Smooth Shading
			gl.glClearColor(0.0f, 0.0f, 0.0f, 0.0f);			// Black Background
			gl.glClearDepth(1.0f);								// Depth Buffer Setup
			gl.glEnable(gl.GL_DEPTH_TEST);						// Enables Depth Testing
			gl.glDepthFunc(gl.GL_LEQUAL);						// The Type Of Depth Test To Do
			// Really Nice Perspective Calculations
			gl.glHint(gl.GL_PERSPECTIVE_CORRECTION_HINT, gl.GL_NICEST);	
			
			return true;
		}
		
		// The correct time to init the gl window is at Realize time
		void OnRealized (object o, EventArgs e)
		{
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
			
			gl.glTranslatef(-1.5f,0.0f,-6.0f);				// Move Left 1.5 Units And Into The Screen 6.0
			
			gl.glRotatef(rtri,0.0f,1.0f,0.0f);				// Rotate The Triangle On The Y axis (NEW)
			
			gl.glBegin(gl.GL_TRIANGLES);					// Drawing Using Triangles
				gl.glColor3f(1.0f,0.0f,0.0f);				// Set The Color To Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);			// Top
				gl.glColor3f(0.0f,1.0f,0.0f);				// Set The Color To Green
				gl.glVertex3f(-1.0f,-1.0f, 0.0f);			// Bottom Left
				gl.glColor3f(0.0f,0.0f,1.0f);				// Set The Color To Blue				
				gl.glVertex3f( 1.0f,-1.0f, 0.0f);			// Bottom Right
			gl.glEnd();										// Finished Drawing The Triangle
			
			gl.glLoadIdentity();								// If this didn't happen, weird rotation happens
			gl.glTranslatef(1.5f,0.0f,-6.0f);				// Move Right 1.5 Units And Into The Screen 6.0
			gl.glRotatef(rquad,1.0f,0.0f,0.0f);				// Rotate The Quad On The X axis ( NEW )
			
			gl.glColor3f(0.5f,0.5f,1.0f);					// Set The Color To Blue One Time Only
			gl.glBegin(gl.GL_QUADS);						// Draw A Quad
				gl.glVertex3f(-1.0f, 1.0f, 0.0f);			// Top Left
				gl.glVertex3f( 1.0f, 1.0f, 0.0f);			// Top Right
				gl.glVertex3f( 1.0f,-1.0f, 0.0f);			// Bottom Right
				gl.glVertex3f(-1.0f,-1.0f, 0.0f);			// Bottom Left
			gl.glEnd();										// Done Drawing The Quad
			
			glarea.SwapBuffers ();							// Show the newly displayed contents
		}

		public static int Main (string[] argc)
		{
			Gtk.Application.Init ();
			
			System.Timers.Timer t = new System.Timers.Timer();
			
			Lesson04 l = new Lesson04();
			
			GLib.Timeout.Add (50, new GLib.TimeoutHandler (l.RotateObjects));

			// Go, dog, go!
			Gtk.Application.Run ();

			return 1;
		}
		
		private bool RotateObjects()
		{
			rtri = (rtri % 360) + 1.0f;
			rquad = (rquad % 360) + 0.8f;
			
			if( glarea.MakeCurrent() == 0)
				return true;
			
			glarea.QueueDraw();
			
			return true;
		}

		public Lesson04()
		{
			// Create a new GLArea widget and request a size
			glarea = new GLArea (attrlist);
			glarea.SetSizeRequest (300, 300);
			
			// Set some event handlers
			glarea.ExposeEvent += OnExposed;
			glarea.Realized += OnRealized;
			glarea.Unrealized += OnUnrealized;
			glarea.ConfigureEvent += OnConfigure;
						
			// Create a new Vertical Box that the GLArea can live in
			VBox vb = new VBox (false, 0);
			
			// Pack the GLArea widget into the VBox
			vb.PackStart (glarea, true, true, 0);
			
			// Create a new window and name it appropriately
			Window win = new Window ("NeHe Lesson04");
			
			// Pack the VBox into the window
			win.Add (vb);

			// Show all of win's contained widgets
			win.ShowAll ();
		}
				

		
		void OnUnrealized (object o, EventArgs e)
		{
			Application.Quit();
		}
		

	}
}
