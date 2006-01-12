// project created on 11/25/2005 at 10:32 PM
using System;
using Gtk;
using Glade;
using GtkGL;

public class GladeExample
{
	public static void Main (string[] args)
	{
		new GladeExample (args);
	}

	public GladeExample (string[] args) 
	{
		Application.Init ();

		Glade.XML gxml = new Glade.XML (null, "glwidget.glade", "glwidget", null);

		// Connect the Signals defined in Glade
		gxml.Autoconnect (this);
		
		GLWidget glw = new GLWidget();

		Gtk.VBox vbox1 = (Gtk.VBox)gxml["vbox1"];
		vbox1.PackStart( glw );
		
		glw.Show();
		
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
