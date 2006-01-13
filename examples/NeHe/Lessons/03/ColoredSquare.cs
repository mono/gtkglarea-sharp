namespace NeHe {
    using System;
    
    using gl=Tao.OpenGl.Gl;    
    
    public class ColoredSquare : GtkGL.GLObjectBase, GtkGL.IGLObject {
     
        protected override void DrawObject ()
        {
			gl.glColor3f(0.5f,0.5f,1.0f);					// Set The Color To Blue One Time Only
			gl.glBegin(gl.GL_QUADS);						// Draw A Quad
				gl.glVertex3f(-1.0f, 1.0f, 0.0f);			// Top Left
				gl.glVertex3f( 1.0f, 1.0f, 0.0f);			// Top Right
				gl.glVertex3f( 1.0f,-1.0f, 0.0f);			// Bottom Right
				gl.glVertex3f(-1.0f,-1.0f, 0.0f);			// Bottom Left
			gl.glEnd();										// Done Drawing The Quad
		}
    }
}
