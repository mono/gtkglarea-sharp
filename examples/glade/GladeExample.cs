// project created on 11/25/2005 at 10:32 PM
using System;
using Gtk;
using Glade;
using GtkGL;

namespace GtkGL
{
	public class GladeExample
	{
		public GLWidget glw;
		public Gtk.Window window;

		public GladeExample () 
		{
			Glade.XML gxml = new Glade.XML (null, "glwidget.glade", "glwidget", null);

			// Connect the Signals defined in Glade
			gxml.Autoconnect (this);
			
			// Create a new glw widget and request a size
			glw = new GLWidget ();

			// Create a new Vertical Box that the glw can live in
			VBox vb = (Gtk.VBox)gxml["vbox1"];
						
			// Pack the glw widget into the VBox
			vb.PackStart (glw);
			
			window = (Gtk.Window)gxml["glwidget"];
		}
		
		private void OnQuit (object o, System.EventArgs e){
			Application.Quit();
		}

		private void OnWindowDeleteEvent (object sender, DeleteEventArgs a) 
		{
			Application.Quit ();
			a.RetVal = true;
		}
	}

}