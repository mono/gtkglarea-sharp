namespace GtkGL {
    using System;
    using System.Collections;
    using GtkGL;
    
    public interface IGLObject {
    	// An event handler to notify folks of updates
		event EventHandler Updated;    
    
    	// A hook for initialization tasks
        void Init();
        
        // Draw the object
        bool Draw();
        
        // Rotate the object 'angle' degrees in the 'rot' direction, and fire the 'Updated' handlers if 'doUpdate'
        void Rotate(float angle, GtkGL.Rotation rot, bool doUpdate);
  		  		
  		// Rotate the object angle degrees in the rot direction
  		void Rotate(float angle, GtkGL.Rotation rot);
  		
  		// Reset the rotation to the identity, and fire the 'Updated' handlers if 'doUpdate'
  		void ResetRotation(bool doUpdate);
  		
  		// Reset the rotation to the identity
  		void ResetRotation();
  			
  		// Return the Euler rotation
  		EulerRotation GetRotation();
    }
    
}
