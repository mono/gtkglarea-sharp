using System;

using Gtk;
using Glade;

namespace GladeExample
{
	public class Engine
	{
		public static int Main (string[] args)
		{
			// Prepare application execution. This is where you
			// would load up your glade files and any other
			// resources.
			Gtk.Application.Init();

			GtkGL.GladeExample ge = new GtkGL.GladeExample();

			// Show the GL widget window and all it's children.
			ge.glwidget.ShowAll();
			
			// Begin application processing.
			Gtk.Application.Run();

			// And we're done.
			return 0;
		}
    }
}
