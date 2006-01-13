namespace GtkGL {
    using System;
    using System.Collections;
    using GtkGL;
    using gl=Tao.OpenGl.Gl;
    
    // public abstract class GLObjectBase { // This doesn't work because of a bug:
    // http://bugzilla.ximian.com/show_bug.cgi?id=76122 - GLObjectBase shouldn't need to implement IGLObject
	public abstract class GLObjectBase : IGLObject {    

        protected ArrayList GLAreaList = null;
        
        protected int shapeID;
        
        protected GtkGL.TransformationMatrix transMatrix = null;
        protected GtkGL.EulerRotation eRot = null;
        protected GtkGL.Quaternion quat = null;
        
		// our Updated event handler
		public event EventHandler Updated;
        
		protected virtual void DrawObject()	{}   
		
		// Cache the drawing of the object
		public new void Init()
		{
	  		// Do some genlist magic
			shapeID = gl.glGenLists (1);

			gl.glNewList (shapeID, gl.GL_COMPILE);
				DrawObject();
			gl.glEndList ();
		}
        
        // Make setting of euler, quat and matrix an atomic action
        protected GtkGL.EulerRotation ERot {
        	get { return eRot; }
        	set {
        		if(value == null)
        			eRot = GtkGL.EulerRotation.Identity;
        		else
        			eRot = value;
        	
        		eRot = value;
        		quat = eRot.ToQuaternion();
        		transMatrix = eRot.ToTransMatrix();
        	}
        }
        
        // Make setting of euler, quat and matrix an atomic action
        protected GtkGL.Quaternion Quat {
        	get { return quat; }
        	set {
        		if(value == null)
        			quat = GtkGL.Quaternion.Identity;
        		else
        			quat = value;

        		eRot = quat.ToEulerRotation();
        		transMatrix = quat.ToTransMatrix();
        	}
        }
        
        // Make setting of euler, quat and matrix an atomic action
        protected GtkGL.TransformationMatrix TransMatrix {
        	get { return transMatrix; }
        	set {
        		if(value == null)
        			transMatrix = GtkGL.TransformationMatrix.Identity;
				else
        			transMatrix = value;
        		
        		eRot = transMatrix.ToEulerRotation();
        		quat = transMatrix.ToQuaternion();
        	}
        }
        
        public void Translate(float x, float y, float z)
        {
        	this.Translate((double) x, (double) y, (double) z);
        }
        
        public void Translate(double x, double y, double z)
        {
        	GtkGL.TransformationMatrix tm = GtkGL.TransformationMatrix.Identity;
			        	
        	tm.Matrix[12] = x;
        	tm.Matrix[13] = y;
        	tm.Matrix[14] = z;

        	if(this.transMatrix == null)
        		this.transMatrix = GtkGL.TransformationMatrix.Identity;

			this.transMatrix *= tm;
			
			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);			
        }
        
        public void Rotate(GtkGL.Quaternion q)
        {
        	Quat *= q;
        	
			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);        	
        }
        
        public void Rotate(GtkGL.EulerRotation er)
        {
			ERot += er;
			
			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);        				
        }
        
        
        public void Rotate(GtkGL.TransformationMatrix tm)
        {
        	TransMatrix *= tm;
			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);        
	  	}
        
		public void ResetRotation()
		{
			ERot = GtkGL.EulerRotation.Identity;

			// Tell our handlers that we have been updated
  			if (Updated != null)
	  			Updated (this, null);        				
		}
		
		public void ResetRotation(bool doUpdate)
		{
			ERot = GtkGL.EulerRotation.Identity;

			// Tell our handlers that we have been updated
  			if (doUpdate && Updated != null)
	  			Updated (this, null);        				
		}		
		
		public EulerRotation GetEulerRotation()
		{
			if(eRot != null)
				return eRot;

			ERot = GtkGL.EulerRotation.Identity;

			return ERot; 
		}
		
		public Quaternion GetQuaternion()
		{
			if(quat != null)
				return quat;
			
			Quat = GtkGL.Quaternion.Identity;
			
			return Quat;
		}
		
		public TransformationMatrix GetTransformationMatrix()
		{
			if(transMatrix != null)
				return transMatrix;
				
			TransMatrix = GtkGL.TransformationMatrix.Identity;
			
			return TransMatrix;
		}
		
		public virtual bool Draw()
		{
			gl.glPushMatrix();

			if(transMatrix != null){
				// Apply the transformation matrix
				gl.glMultMatrixd(transMatrix.Matrix);
			}
			
			// Draw the image from the display list
	  		//gl.glCallList (shapeID);
	  		DrawObject();
	  		
	  		gl.glPopMatrix();
	  		 
	  		return true;
		}		
        
    }
    
}
