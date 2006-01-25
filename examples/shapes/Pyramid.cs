
namespace GtkGL {
    using System;
    
    using gl=Tao.OpenGl.Gl;
    
    public class Pyramid : GtkGL.GLObjectBase,GtkGL.IGLObject{

		protected override void DrawObject()
		{
			gl.glBegin(gl.GL_TRIANGLES);                                   // Start drawing the Pyramid

				gl.glColor3f(1.0f,0.0f,0.0f);                           // Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);                       // Top Of Triangle (Front)
				gl.glColor3f(0.0f,1.0f,0.0f);                           // Green
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);                       // Left Of Triangle (Front)
				gl.glColor3f(0.0f,0.0f,1.0f);                           // Blue
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);                       // Right Of Triangle (Front)

				gl.glColor3f(1.0f,0.0f,0.0f);                           // Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);                       // Top Of Triangle (Right)
				gl.glColor3f(0.0f,0.0f,1.0f);                           // Blue
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);                       // Left Of Triangle (Right)
				gl.glColor3f(0.0f,1.0f,0.0f);                           // Green
				gl.glVertex3f( 1.0f,-1.0f, -1.0f);                      // Right Of Triangle (Right)

				gl.glColor3f(1.0f,0.0f,0.0f);                           // Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);                       // Top Of Triangle (Back)
				gl.glColor3f(0.0f,1.0f,0.0f);                           // Green
				gl.glVertex3f( 1.0f,-1.0f, -1.0f);                      // Left Of Triangle (Back)
				gl.glColor3f(0.0f,0.0f,1.0f);                           // Blue
				gl.glVertex3f(-1.0f,-1.0f, -1.0f);                      // Right Of Triangle (Back)

				gl.glColor3f(1.0f,0.0f,0.0f);                           // Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);                       // Top Of Triangle (Left)
				gl.glColor3f(0.0f,0.0f,1.0f);                           // Blue
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);                       // Left Of Triangle (Left)
				gl.glColor3f(0.0f,1.0f,0.0f);                           // Green
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);                       // Right Of Triangle (Left)

			gl.glEnd();                                                    // Finished Drawing sides

			gl.glBegin(gl.GL_QUADS);                                    // Draw the base
				
				gl.glColor3f(0.0f,1.0f,0.0f);                           // Green
				gl.glVertex3f(-1.0f,-1.0f, 1.0f);                       // Left Of Triangle (Front)				
				gl.glColor3f(0.0f,0.0f,1.0f);                           // Blue
				gl.glVertex3f( 1.0f,-1.0f, 1.0f);                       // Left Of Triangle (Right)
				gl.glColor3f(0.0f,1.0f,0.0f);                           // Green
				gl.glVertex3f( 1.0f,-1.0f, -1.0f);                      // Left Of Triangle (Back)
				gl.glColor3f(0.0f,0.0f,1.0f);                           // Blue
				gl.glVertex3f(-1.0f,-1.0f,-1.0f);                       // Left Of Triangle (Left)

			gl.glEnd();                                                 // Finished Drawing The Pyramid

		}
	}
}
