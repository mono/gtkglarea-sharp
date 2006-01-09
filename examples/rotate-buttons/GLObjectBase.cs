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
        
        // Make setting of euler, quat and matrix an atomic action
        private GtkGL.EulerRotation ERot {
        	get { return eRot; }
        	set {
        		if(value == null)
        			eRot = GtkGL.EulerRotation.Identity;
        		else
        			eRot = value;
        	
        		eRot = value;
        		quat = eRot.ToQuaternion();
        		rotMatrix = eRot.ToRotMatrix();
        	}
        }
        
        // Make setting of euler, quat and matrix an atomic action
        private GtkGL.Quaternion Quat {
        	get { return quat; }
        	set {
        		if(value == null)
        			quat = GtkGL.Quaternion.Identity;
        		else
        			quat = value;

        		eRot = quat.ToEulerRotation();
        		rotMatrix = quat.ToRotMatrix();
        	}
        }
        
        // Make setting of euler, quat and matrix an atomic action
        private GtkGL.RotationMatrix RotMatrix {
        	get { return rotMatrix; }
        	set {
        		if(value == null)
        			rotMatrix = GtkGL.RotationMatrix.Identity;
				else
        			rotMatrix = value;
        		
        		eRot = rotMatrix.ToEulerRotation();
        		quat = rotMatrix.ToQuaternion();
        	}
        }
        
        public void Rotate(GtkGL.Quaternion q)
        {
        	Quat *= q;
        }
        
        public void Rotate(GtkGL.EulerRotation er)
        {
			ERot += er;	
        }
        
        
        public void Rotate(GtkGL.RotationMatrix rm)
        {
        	RotMatrix += rm;
        }
        
		public void ResetRotation()
		{
			ERot = GtkGL.EulerRotation.Identity;
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
		
		public RotationMatrix GetRotationMatrix()
		{
			if(rotMatrix != null)
				return rotMatrix;
				
			RotMatrix = GtkGL.RotationMatrix.Identity;
			
			return RotMatrix;
		}
        
    }
    
}
