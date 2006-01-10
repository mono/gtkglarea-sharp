
namespace GtkGL {
    using System;
    
    
    public class EulerRotation {
        double x, y, z;

        GtkGL.RotationMatrix rotMatrix;
        GtkGL.Quaternion quat;
        
        public static EulerRotation operator -(EulerRotation rot1, EulerRotation rot2)
        {
        	EulerRotation newRotation = new EulerRotation(0.0, 0.0, 0.0);

        	newRotation.X = Math.Abs( rot1.X - rot2.X );
        	newRotation.Y = Math.Abs( rot1.Y - rot2.Y );
        	newRotation.Z = Math.Abs( rot1.Z - rot2.Z );
        	
        	return newRotation;
        }
        
        public static EulerRotation operator +(EulerRotation rot1, EulerRotation rot2)
        {
        	if(rot1 == null)
        		if(rot2 == null)
        			return null;
        		else
        			return rot2;
       		
       		if(rot2 == null)
        		return rot1;
        
        	EulerRotation newRotation = new EulerRotation(0.0, 0.0, 0.0);

        	newRotation.X = ( (rot1.X + rot2.X) % 360 );
        	newRotation.Y = ( (rot1.Y + rot2.Y) % 360 );
        	newRotation.Z = ( (rot1.Z + rot2.Z) % 360 );

        	return newRotation;
        }
        
        public static bool operator ==(EulerRotation rot1, EulerRotation rot2)
        {
        	if(Object.Equals(rot1, null))
	        	if(Object.Equals(rot2, null))
        			return true;
        		else
        			return false;
        			
        	if(Object.Equals(rot2, null))
        		return false;
        		        
			// We consider values within 0.001 of each other to be equal
			if(Math.Abs(rot1.X - rot2.X) <= 0.001 &&
			   Math.Abs(rot1.Y - rot2.Y) <= 0.001 &&
			   Math.Abs(rot1.Z - rot2.Z) <= 0.001 )
				return true;
				
			return false;
        }

        public static bool operator !=(EulerRotation rot1, EulerRotation rot2) {
			bool isEqual = (rot1 == rot2);
			
			if(isEqual)
				return false;
			else
				return true;

        }
        
        // An event that gets triggered when X, Y or Z are updated
        public event EventHandler Updated;    
        
		public double X	{
			get { return x; }
			set { x = value; if(Updated != null) Updated(this, null); }
		}
        
		public double Y	{
			get { return y; }
			set { y = value; if(Updated != null) Updated(this, null); }
		}

		public double Z	{
			get { return z; }
			set { z = value; if(Updated != null) Updated(this, null); }
		}

		void ClearCache(object o, EventArgs e)
		{
			// This should happen when the Updated handler fires
			quat = null;
			rotMatrix = null;
		}

	   	public class GimbalLock : System.Exception {
    		GimbalLock() : base() {}
	    }

        static EulerRotation identity = null;
        
		// This represents a null rotation
        public static EulerRotation Identity
        {
        	get {
	        	identity = new EulerRotation(0.0, 0.0, 0.0);
	        	return identity;
        	}
        }
        
        void ConnectHandlers()
        {
        	// When we are updated, clear the cached Quaternion and Rotation matrix
        	this.Updated += this.ClearCache;
        }
        
        public EulerRotation()
        {
        	x = 0.0;
        	y = 0.0;
        	z = 0.0;
        	
        	ConnectHandlers();
        }
        
        public EulerRotation(double x, double y, double z)
        {
        	this.x = x;
        	this.y = y;
        	this.z = z;
        	
        	ConnectHandlers();        	
        }
        
        public GtkGL.RotationMatrix ToRotMatrix()
        {
        	if(rotMatrix != null)
        		return this.rotMatrix;
        		
        	double[] matrix = GtkGL.RotationMatrix.Identity.Matrix;
        	
        	// Convert to radians
        	double Xradians = x * 2 * Math.PI / 360.0;
        	double Yradians = y * 2 * Math.PI / 360.0;
        	double Zradians = z * 2 * Math.PI / 360.0;
        	
		    double ch = Math.Cos(Xradians);
		    double sh = Math.Sin(Xradians);
		    
		    double ca = Math.Cos(Yradians);
		    double sa = Math.Sin(Yradians);
		    
		    double cb = Math.Cos(Zradians);
		    double sb = Math.Sin(Zradians); 

		    matrix[0*4 + 0] = ch * ca;
		    matrix[0*4 + 1] = sh*sb - ch*sa*cb;
		    matrix[0*4 + 2] = ch*sa*sb + sh*cb;
		    matrix[1*4 + 0] = sa;
		    matrix[1*4 + 1] = ca*cb;
		    matrix[1*4 + 2] = -ca*sb;
		    matrix[2*4 + 0] = -sh*ca;
		    matrix[2*4 + 1] =  sh*sa*cb + ch*sb;
		    matrix[2*4 + 2] = -sh*sa*sb + ch*cb;
		    
		    rotMatrix = new GtkGL.RotationMatrix(matrix); 
		    
		    return rotMatrix;
        }
        
        public GtkGL.Quaternion ToQuaternion()
        {
        	if(quat != null)
        		return quat;
     	
			// Assuming the angles are in radians.
		    double c1 = Math.Cos(x/2);
		    double s1 = Math.Sin(x/2);
		    double c2 = Math.Cos(y/2);
		    double s2 = Math.Sin(y/2);
		    double c3 = Math.Cos(z/2);
		    double s3 = Math.Sin(z/2);
		    double c1c2 = c1*c2;
		    double s1s2 = s1*s2;
        	
        	quat = new GtkGL.Quaternion(c1c2*c3 - s1s2*s3,
        								c1c2*s3 + s1s2*c3,
        								s1*c2*c3 + c1*s2*s3,
        								c1*s2*c3 - s1*c2*s3);
        	
        	return quat;
        }
    }
    
}
