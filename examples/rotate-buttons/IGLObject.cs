namespace GtkGL {
    using System;
    using System.Collections;
    using GtkGL;
    
    public interface IGLObject {
    	// A hook for initialization tasks
        void Init();
        
        // Draw the object to the specified GLArea
        bool Draw();
  		
  		// Rotate the object angle degrees in the rot direction
  		void Rotate(float angle, GtkGL.Rotation rot);
  		
    }
    
}
