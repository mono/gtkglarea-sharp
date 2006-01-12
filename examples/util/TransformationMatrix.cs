
namespace GtkGL {
    using System;
    
    public class TransformationMatrix {
    	private double[] matrix;
    	
    	public double[] Matrix
    	{
    		get { if(matrix != null) return matrix; matrix = Identity.Matrix; return matrix; }
    		set { if(value.Length == 16) matrix = value; }
    	}
    	
    	        
        public static TransformationMatrix operator +(TransformationMatrix m1, TransformationMatrix m2)
        {
        	double[] newMatrix = new double[16];
        	
        	for(int i = 0; i < 16; i++){
        		newMatrix[i] = m1.Matrix[i] + m2.Matrix[i];
        	}
        	
        	return new GtkGL.TransformationMatrix(newMatrix);
        }
        
        public static TransformationMatrix operator *(TransformationMatrix m1, TransformationMatrix m2)
        {
        	double[] newMatrix = new double[16];
			Console.WriteLine("Multiplying the matrix!");
        	for(int i = 0; i < 16; i++){
        		double sum = 0.0;
        		for(int j = 0; j < 4; j++){
        			sum += m1.Matrix[(i%4) + j*4] * m2.Matrix[(i/4)*4 + j]; 
        		}
        		newMatrix[i] = sum;
        	}
        	
        	return new GtkGL.TransformationMatrix(newMatrix);
        }
    	
        void Clear()
        {
        	matrix = new double[16];
        	for(int i = 0; i < 16; i++){
        		matrix[i] = 0.0;
        	}   
        }
        
    	static TransformationMatrix identity;
    	// The identity matrix:
    	/*
    	
    	| 1.0 0.0 0.0 0.0 |
    	| 0.0 1.0 0.0 0.0 |
    	| 0.0 0.0 1.0 0.0 |
    	| 0.0 0.0 0.0 1.0 |
    	
    	*/
    	
    	public static TransformationMatrix Identity
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
    			
    			identity = new TransformationMatrix(identityMatrix);
    			
    			return identity;
    		}
    	}
    	
    	GtkGL.Quaternion quat;
        
        public TransformationMatrix() {
			matrix = Identity.Matrix;
        }
        
        public TransformationMatrix(double[] rotMatrix)
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
