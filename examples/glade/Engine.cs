namespace GladeExample {
    using System;
    
    public class Engine {
		public static int Main (string[] args)
		{
			Gtk.Application.Init ();

			GtkGL.GladeExample ge = new GtkGL.GladeExample ();

			// Show the GL widget window
			ge.window.Show();
			ge.glw.Show();
			
			Gtk.Application.Run ();
			
			return 0;
		}
    }   
}
