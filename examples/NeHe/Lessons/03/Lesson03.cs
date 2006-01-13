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
namespace NeHe.Lesson03
{
	public class Lesson03
	{
		public static int Main (string[] argc)
		{
			Application.Init ();

			GtkGL.GladeExample gladeWindow = new GtkGL.GladeExample();			

	   		// Create a new Triangle object, translate it a bit and add it to our gl window
			ColoredTriangle triangle = new ColoredTriangle();
			triangle.Translate(-1.5,0.0,-3);
			gladeWindow.glw.AddGLObject( triangle );			

			// Create a new Square object, translate it a bit and add it to our gl window
			ColoredSquare square = new ColoredSquare();
			square.Translate(1.5,0.0,-3);
			gladeWindow.glw.AddGLObject( square );
			
			// Show GL Window
			// Is this a bug?  Shouldn't ShowAll do what these two commands do?
			// gladeWindow.window.ShowAll();
			gladeWindow.window.Show();
			gladeWindow.glw.Show();
			
			Application.Run ();

			return 0;
		}		
	}
}
