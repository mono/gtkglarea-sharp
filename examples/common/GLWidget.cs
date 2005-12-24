using System;

using GtkGL;
using Tao.OpenGl;

using Gdk;

using gl=Tao.OpenGl.Gl;
using glu=Tao.OpenGl.Glu;

public class GlWidget {

	static int[] attrlist = {
	    (int)GtkGL._GDK_GL_CONFIGS.Rgba,
	    (int)GtkGL._GDK_GL_CONFIGS.RedSize,1,
	    (int)GtkGL._GDK_GL_CONFIGS.GreenSize,1,
	    (int)GtkGL._GDK_GL_CONFIGS.BlueSize,1,
	    (int)GtkGL._GDK_GL_CONFIGS.DepthSize,1,
	    (int)GtkGL._GDK_GL_CONFIGS.Doublebuffer,
	    (int)GtkGL._GDK_GL_CONFIGS.None,
	  };

	public GtkGL.GLArea glArea;

	public GlWidget() {
		glArea = new GtkGL.GLArea ( attrlist );
		
		glArea.SetSizeRequest(300,300);
		
		glArea.ExposeEvent += OnExposed;
		glArea.Realized += OnRealized;
		glArea.SizeAllocated += OnSizeAllocated;
		glArea.ConfigureEvent += OnConfigure;


	}
	
	// This handler gets fired when the glArea widget is re-sized
	void OnSizeAllocated (object o, Gtk.SizeAllocatedArgs e)
	{
		int height = e.Allocation.Height, width = e.Allocation.Width;
		
		if(height == 0){
			height = 1;
		}
		
		gl.glViewport(0, 0, width, height);
		
		gl.glMatrixMode(gl.GL_PROJECTION);				// Select The Projection Matrix
		gl.glLoadIdentity();							// Reset The Projection Matrix

		// Calculate The Aspect Ratio Of The Window
		glu.gluPerspective(45.0f,(float)width/(float)height,0.1f,100.0f);
		
		// 		glu.gluPerspective(45.0f,(float)width/(float)height,0.1f,100.0f);
		gl.glMatrixMode(gl.GL_MODELVIEW);						// Select The Modelview Matrix
		gl.glLoadIdentity();							// Reset The Modelview Matrix
		
	}
	
	// Drawing of pretty objects happens here
	void OnExposed (object o, EventArgs e)
	{
		Console.WriteLine ("expose");
		
		if (glArea.MakeCurrent() == 0)
		  return;
		
		// Clear the scene
		gl.glClear (gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);
		
		// Replace current matrix with the identity matrix
		gl.glLoadIdentity ();
		gl.glClearColor (0.0f, 0.0f, 0.0f, 0.0f);

		// Translate a bit...
		gl.glLoadIdentity();
		gl.glTranslatef(0.0f,0.0f,-3.0f);				// Move Into The Screen 7.0
		
		// Draw the contents of our shapeList
		gl.glCallList (shapeList);
				
		// bring back buffer to front, put front buffer in back
		glArea.SwapBuffers ();

	}

	int shapeList;

	// One-time configuration of opengl states happens here
	void OnRealized (object o, EventArgs e)
	{
		if (glArea.MakeCurrent() == 0)
		  return;

		gl.glClearColor (0.0f, 0.0f, 0.0f, 0.0f);
		gl.glClearDepth (1.0f);

		float[] materialSpecular = {1.0f, 1.0f, 1.0f, 0.15f};
		float[] materialShininess = {100.0f};
		float[] position = {-2.0f, 2.0f, 2.0f, 0.5f};

		Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, materialSpecular);
		Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, materialShininess);
		Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, position);

		Gl.glFrontFace (Gl.GL_CW);

		Gl.glEnable (Gl.GL_LIGHTING);
		Gl.glEnable (Gl.GL_LIGHT0);
		Gl.glEnable (Gl.GL_AUTO_NORMAL);
		Gl.glEnable (Gl.GL_NORMALIZE);
		Gl.glEnable (Gl.GL_DEPTH_TEST);
		Gl.glDepthFunc (Gl.GL_LEQUAL);
		gl.glShadeModel (gl.GL_SMOOTH);
		gl.glHint(gl.GL_PERSPECTIVE_CORRECTION_HINT, gl.GL_NICEST);			// Really Nice Perspective Calculations
		
		shapeList = gl.glGenLists (1);

		gl.glNewList (shapeList, gl.GL_COMPILE);
				Teapot.Teapot.DrawTeapot (true, 0.5f);
		gl.glEndList ();

	}

	// This handler gets fired when the glArea widget is re-sized
	// This method is called "ReSizeGLScene" in the original lesson
	void OnConfigure (object o, EventArgs e)
	{	
		if( glArea.MakeCurrent() == 0)
			return;
			
		gl.glViewport (0, 0, glArea.Allocation.Width, glArea.Allocation.Height);
		
	}
}