
namespace GtkGL {
    using System;
    
    using gl=Tao.OpenGl.Gl;    
    
    public class Square : GtkGL.GLObjectBase, GtkGL.IGLObject {
        
        public Square() {
        }
        
        private void DrawSquare ()
        {
        
        	gl.glBegin(gl.GL_QUADS);						// Draw A Quad
				gl.glVertex3f(-1.0f, 1.0f, 0.0f);			// Top Left
				gl.glVertex3f( 1.0f, 1.0f, 0.0f);			// Top Right
				gl.glVertex3f( 1.0f,-1.0f, 0.0f);			// Bottom Right
				gl.glVertex3f(-1.0f,-1.0f, 0.0f);			// Bottom Left
			gl.glEnd();										// Done Drawing The Quad
		}
			
		// our Updated event handler
		public new event EventHandler Updated;
		
        public new void Translate(float x, float y, float z)
        {
        	base.Translate(x, y, z);
        	
			// Tell our handlers that we have been updated
			if (Updated != null)
        		Updated (this, null);
        }
        
        public new void Translate(double x, double y, double z)
        {
        	base.Translate(x, y, z);
        	
			// Tell our handlers that we have been updated
			if (Updated != null)
        		Updated (this, null);
        }
	        
	    public new void Rotate(GtkGL.Quaternion quat)
		{
			base.Rotate(quat);
				
			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);
		}
		
		public new void Rotate(GtkGL.TransformationMatrix transMatrix)
		{
			base.Rotate(transMatrix);
				
			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);
		}
		
		public new void Rotate(GtkGL.EulerRotation eRot)
		{
			base.Rotate(eRot);
				
			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);
		}
		
		
		public new void ResetRotation(bool doUpdate)
		{
			base.ResetRotation();

  			// Tell our handlers that we have been updated
  			if (doUpdate && Updated != null)
	  			Updated (this, null);
		}
		
		public new void ResetRotation()
		{
			base.ResetRotation();

  			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);
		}
		
		public bool Draw()
		{
			gl.glPushMatrix();
		
			if(transMatrix != null){
				// Apply the transformation matrix
				gl.glMultMatrixd(transMatrix.Matrix);
			}
			
			// Draw the image from the display list
	  		gl.glCallList (shapeID);
	  		
	  		gl.glPopMatrix();
	  		 
	  		return true;
		}

		// Cache the drawing of the object
		public new void Init()
		{
			// Do some genlist magic
			shapeID = gl.glGenLists (1);

			gl.glNewList (shapeID, gl.GL_COMPILE);
				DrawSquare();
			gl.glEndList ();
		}
    }
    
}
