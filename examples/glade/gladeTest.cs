// project created on 11/25/2005 at 10:32 PM
using System;
using Gtk;
using Glade;

public class Engine
{
	public static void Main (string[] args)
	{
		new Engine (args);
	}
	

	public Engine (string[] args) 
	{
		Application.Init ();

		Glade.XML gxml = new Glade.XML (null, "glwidget.glade", "glwidgets", null);

		// Connect the Signals defined in Glade
		gxml.Autoconnect (this);
		
		GlWidget glw = new GlWidget();

		Gtk.Alignment alignment1 = (Gtk.Alignment)gxml["alignment1"];
		alignment1.Add( glw.glArea );
		
		glw.glArea.Show();
		
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

