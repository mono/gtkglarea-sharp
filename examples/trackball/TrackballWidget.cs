using System;

using Tao.OpenGl;
using gl=Tao.OpenGl.Gl;
using GtkGL;

namespace GtkGL {

	public class TrackballWidget: GtkGL.GLWidget {
	      
	    //Trackball.Trackball tb = new Trackball.Trackball();
		double beginX = 0;
	  	double beginY = 0;
	  	bool button1Pressed = false;
		GtkGL.Quaternion quat = null;
		
		Trackball tb = new Trackball();
		
	    public TrackballWidget() : base()
	    {
			base.GLSetup += GtkGL.GLWidget.EnableLighting;
	    
	    	// Create and initialize the quaternion
			quat = new GtkGL.Quaternion(0.0, 0.0, 0.0, 1.0);

			connectHandlers();
	    }
	    
	    void connectHandlers()
	    {
	    	this.Events |=
	    		Gdk.EventMask.Button1MotionMask |
		    	Gdk.EventMask.Button2MotionMask |
	    		Gdk.EventMask.ButtonPressMask |
	    		Gdk.EventMask.ButtonReleaseMask |
	    		Gdk.EventMask.VisibilityNotifyMask |
	    		Gdk.EventMask.PointerMotionMask |
	    		Gdk.EventMask.PointerMotionHintMask ;
	    
	    	this.ButtonPressEvent += OnButtonPress;
			this.ButtonReleaseEvent += OnButtonRelease;
			this.MotionNotifyEvent += OnMotionNotify;
			
			// Use this OnExposed instead of GlWidget's
			this.ExposeEvent -= base.OnExposed;
			this.ExposeEvent += OnExposed;
	    }
	        
	   	// Track mouse dragging
		void OnMotionNotify (object o, Gtk.MotionNotifyEventArgs e)
		{
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
				// Create a quaternion based on the mouse movement
				float[] spinQuat = new float[4];
				tb.trackball(ref spinQuat,
					(float) ((2.0 * beginX - this.Allocation.Width)        / this.Allocation.Width),
					(float) ((this.Allocation.Height - 2.0 * beginY)       / this.Allocation.Height),
					(float) ((2.0 * x - this.Allocation.Width)             / this.Allocation.Width),
					(float) ((this.Allocation.Height - 2.0 * y)            / this.Allocation.Height));
					
				// Add created quaternion to the current quat to get the new spin
				// Quaternion newQuat = new Quaternion(spinQuat);
				
				
				Quaternion newQuat = new Quaternion((double) spinQuat[3],
												   	(double) spinQuat[0],
												   	(double) spinQuat[1],
												   	(double) spinQuat[2]
												  	);
  	
				// Apply rotation to all objects								  	
				System.Collections.IEnumerator enumerator = GLObjectList.GetEnumerator();
				
  				while(enumerator.MoveNext()){
  					( (GtkGL.IGLObject) enumerator.Current ).Rotate(newQuat);
				}
				
				// Reset the "old" X and Y positions
				beginX = x;
				beginY = y;
			}
			
			
		}
		
		void OnButtonPress (object o, Gtk.ButtonPressEventArgs e)
		{
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
			if(e.Event.Button == 1){
				button1Pressed = false;
			}
	    }
	    
	}
}