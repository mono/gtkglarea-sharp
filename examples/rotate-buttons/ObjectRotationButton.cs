namespace GtkGL {
    using System;
    using GtkGL;
    
    public class ObjectRotationButton : Gtk.Button {
        
        public GtkGL.Rotation rotation;
        private GtkGL.IGLObject glObject;
        private float rotAngle;
        
        GtkGL.EulerRotation eRot;
     
        private bool doRotate;
        
        public ObjectRotationButton(GtkGL.IGLObject glObject, GtkGL.EulerRotation rot) : base() {
        	Init(glObject, rot);
        }

        public ObjectRotationButton(Gtk.Widget widget, GtkGL.IGLObject glObject, GtkGL.EulerRotation rot) : base(widget) {
        	Init(glObject, rot);
        }
        
        bool pressedConnected = false;
        bool releasedConnected = false;
        
        private void Init(GtkGL.IGLObject glObject, GtkGL.EulerRotation rot)
        {
        	this.eRot = rot;
        	this.glObject = glObject;
        	       	
        	if(pressedConnected == false){
        		this.Pressed += OnPressed;
        		pressedConnected = true;
        	}
        		
        	if(releasedConnected == false){
				this.Released += OnReleased;
				releasedConnected = true;
			}
        }
        
        void OnPressed (object o, System.EventArgs e)
        {
        	doRotate = true;
        	GLib.Timeout.Add (50, new GLib.TimeoutHandler (this.RotateObject));
        }
        
        void OnReleased (object o, EventArgs e)
        {
        	doRotate = false;
        }
        
        // our Rotated event handler
		public event EventHandler Rotated;
        
        bool RotateObject ()
        {
			// Tell the glObject to rotate itself
        	glObject.Rotate(eRot);
        	
        	// Fire off our Rotated event
        	if(Rotated != null)
        		Rotated(this, null);
        	
        	return doRotate;
        }
    }
    
}
