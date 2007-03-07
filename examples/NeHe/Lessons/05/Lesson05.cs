// created on 12/13/2005 at 1:51 PM
using System;
using System.Timers;

using Gtk;
using GtkSharp;

using GtkGL;

// This code is based on work from Alp Toker and the NeHe lesson here:
// http://nehe.gamedev.net/data/lessons/lesson.asp?lesson=01
namespace NeHe
{
	public class Lesson05
	{
		static GtkGL.GladeExample gladeWindow;
		static Cube cube;
		static Pyramid pyramid;
	
		public static int Main (string[] argc)
		{
			Application.Init ();

			GtkGL.GladeExample gladeWindow = new GtkGL.GladeExample();			

	   		// Create a new Triangle object, translate it a bit and add it to our gl window
			pyramid = new Pyramid();
			pyramid.Translate(-1.5,0.0,-3);
			gladeWindow.glw.AddGLObject( pyramid );			

			// Create a new Square object, translate it a bit and add it to our gl window
			cube = new Cube();
			cube.Translate(1.5,0.0,-3);
			gladeWindow.glw.AddGLObject( cube );
			
			// Show GL Window
			// Is this a bug?  Shouldn't ShowAll do what these two commands do?
			// gladeWindow.glwidget.ShowAll();
			gladeWindow.glwidget.Show();
			gladeWindow.glw.Show();
			
			// Rotate (and draw) our objects once evry 50 ms 
			GLib.Timeout.Add (50, new GLib.TimeoutHandler (NeHe.Lesson05.RotateObjects));			
			
			Application.Run ();

			return 0;
		}
		
		static GtkGL.Quaternion pyramidRot = new GtkGL.EulerRotation(-0.05, -0.05, -0.05).ToQuaternion();
		static GtkGL.Quaternion cubeRot = new GtkGL.EulerRotation(0.05, 0.05, 0.05).ToQuaternion();
		
		public static bool RotateObjects()
		{
			pyramid.Rotate(pyramidRot);
			cube.Rotate(cubeRot);
			
			return true;
		}
	}
}
