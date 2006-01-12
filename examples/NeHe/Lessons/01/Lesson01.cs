// created on 12/13/2005 at 1:51 PM
using System;
using Gtk;
using GtkSharp;

using GtkGL;

using Tao.OpenGl;

using gl=Tao.OpenGl.Gl;
using glu=Tao.OpenGl.Glu;

// This code is based on work from Alp Toker and the NeHe lesson here:
// http://nehe.gamedev.net/data/lessons/lesson.asp?lesson=01
namespace NeHe
{
	public class Lesson01
	{
	
	public static void Main (string[] args)
	{
		new Lesson01 ();
	}
	
	public Lesson01()
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
}
