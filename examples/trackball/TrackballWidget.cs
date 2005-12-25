using System;
    
using gl=Tao.OpenGl.Gl;

public class TrackballWidget: GlWidget {
      
    //Trackball.Trackball tb = new Trackball.Trackball();
	double beginX = 0;
  	double beginY = 0;
  	bool button1Pressed = false;
	float[] quat;
	
	Trackball tb = new Trackball();
	
    public TrackballWidget() : base()
    {
    	// Create and initialize the quaternion
		quat = new float[4];
		tb.trackball(ref quat, 0.0f, 0.0f, 0.0f, 0.0f);

		connectHandlers();
    }
    
    void connectHandlers()
    {
    	glArea.Events |=
    		Gdk.EventMask.Button1MotionMask |
	    	Gdk.EventMask.Button2MotionMask |
    		Gdk.EventMask.ButtonPressMask |
    		Gdk.EventMask.ButtonReleaseMask |
    		Gdk.EventMask.VisibilityNotifyMask |
    		Gdk.EventMask.PointerMotionMask |
    		Gdk.EventMask.PointerMotionHintMask ;
    
    	glArea.ButtonPressEvent += OnButtonPress;
		glArea.ButtonReleaseEvent += OnButtonRelease;
		glArea.MotionNotifyEvent += OnMotionNotify;
		
		// Use this OnExposed instead of GlWidget's
		glArea.ExposeEvent -= base.OnExposed;
		glArea.ExposeEvent += OnExposed;
    }
        
   	// Track mouse dragging
	void OnMotionNotify (object o, Gtk.MotionNotifyEventArgs e)
	{
		// We only want to spin the object if we're dragging on the glarea
		if(o != this.glArea)
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
				(float) ((2.0 * beginX - glArea.Allocation.Width)        / glArea.Allocation.Width),
				(float) ((glArea.Allocation.Height - 2.0 * beginY)       / glArea.Allocation.Height),
				(float) ((2.0 * x - glArea.Allocation.Width)             / glArea.Allocation.Width),
				(float) ((glArea.Allocation.Height - 2.0 * y)            / glArea.Allocation.Height));
				
			// Add created quaternion to the current quat to get the new spin
			tb.add_quats(spinQuat, quat, ref quat);
			
			// Re-draw object with new spin
			glArea.QueueDraw();
		}
		
		// Reset the "old" X and Y positions
		beginX = x;
		beginY = y;
	}
	
	void OnButtonPress (object o, Gtk.ButtonPressEventArgs e)
	{
		Console.WriteLine("Button Pressed!");
		if(e.Event.Button == 1){
			button1Pressed = true;
			
			/* potential beginning of drag, reset mouse position */
			beginX = e.Event.X;
			beginY = e.Event.Y;
			return;
		}
	}
	
	void OnButtonRelease (object o, Gtk.ButtonReleaseEventArgs e)
	{
		Console.WriteLine("Button Released!");
		if(e.Event.Button == 1){
			button1Pressed = false;
		}
    }
    
   	// Drawing of pretty objects happens here
	void OnExposed (object o, EventArgs e)
	{
		Console.WriteLine ("expose");
		
		if (glArea.MakeCurrent() == 0)
		  return;
		
		// Find the rotation matrix based on our quaternion
		float[] rotMatrix = new float[16];
		tb.build_rotmatrix(ref rotMatrix, quat);

		// Clear the scene
		gl.glClear (gl.GL_COLOR_BUFFER_BIT | gl.GL_DEPTH_BUFFER_BIT);
		
		// Replace current matrix with the identity matrix
		gl.glLoadIdentity ();
		gl.glClearColor (0.0f, 0.0f, 0.0f, 0.0f);
		
		// Translate a bit so the object is in view...
		gl.glTranslatef(0.0f,0.0f,-3.0f);

		// Rotate the matrix to the angle described by the quaternion
		gl.glMultMatrixf(rotMatrix);
				
		// Draw the contents of our shapeList
		gl.glCallList (shapeList);

		// bring back buffer to front, put front buffer in back
		glArea.SwapBuffers ();

	}
}
