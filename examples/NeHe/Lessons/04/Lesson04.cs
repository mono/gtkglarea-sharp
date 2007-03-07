// created on 12/13/2005 at 1:51 PM
using System;
using System.Timers;

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
	public class Lesson04
	{
		static GtkGL.GladeExample gladeWindow;
		static GtkGL.ColoredSquare square;
		static GtkGL.ColoredTriangle triangle;
	
		public static int Main (string[] argc)
		{
			Application.Init ();

			GtkGL.GladeExample gladeWindow = new GtkGL.GladeExample();			

	   		// Create a new Triangle object, translate it a bit and add it to our gl window
			triangle = new GtkGL.ColoredTriangle();
			triangle.Translate(-1.5,0.0,-3);
			gladeWindow.glw.AddGLObject( triangle );			

			// Create a new Square object, translate it a bit and add it to our gl window
			square = new GtkGL.ColoredSquare();
			square.Translate(1.5,0.0,-3);
			gladeWindow.glw.AddGLObject( square );
			
			// Show GL Window
			// Is this a bug?  Shouldn't ShowAll do what these two commands do?
			// gladeWindow.glwidget.ShowAll();
			gladeWindow.glwidget.Show();
			gladeWindow.glw.Show();
			
			// Rotate (and draw) our objects once evry 50 ms 
			GLib.Timeout.Add (50, new GLib.TimeoutHandler (NeHe.Lesson04.RotateObjects));			
			
			Application.Run ();

			return 0;
		}
		
		static GtkGL.Quaternion triRot = new GtkGL.EulerRotation(0, 0.05, 0).ToQuaternion();
		static GtkGL.Quaternion squareRot = new GtkGL.EulerRotation(0, 0, 0.05).ToQuaternion();
		
		public static bool RotateObjects()
		{
			triangle.Rotate(triRot);
			square.Rotate(squareRot);
			
			return true;
		}
	}
}
