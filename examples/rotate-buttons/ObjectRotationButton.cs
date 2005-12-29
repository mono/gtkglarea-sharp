namespace GtkGL {
    using System;
    using GtkGL;
    
    public class ObjectRotationButton : Gtk.Button {
        
        private GtkGL.Rotation rotation;
        private GtkGL.GLArea glArea;
        private float rotAngle;
        private float rotMult;
        
        private bool doRotate;
        
        public ObjectRotationButton(GtkGL.GLArea glArea, GtkGL.Rotation rot) : base() {
        	Init(glArea, rot);
        }

        public ObjectRotationButton(Gtk.Widget widget, GtkGL.GLArea glArea, GtkGL.Rotation rot) : base(widget) {
        	Init(glArea, rot);
        }
        
        private void Init(GtkGL.GLArea glArea, GtkGL.Rotation rot)
        {
        	this.rotation = rot;
        	this.glArea = glArea;
        	
        	this.rotAngle = 0.0f;
        	this.rotMult = 1.0f;
        	
        	this.Pressed += OnPressed;
			this.Released += OnReleased;
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
        
        bool RotateObject ()
        {
        	int direction = 0;
        	
        	if(this.rotation.dir == GtkGL.Rotation.Direction.Clockwise){
        		direction = -1;
        	}else{
        		direction = 1;
        	}
        	
        	rotAngle = (rotAngle % 360) + (rotMult * direction);
        	
        	return doRotate;
        }
    }
    
}
