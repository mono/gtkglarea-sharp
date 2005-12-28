
namespace GtkGL {
    using System;
    
    public class Rotation {
        
        float xRot, yRot, zRot;
        int angleMult;
        
        public Rotation(int am, float xr, float yr, float zr)
        {
        	this.angleMult = am;
        	this.xRot = xr;
        	this.yRot = yr;
        	this.zRot = zr;
        	
        }
    }
    
}
