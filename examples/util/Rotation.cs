
namespace GtkGL {
    using System;
    
    public class Rotation {
        
        public enum Direction {
        	Clockwise,
        	CounterClockwise
        }
 
        public float xRot, yRot, zRot;
        public Direction dir;
              
        public Rotation(Direction d, float xr, float yr, float zr)
        {
        	this.dir  = d;
        	this.xRot = xr;
        	this.yRot = yr;
        	this.zRot = zr;
        	
        }
    }
    
}
