namespace GtkGL {
    using System;
    using System.Collections;
    using GtkGL;
    using gl=Tao.OpenGl.Gl;
    
    abstract class GLObjectBase {
    
        protected ArrayList GLAreaList = null;
        
        protected int shapeID;
        
        protected float[] rotMatrix = null;
        
        public void Rotate(float angle, GtkGL.Rotation rot)
        {
			// Clear the current matrix and prepare to generate a rotation
       		gl.glMatrixMode(gl.GL_MODELVIEW_MATRIX);
        	gl.glPushMatrix();
      		gl.glLoadIdentity();

        	// Are we rotating clockwise or counter-clockwise?
        	int direction = 0;
        	
        	if(rot.dir == GtkGL.Rotation.Direction.Clockwise){
        		direction = -1;
        	}else{
        		direction = 1;
        	}
        	
        	// Ensure that angle is not > 360.  That wouldn't make any sense :)
        	angle = (angle % 360);
        	
        	// Apply rotation to matrix
        	gl.glRotatef(direction * angle, rot.xRot, rot.yRot, rot.zRot);
        	
			// If there is already a rotation, we will apply this new one to it
        	if(rotMatrix != null){
        		gl.glMultMatrixf(rotMatrix);
        	}
        	
        	// Save the rotation matrix
        	rotMatrix = new float[16];
        	gl.glGetFloatv(gl.GL_MODELVIEW_MATRIX, rotMatrix);
        	
        	// Pop the head off of the matrix stack.
        	gl.glPopMatrix();

        }
        
		public void ResetRotation()
		{
			rotMatrix = null;
		}
		
		public EulerRotation GetRotation()
		{
			// If there is a rotation matrix, calculate the euler angles
			if(rotMatrix != null)
				return RotationUtil.RotMatrixToEuler(rotMatrix);
			else{
				// Otherwise, return a zeroed object
				EulerRotation rot = new EulerRotation();
				rot.x = rot.y = rot.z = 0.0f;
				
				return rot;
			}
		}
        
    }
    
}
