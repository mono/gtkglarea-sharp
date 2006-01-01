/*  Mad props to ##opengl on freenode and http://skal.planet-d.net/demo/matrixfaq.htm#Q37 */

namespace GtkGL {
    using System;
    
    public struct EulerRotation {
    	public float x, y, z;
    }
    
    public class RotationUtil {
        
        private RotationUtil() {
        }
        
        public static EulerRotation RotMatrixToEuler(float[] mat)
        {
        	float angle_x, angle_y, angle_z;
        	float C, RADIANS = 57.2999999999999f;
        	float trX, trY;
        	
        	if(mat == null)
        		throw new System.ArgumentNullException();
        	
        	if(mat.Length != 16)
        		throw new System.ArgumentException();
        	
            angle_y     =  (float) Math.Asin( mat[2]);        /* Calculate Y-axis angle */
    		C           =  (float) Math.Cos( angle_y );
    		angle_y    *=  RADIANS;
    		if ( Math.Abs( C ) > 0.005 )             /* Gimball lock? */
      		{
      			trX      =  mat[10] / C;           /* No, so get X-axis angle */
      			trY      = -mat[6]  / C;
      			angle_x  = (float) Math.Atan2( trY, trX ) * RADIANS;
      			trX      =  mat[0] / C;            /* Get Z-axis angle */
      			trY      = -mat[1] / C;
      			angle_z  = (float) Math.Atan2( trY, trX ) * RADIANS;
    	  	} else {                                 /* Gimball lock has occurred */
		    	angle_x  = 0;                      /* Set X-axis angle to zero */
		    	trX      =  mat[5];                 /* And calculate Z-axis angle */
      			trY      =  mat[4];
      			angle_z  = (float) Math.Atan2( trY, trX ) * RADIANS;
		    }

    		/* return only positive angles in [0,360] */
    		if (angle_x < 0) angle_x += 360.0f;
    		if (angle_y < 0) angle_y += 360.0f;
    		if (angle_z < 0) angle_z += 360.0f;
    		
    		EulerRotation eRot = new EulerRotation();
    		
    		eRot.x = angle_x;
    		eRot.y = angle_y;
    		eRot.z = angle_z;
    		
    		return eRot;
        }
    }    
}
