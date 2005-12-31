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

	GLWidget glw;
	GLObjectRotationController controller;

	public Engine (string[] args) 
	{
		Application.Init ();
		
		// Create a new GL widget
		glw = new GLWidget();

   		// Create a new Teapot object
		GtkGL.Teapot teapot = new Teapot();

		// Create a controller that manages rotation of the Teapot object
		controller = new GLObjectRotationController(teapot);
		
		// Add our Teapot object to the GLWidget's list of associated GLObjects
		glw.AddGLObject( teapot );
		
		// Read in the glade file that describes the widget layout
        Glade.XML gxml = new Glade.XML (null, "glwidget.glade", "glwidget", null);

        // Connect the Signals defined in Glade
        gxml.Autoconnect ( glw );

		// Pack the gl window into the vbox
        Gtk.VBox vbox1 = (Gtk.VBox)gxml["vbox1"];
        vbox1.PackStart ( glw );
        
		// Show the GL widget
		glw.Show();
		
		// Go dog, go!
		Application.Run ();
	}
	
}

