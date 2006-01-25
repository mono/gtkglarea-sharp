
namespace GtkGL {
    using System;
    
    using gl=Tao.OpenGl.Gl;
    
    public class ColoredTriangle : GtkGL.GLObjectBase,GtkGL.IGLObject{

		protected override void DrawObject()
		{
			gl.glBegin(gl.GL_TRIANGLES);					// Drawing Using Triangles
				gl.glColor3f(1.0f,0.0f,0.0f);				// Set The Color To Red
				gl.glVertex3f( 0.0f, 1.0f, 0.0f);			// Top
				gl.glColor3f(0.0f,1.0f,0.0f);				// Set The Color To Green
				gl.glVertex3f(-1.0f,-1.0f, 0.0f);			// Bottom Left
				gl.glColor3f(0.0f,0.0f,1.0f);				// Set The Color To Blue				
				gl.glVertex3f( 1.0f,-1.0f, 0.0f);			// Bottom Right
			gl.glEnd();										// Finished Drawing The Triangle
		}
	}
}
