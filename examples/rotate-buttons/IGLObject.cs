namespace GtkGL {
    using System;
    using System.Collections;
    using GtkGL;
    
    public interface IGLObject {
    	// An event handler to notify folks of updates
		event EventHandler Updated;    
    
    	// A hook for initialization tasks
        void Init();
        
        // Draw the object to the specified GLArea
        bool Draw();
  		
  		// Rotate the object angle degrees in the rot direction
  		void Rotate(float angle, GtkGL.Rotation rot);
  		
  		// Reset the rotation to the identity
  		void ResetRotation();
  			
    }
    
}
