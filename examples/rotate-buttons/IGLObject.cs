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

  		// Rotate the object by passing a quaternion
  		void Rotate(GtkGL.Quaternion quat);
  		
  		// Rotate the object by passing an Euler rotation
  		void Rotate(GtkGL.EulerRotation eRot);
  		
  		// Reset the rotation to the identity, and fire the 'Updated' handlers if 'doUpdate'
  		void ResetRotation(bool doUpdate);
  		
  		// Reset the rotation to the identity
  		void ResetRotation();
  			
  		// Return the Euler rotation
  		EulerRotation GetRotation();
    }
    
}
