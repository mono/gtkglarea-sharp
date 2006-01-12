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
namespace NeHe.Lesson06
{
	public class Lesson06
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
		
		float		rtri;						// Angle For The Triangle
		float		rquad;						// Angle For The Quad
		
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
			
			gl.glRotatef(rtri,0.0f,1.0f,0.0f);				// Rotate The Pyramid On The Y axis
			
			gl.glBegin(gl.GL_TRIANGLES);					// Start drawing the Pyramid
			
				gl.glColor3f(1.0f,0.0f,0.0f);				// Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);			// Top Of Triangle (Front)
				gl.glColor3f(0.0f,1.0f,0.0f);				// Green
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Left Of Triangle (Front)
				gl.glColor3f(0.0f,0.0f,1.0f);				// Blue
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Right Of Triangle (Front)
			
				gl.glColor3f(1.0f,0.0f,0.0f);				// Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);			// Top Of Triangle (Right)
				gl.glColor3f(0.0f,0.0f,1.0f);				// Blue
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Left Of Triangle (Right)
				gl.glColor3f(0.0f,1.0f,0.0f);				// Green
				gl.glVertex3f( 1.0f,-1.0f, -1.0f);			// Right Of Triangle (Right)
				
				gl.glColor3f(1.0f,0.0f,0.0f);				// Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);			// Top Of Triangle (Back)
				gl.glColor3f(0.0f,1.0f,0.0f);				// Green
				gl.glVertex3f( 1.0f,-1.0f, -1.0f);			// Left Of Triangle (Back)
				gl.glColor3f(0.0f,0.0f,1.0f);				// Blue
				gl.glVertex3f(-1.0f,-1.0f, -1.0f);			// Right Of Triangle (Back)
				
				gl.glColor3f(1.0f,0.0f,0.0f);				// Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);			// Top Of Triangle (Left)
				gl.glColor3f(0.0f,0.0f,1.0f);				// Blue
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);			// Left Of Triangle (Left)
				gl.glColor3f(0.0f,1.0f,0.0f);				// Green
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Right Of Triangle (Left)

			gl.glEnd();										// Finished Drawing The Pyramid
			
			gl.glLoadIdentity();							// If this didn't happen, weird rotation happens
			gl.glTranslatef(1.5f,0.0f,-7.0f);				// Move Right 1.5 Units And Into The Screen 6.0
			gl.glRotatef(rquad,1.0f,1.0f,1.0f);				// Rotate The Quad On The X axis ( NEW )
			
			gl.glBegin(gl.GL_QUADS);						// Draw a Cube
			
				gl.glColor3f(0.0f,1.0f,0.0f);			    // Set The Color To Green
				gl.glVertex3f( 1.0f, 1.0f,-1.0f);			// Top Right Of The Quad (Top)
				gl.glVertex3f(-1.0f, 1.0f,-1.0f);			// Top Left Of The Quad (Top)
				gl.glVertex3f(-1.0f, 1.0f, 1.0f);			// Bottom Left Of The Quad (Top)
				gl.glVertex3f( 1.0f, 1.0f, 1.0f);			// Bottom Right Of The Quad (Top)
				
				gl.glColor3f(1.0f,0.5f,0.0f);				// Set The Color To Orange
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Top Right Of The Quad (Bottom)
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Top Left Of The Quad (Bottom)
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);			// Bottom Left Of The Quad (Bottom)
				gl.glVertex3f( 1.0f,-1.0f,-1.0f);			// Bottom Right Of The Quad (Bottom)
				
				gl.glColor3f(1.0f,0.0f,0.0f);				// Set The Color To Red
				gl.glVertex3f( 1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Front)
				gl.glVertex3f(-1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Front)
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Bottom Left Of The Quad (Front)
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Bottom Right Of The Quad (Front)
				
				gl.glColor3f(1.0f,1.0f,0.0f);				// Set The Color To Yellow
				gl.glVertex3f( 1.0f,-1.0f,-1.0f);			// Bottom Left Of The Quad (Back)
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);			// Bottom Right Of The Quad (Back)
				gl.glVertex3f(-1.0f, 1.0f,-1.0f);			// Top Right Of The Quad (Back)
				gl.glVertex3f( 1.0f, 1.0f,-1.0f);			// Top Left Of The Quad (Back)
				
				gl.glColor3f(0.0f,0.0f,1.0f);				// Set The Color To Blue
				gl.glVertex3f(-1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Left)
				gl.glVertex3f(-1.0f, 1.0f,-1.0f);			// Top Left Of The Quad (Left)
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);			// Bottom Left Of The Quad (Left)
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);			// Bottom Right Of The Quad (Left)
				
				gl.glColor3f(1.0f,0.0f,1.0f);				// Set The Color To Violet
				gl.glVertex3f( 1.0f, 1.0f,-1.0f);			// Top Right Of The Quad (Right)
				gl.glVertex3f( 1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Right)
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);			// Bottom Left Of The Quad (Right)
				gl.glVertex3f( 1.0f,-1.0f,-1.0f);			// Bottom Right Of The Quad (Right)
			
			gl.glEnd();										// Done Drawing The Cube
			
			glarea.SwapBuffers ();							// Show the newly displayed contents
		}

		public static int Main (string[] argc)
		{
			Gtk.Application.Init ();
			
			System.Timers.Timer t = new System.Timers.Timer();
			
			Lesson06 l = new Lesson06();
			
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

		public Lesson06()
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
			Window win = new Window ("NeHe Lesson06");
			
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
