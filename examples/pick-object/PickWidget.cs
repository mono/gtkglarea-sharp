using System;

using Tao.OpenGl;
using gl=Tao.OpenGl.Gl;
using glu=Tao.OpenGl.Glu;
using GtkGL;

using Gtk;

namespace GtkGL {

	public enum InputMode {
		TrackballMode,
		PickMode,
		TranslateMode,
		ScaleMode
	}

	public class PickWidget: GtkGL.GLWidget {
	      
	    //Trackball.Trackball tb = new Trackball.Trackball();
		double beginX = 0;
	  	double beginY = 0;
	  	bool button1Pressed = false;
		GtkGL.Quaternion quat = GtkGL.Quaternion.Identity;
		
		InputMode currentInputMode = InputMode.PickMode;
		//InputMode currentInputMode = InputMode.TrackballMode;
		public InputMode CurrentInputMode {
			get { return currentInputMode; }
			set { currentInputMode = value; }
		}
		
		
	    public PickWidget() : base()
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
		
		struct hitStruct {
			public uint numNames;
			public double minZ;
			public double maxZ;
			public uint[] nameStack;
		}

		void processHits (int hits, uint[] buffer)
		{
			uint i, j=0;
			uint names, minZ, maxZ, numberOfNames;
			
			for (i = 0; i < hits; i++) {
				hitStruct hit = new hitStruct();
				
				// Get the number of names hit
				hit.numNames = buffer[j++];

				// I don't know if this is correct...
				// Findour min and max Z values
				hit.minZ = (double) (buffer[j++] / 0x7fffffff);
				hit.maxZ = (double) (buffer[j++] / 0x7fffffff);
				
				// Console.WriteLine("Hit occured between [{0} .. {1}]", hit.minZ, hit.maxZ);
				
				// Allocate some space for our hit names
				hit.nameStack = new uint[hit.numNames];
				
				for(int k = 0; k < hit.numNames; k++){
					hit.nameStack[k] = buffer[j++];
					
					// Select the objects that were hit
					System.Collections.IEnumerator enumerator = GLObjectList.GetEnumerator();
	  	
		  			while(enumerator.MoveNext()){
		  				if(( (GtkGL.IGLObject) enumerator.Current ).ID == hit.nameStack[k]){
		  					( (GtkGL.IGLObject) enumerator.Current ).Selected = true;
		  					Console.WriteLine("Object {0} selected.", hit.nameStack[k]);
		  				}
		  			}
				}
			}
		}
		
		void OnButtonRelease (object o, Gtk.ButtonReleaseEventArgs e)
		{
			if(e.Event.Button == 1){
				button1Pressed = false;
			}
			
			if(this.CurrentInputMode == GtkGL.InputMode.PickMode){
				// This document is based on the following example:
				// http://www.lighthouse3d.com/opengl/picking/
				// Console.WriteLine("Entering Pick mode...");

				// Console.WriteLine("Deselecting all objects");				
				System.Collections.IEnumerator enumerator = GLObjectList.GetEnumerator();
  	
	  			while(enumerator.MoveNext()){
					( (GtkGL.IGLObject) enumerator.Current ).Selected = false;
	  			}
		  			
				// Get Mouse coordinates
	    		double x = e.Event.X, y = e.Event.Y;
		
				if( this.MakeCurrent() == 0)
					return;
					
				double xCenter, yCenter,
					   width, height;
					   
				if(x > beginX){
					width = x - beginX;
					xCenter = beginX + ( width / 2 );
				}else{
					width = beginX - x;
					xCenter = x + ( width / 2 );
				}
				
				if(y > beginY){
					height = y - beginY;
					yCenter = beginY + ( height / 2 );
				}else{
					height = beginY - y;
					yCenter = y + ( height / 2 );
				}
				
				// Establish a buffer for selection mode values
				uint[] selectBuf = new uint[64];
				// http://www.mevis.de/opengl/glSelectBuffer.html
				gl.glSelectBuffer (64, selectBuf);
				
				// http://www.mevis.de/opengl/glRenderMode.html
				gl.glRenderMode(gl.GL_SELECT); // Enter the SELECT render mode

				gl.glMatrixMode(gl.GL_PROJECTION);				// Select The Projection Matrix
				gl.glPushMatrix();                              // Save our state
				gl.glLoadIdentity();							// Reset The Projection Matrix

				// Get the viewport
				int[] viewport = new int[4];
				gl.glGetIntegerv(gl.GL_VIEWPORT, viewport);
					
				glu.gluPickMatrix(xCenter, viewport[3] - yCenter, width+1, height+1, viewport);
				
				int windowWidth, windowHeight;
				e.Event.Window.GetSize(out windowWidth, out windowHeight);
				
				glu.gluPerspective(45,windowWidth / windowHeight,0.1,100);
				gl.glTranslatef(0.0f,0.0f,-6.0f);				// Move away from the drawing area 6.0				
				
				gl.glMatrixMode(gl.GL_MODELVIEW);
				
				// http://www.mevis.de/opengl/glInitNames.html				
				gl.glInitNames();  // Initialize the names stack
				
				// Draw the objects
				enumerator = GLObjectList.GetEnumerator();
	  	
	  			while(enumerator.MoveNext()){
	  				( (GtkGL.IGLObject) enumerator.Current ).Draw();
	  			}
	  			
	  			gl.glMatrixMode(gl.GL_PROJECTION);
	  			gl.glPopMatrix();
	  			
	  			gl.glMatrixMode(gl.GL_MODELVIEW);
	  			gl.glFlush();
	  			
				// Return to normal rendering mode
				int hits = gl.glRenderMode(gl.GL_RENDER);
				
				processHits(hits, selectBuf);

				// Console.WriteLine("Exited picking mode");
			}
	    }
	    
		private void OnQuit (object o, System.EventArgs e){
			Application.Quit();
		}

		private void OnWindowDeleteEvent (object sender, Gtk.DeleteEventArgs a) 
		{
			Application.Quit ();
			a.RetVal = true;
		}	    
	    
	}
}