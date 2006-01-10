
namespace GtkGL {
    using System;
    
    public class Vector {
    
    	public double x, y, z;
        
        public Vector(double x, double y, double z) {
        	this.x = x;
        	this.y = y;
        	this.z = z;
        }
        
        public Vector(double[] v) {
        	if(v == null || v.Length < 3)
        		this.x = this.y = this.z = 0.0;
        	else{
        		this.x = v[0];
        		this.y = v[1];
        		this.z = v[2];
        	}
        }
        
        
        public static Vector Cross(Vector v1, Vector v2)
        {
        	return new Vector( (v1.y * v2.z) - (v1.z * v2.y),
        					   (v1.z * v2.x) - (v1.x * v2.z),
        					   (v1.x * v2.y) - (v1.y * v2.x)
        					 );									 
        }
        
        public static Vector operator +(Vector v1, Vector v2)
        {
        	return new Vector(v1.x + v2.x,
        					  v1.y + v2.y,
        					  v1.z + v2.z
        					 );
        }
        
        public static Vector operator *(Vector v1, double factor)
        {
        	Vector result = new Vector( v1.x * factor,
        								v1.y * factor,
        								v1.z * factor
        							   );
        	return result;
        }
        
        public static double Dot(Vector v1, Vector v2)
        {
        	return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z * v2.z);
        }
        
       
    }
    
}
