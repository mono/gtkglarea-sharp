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
	public class Lesson02
	{
		/* GlWidget is a widget defined in the GtkGL namespace and is a
		 * specialized GtkDrawingArea with GL cow powers.
		 */
		
		GtkGL.GLWidget glw;
		GtkGL.Triangle triangle = null;
		GtkGL.Square square = null;

		public static int Main (string[] argc)
		{
			new Lesson02();

			return 1;
		}		

		public Lesson02()
		{
			Gtk.Application.Init ();
			
			Glade.XML gxml = new Glade.XML (null, "glwidget.glade", "glwidget", null);
			
			// Connect the Signals defined in Glade
			gxml.Autoconnect (this);
			
			// Create a new glw widget and request a size
			glw = new GLWidget ();		
			

	   		// Create a new Triangle object and add it to our gl window
			triangle = new Triangle();
			triangle.Translate(-1.5,0.0,-3);
			glw.AddGLObject( triangle );			

			// Create a new Square object and add it to our gl window
			square = new Square();
			square.Translate(1.5,0.0,-3);
			glw.AddGLObject( square );
			
			// Create a new Vertical Box that the glw can live in
			VBox vb = (Gtk.VBox)gxml["vbox1"];
			
			// Pack the glw widget into the VBox
			vb.PackStart (glw, true, true, 0);

			// Show all of glw's contained widgets
			Gtk.Window win = (Gtk.Window)gxml["glwidget"];
			win.ShowAll ();
			
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
		
		void OnUnrealized (object o, EventArgs e)
		{
			Application.Quit();
		}
		

	}
}
