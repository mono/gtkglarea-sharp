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
	

	public Engine (string[] args) 
	{
		Application.Init ();
		
		GlWidget glw = new GlWidget();
		
		buildControlWindow(glw);

		Glade.XML gxml = new Glade.XML (null, "glwidget.glade", "glwidget", null);		

		// Connect the Signals defined in Glade
		gxml.Autoconnect (this);
		
		Gtk.VBox vbox1 = (Gtk.VBox)gxml["vbox1"];
		vbox1.PackStart( glw.glArea );
		
		glw.glArea.Show();
		
		Application.Run ();
	}
	
	void buildControlWindow(GlWidget glw)
	{
		Glade.XML controlXML = new Glade.XML (null, "glwidget.glade", "controlWindow", null);
		
		Gtk.Window controlWindow = (Gtk.Window)controlXML["controlWindow"];
		
		controlWindow.Visible = true;
		
		Gtk.Table t = (Gtk.Table)controlXML["table1"];
				
		ObjectRotationButton btnXMinus =
			new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button), glw.glArea, new GtkGL.Rotation(1, 1.0f, 0.0f, 0.0f));

		t.Attach(btnXMinus, 2, 3, 0, 1);
		
		ObjectRotationButton btnXPlus =
			new ObjectRotationButton(new Image(Stock.Add, IconSize.Button), glw.glArea, new GtkGL.Rotation(-1, 1.0f, 0.0f, 0.0f));
			
		t.Attach(btnXPlus, 3, 4, 0, 1);

		ObjectRotationButton btnYMinus =
			new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button), glw.glArea, new GtkGL.Rotation(1, 1.0f, 1.0f, 0.0f));

		t.Attach(btnYMinus, 2, 3, 1, 2);
		
		ObjectRotationButton btnYPlus =
			new ObjectRotationButton(new Image(Stock.Add, IconSize.Button), glw.glArea, new GtkGL.Rotation(-1, 1.0f, 1.0f, 0.0f));
			
		t.Attach(btnYPlus, 3, 4, 1, 2);
		
		ObjectRotationButton btnZMinus =
			new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button), glw.glArea, new GtkGL.Rotation(1, 1.0f, 0.0f, 1.0f));

		t.Attach(btnZMinus, 2, 3, 2, 3);
		
		ObjectRotationButton btnZPlus =
			new ObjectRotationButton(new Image(Stock.Add, IconSize.Button), glw.glArea, new GtkGL.Rotation(-1, 0.0f, 0.0f, 1.0f));
			
		t.Attach(btnZPlus, 3, 4, 2, 3);
		
		controlWindow.ShowAll();

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

