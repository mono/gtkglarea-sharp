namespace NeHe {
    using System;
    
    using gl=Tao.OpenGl.Gl;    
    
    public class Square : GtkGL.GLObjectBase, GtkGL.IGLObject {
     
        protected override void DrawObject ()
        {
         	gl.glBegin(gl.GL_QUADS);						// Draw A Quad
				gl.glVertex3f(-1.0f, 1.0f, 0.0f);			// Top Left
				gl.glVertex3f( 1.0f, 1.0f, 0.0f);			// Top Right
				gl.glVertex3f( 1.0f,-1.0f, 0.0f);			// Bottom Right
				gl.glVertex3f(-1.0f,-1.0f, 0.0f);			// Bottom Left
			gl.glEnd();										// Done Drawing The Quad
		}
    }
}
