namespace LWObjectExample {
    using System;
    
    public class LWObjectExample {
		public static int Main (string[] args)
		{
			Gtk.Application.Init ();

			GtkGL.GladeExample ge = new GtkGL.GladeExample ();
			
	   		// Create a new Triangle object, translate it a bit and add it to our gl window
			LWObject penguin = new LWObject("penguin.lwo");
			gladeWindow.glw.AddGLObject( penguin );						

			// Show the GL widget window
			ge.window.Show();
			ge.glw.Show();
			
			Gtk.Application.Run ();
			
			return 0;
		}
    }   
    
}
