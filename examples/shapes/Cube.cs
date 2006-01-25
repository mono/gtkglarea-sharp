namespace GtkGL {
    using System;
    
    using gl=Tao.OpenGl.Gl;    
    
    public class Cube : GtkGL.GLObjectBase, GtkGL.IGLObject {
     
        protected override void DrawObject ()
        {
			gl.glBegin(gl.GL_QUADS);                                    // Draw a Cube

				gl.glColor4f(0.0f,1.0f,0.0f, alphaValue);              	// Set The Color To Green
				gl.glVertex3f( 1.0f, 1.0f,-1.0f);                       // Top Right Of The Quad (Top)
				gl.glVertex3f(-1.0f, 1.0f,-1.0f);                       // Top Left Of The Quad (Top)
				gl.glVertex3f(-1.0f, 1.0f, 1.0f);                       // Bottom Left Of The Quad (Top)
				gl.glVertex3f( 1.0f, 1.0f, 1.0f);                       // Bottom Right Of The Quad (Top)

				gl.glColor4f(1.0f,0.5f,0.0f, alphaValue);                           // Set The Color To Orange
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);                       // Top Right Of The Quad (Bottom)
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);                       // Top Left Of The Quad (Bottom)
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);                       // Bottom Left Of The Quad (Bottom)
				gl.glVertex3f( 1.0f,-1.0f,-1.0f);                       // Bottom Right Of The Quad (Bottom)

				gl.glColor4f(1.0f,0.0f,0.0f, alphaValue);                           // Set The Color To Red
				gl.glVertex3f( 1.0f, 1.0f, 1.0f);                       // Top Right Of The Quad (Front)
				gl.glVertex3f(-1.0f, 1.0f, 1.0f);                       // Top Left Of The Quad (Front)
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);                       // Bottom Left Of The Quad (Front)
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);                       // Bottom Right Of The Quad (Front)

				gl.glColor4f(1.0f,1.0f,0.0f, alphaValue);                           // Set The Color To Yellow
				gl.glVertex3f( 1.0f,-1.0f,-1.0f);                       // Bottom Left Of The Quad (Back)
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);                       // Bottom Right Of The Quad (Back)
				gl.glVertex3f(-1.0f, 1.0f,-1.0f);                       // Top Right Of The Quad (Back)
				gl.glVertex3f( 1.0f, 1.0f,-1.0f);                       // Top Left Of The Quad (Back)

				gl.glColor4f(0.0f,0.0f,1.0f, alphaValue);                           // Set The Color To Blue
				gl.glVertex3f(-1.0f, 1.0f, 1.0f);                       // Top Right Of The Quad (Left)
				gl.glVertex3f(-1.0f, 1.0f,-1.0f);                       // Top Left Of The Quad (Left)
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);                       // Bottom Left Of The Quad (Left)
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);                       // Bottom Right Of The Quad (Left)

				gl.glColor4f(1.0f,0.0f,1.0f, alphaValue);                           // Set The Color To Violet
				gl.glVertex3f( 1.0f, 1.0f,-1.0f);                       // Top Right Of The Quad (Right)
				gl.glVertex3f( 1.0f, 1.0f, 1.0f);                       // Top Left Of The Quad (Right)
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);                       // Bottom Left Of The Quad (Right)
				gl.glVertex3f( 1.0f,-1.0f,-1.0f);                       // Bottom Right Of The Quad (Right)

			gl.glEnd();                 
		}
    }
}
