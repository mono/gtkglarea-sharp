namespace GtkGL {
    using System;
    using GtkGL;
    
    public class ObjectRotationButton : Gtk.Button {
        
        public ObjectRotationButton(GtkGL.GLArea glArea, GtkGL.Rotation rot) : base() {
        	
        }

        public ObjectRotationButton(Gtk.Widget widget, GtkGL.GLArea glArea, GtkGL.Rotation rot) : base(widget) {
        	
        }
    }
    
}
