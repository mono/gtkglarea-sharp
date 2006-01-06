namespace GtkGL {
    using System;
    using System.Collections;
    using GtkGL;
    using gl=Tao.OpenGl.Gl;
    
    abstract class GLObjectBase {

        protected ArrayList GLAreaList = null;
        
        protected int shapeID;
        
        protected GtkGL.RotationMatrix rotMatrix = null;
        protected GtkGL.EulerRotation eRot = null;
        protected GtkGL.Quaternion quat = null;
        
        public void Rotate(GtkGL.Quaternion q)
        {
        	eRot += q.ToEulerRotation();
        	quat += q;
        	
        	Rotate(q.ToRotMatrix());
        }
        
        public void Rotate(GtkGL.EulerRotation er)
        {
			quat += er.ToQuaternion();
			eRot += er;
			
			Rotate(er.ToRotMatrix());
        }
        
        
        public void Rotate(GtkGL.RotationMatrix rm)
        {
        	// We're going to operate on the modelview matrix
			gl.glMatrixMode(gl.GL_MODELVIEW_MATRIX);
			
			// First, save the current state by pushing a new matrix onto the stack
        	gl.glPushMatrix();
        	
        	// Then clear the newly pushed matrix so we have a clean slate to work with
      		gl.glLoadIdentity();

			// Rotate by the matrix passed
			gl.glMultMatrixd(rm.Matrix);
        	
			// If there is already a rotation, we will apply it as well
        	if(rotMatrix != null)
        		gl.glMultMatrixd(rotMatrix.Matrix);
        	
        	// Create a temporary rotation matrix (4x4)
        	double[] newRM = new double[16];
        	
        	// Get the value of our rotated matrix
        	gl.glGetDoublev(gl.GL_MODELVIEW_MATRIX, newRM);
        	
        	// Assign that value to our object's rotMatrix object
        	rotMatrix.Matrix = newRM;
        	
        	// Pop the head off of the matrix stack, since we're done fiddling.
        	gl.glPopMatrix();

        }
        
		public void ResetRotation()
		{
			rotMatrix = GtkGL.RotationMatrix.Identity;
		}
		
		public EulerRotation GetRotation()
		{
			// If there is a rotation matrix, calculate the euler angles
			if(rotMatrix != null){
				GtkGL.EulerRotation er = null;

				try {
					er = rotMatrix.ToEulerRotation();
				}catch(GtkGL.EulerRotation.GimbalLock gl){
					Console.WriteLine("Caught Gimbal Lock!");
					er = rotMatrix.ToQuaternion().ToEulerRotation();
				}
				
				return er;
			}
			else
				return GtkGL.EulerRotation.Identity;
		}
        
    }
    
}
