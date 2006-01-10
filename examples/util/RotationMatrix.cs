
namespace GtkGL {
    using System;
    
    public class RotationMatrix {
    	private double[] matrix;
    	
    	public double[] Matrix
    	{
    		get { if(matrix != null) return matrix; matrix = Identity.Matrix; return matrix; }
    		set { if(value.Length == 16) matrix = value; }
    	}
    	
    	        
        public static RotationMatrix operator +(RotationMatrix m1, RotationMatrix m2)
        {
        	double[] newRot = new double[16];
        	
        	for(int i = 0; i < 16; i++){
        		newRot[i] = m1.Matrix[i] + m2.Matrix[i];
        	}
        	
        	return new GtkGL.RotationMatrix(newRot);
        }
    	
        void Clear()
        {
        	matrix = new double[16];
        	for(int i = 0; i < 16; i++){
        		matrix[i] = 0.0;
        	}   
        }
        
    	static RotationMatrix identity;
    	// The identity matrix:
    	/*
    	
    	| 1.0 0.0 0.0 0.0 |
    	| 0.0 1.0 0.0 0.0 |
    	| 0.0 0.0 1.0 0.0 |
    	| 0.0 0.0 0.0 1.0 |
    	
    	*/
    	
    	public static RotationMatrix Identity
    	{
    		get { 
    			if(identity != null)
	    			return identity;
    			    			
    			double[] identityMatrix = new double[16];
    			
	        	for(int i = 0; i < 16; i++){
	        		if(i % 5 == 0)
	        			identityMatrix[i] = 1;
	        		else
	        			identityMatrix[i] = 0;
	        	}   
    			
    			identity = new RotationMatrix(identityMatrix);
    			
    			return identity;
    		}
    	}
    	
    	GtkGL.Quaternion quat;
        
        public RotationMatrix() {
			matrix = Identity.Matrix;
        }
        
        public RotationMatrix(double[] rotMatrix)
        {
        	Matrix = rotMatrix;
        }
        
        public GtkGL.EulerRotation ToEulerRotation()
        {
        	GtkGL.EulerRotation eRot;
        	
        	double angle_x, angle_y, angle_z;
        	double C, RADIANS = 57.2999999999999;
        	double trX, trY;        	
        	
            angle_y     =  Math.Asin( matrix[2]);        /* Calculate Y-axis angle */
    		C           =  Math.Cos( angle_y );
    		angle_y    *=  RADIANS;
    		
			// Throw an exception if we have encountered Gimbal Lock
    		if ( Math.Abs( C ) <= 0.005 )
    			throw new GtkGL.EulerRotation.GimbalLock();
      		
      		trX      =  matrix[10] / C;           
      		trY      = -matrix[6]  / C;
      		angle_x  = Math.Atan2( trY, trX ) * RADIANS;
      		trX      =  matrix[0] / C;            /* Get Z-axis angle */
      		trY      = -matrix[1] / C;
      		angle_z  = Math.Atan2( trY, trX ) * RADIANS;

    		/* return only positive angles in [0,360] */
    		if (angle_x < 0) angle_x += 360.0;
    		if (angle_y < 0) angle_y += 360.0;
    		if (angle_z < 0) angle_z += 360.0;
    		
    		eRot = new EulerRotation();
    		
    		eRot.X = angle_x;
    		eRot.Y = angle_y;
    		eRot.Z = angle_z;
    		
    		return eRot;
        }
        
        public GtkGL.Quaternion ToQuaternion()
        {
        	double qx, qy, qz, qw;
        	qx = qy = qz = qw = 0.0;
        	// Create a quaternion
			return new GtkGL.Quaternion(qw, qx, qy, qz);
        }
    }
    
}
