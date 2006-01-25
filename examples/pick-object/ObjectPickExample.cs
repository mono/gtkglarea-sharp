
namespace GtkGL
{
	using System;
	using Gtk;
	
	public class ObjectPickExample
	{
		public GtkGL.PickWidget glw;
		public Gtk.Window window;
		Glade.XML gxml;

		public ObjectPickExample()
		{
			// Create a new GL widget
			glw = new PickWidget();
			
			// Read in the glade file that describes the widget layout
			gxml = new Glade.XML (null, "glwidget.glade", "glwidget", null);

			// Connect the Signals defined in Glade
			gxml.Autoconnect (glw);
			
			// Pack the gl window into the vbox
	        Gtk.VBox vbox1 = (Gtk.VBox)gxml["vbox1"];
	        vbox1.PackStart ( glw );
	        
			window = (Gtk.Window)gxml["glwidget"];
		}
	}
	
}
