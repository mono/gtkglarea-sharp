using GtkGL;

namespace Mdl {
    using System;
    
    public class Engine {
		public static int Main (string[] args)
		{
			MdlLoader loader = new MdlLoader();
			Mdl myMdl = loader.Load("/home/cjcollier/id/models/armor.mdl");
		
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
