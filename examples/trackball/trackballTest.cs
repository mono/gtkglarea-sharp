// project created on 11/25/2005 at 10:32 PM
using System;
using Gtk;
using Glade;

using GtkGL;

public class Engine
{
	public static void Main (string[] args)
	{
		new Engine (args);
	}
	
	GtkGL.TrackballWidget glw;

	public Engine (string[] args) 
	{
		Application.Init ();

		// Create a new GL widget
		glw = new TrackballWidget();
		
		// Create a new Teapot object
		GtkGL.Teapot teapot = new Teapot();

		// Add our Teapot object to the GLWidget's list of associated GLObjects
		glw.AddGLObject( teapot );
		
		// Read in the glade file that describes the widget layout
		Glade.XML gxml = new Glade.XML (null, "glwidget.glade", "glwidget", null);

		// Connect the Signals defined in Glade
		gxml.Autoconnect (this);
		
		// Pack the gl window into the vbox
        Gtk.VBox vbox1 = (Gtk.VBox)gxml["vbox1"];
        vbox1.PackStart ( glw );

		// Show the GL widget
		glw.Show();
		
		// Go dog.  Go!
		Application.Run ();
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

