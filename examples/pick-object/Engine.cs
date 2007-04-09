// created on 12/13/2005 at 1:51 PM
using System;
using System.Timers;

using Gtk;
using GtkSharp;

using GtkGL;
// This code is based on work from Alp Toker and the NeHe lesson here:
// http://nehe.gamedev.net/data/lessons/lesson.asp?lesson=01
namespace GtkGL
{
	public class Engine
	{
		public static int Main (string[] argc)
		{
			Application.Init ();

			ObjectPickExample example = new ObjectPickExample();
			
	   		// Create a new Pyramid object, translate it a bit left and add it to our gl window
			Pyramid pyramid = new Pyramid();
			pyramid.Translate(-1.5,0.0,-3);
			example.glw.AddGLObject( pyramid );
			pyramid.SelectedEvent += Throb;

			// Create a new Cube object, translate it a bit right and add it to our gl window
			Cube cube = new GtkGL.Cube();
			cube.Translate(1.5,0.0,-3);
			example.glw.AddGLObject( cube );
			cube.SelectedEvent += Throb;
			
			// Show GL Window
			// Is this a bug?  Shouldn't ShowAll do what these two commands do?
			// engine.window.ShowAll();
			example.window.Show();
			example.glw.Show();
					
			Application.Run ();

			return 0;
		}
		
		public static void Throb (object o, System.EventArgs e)
		{
			// This function should scale object o up on x,y and z
			// then it should scale up further
			// until it gets big enough
			// then it should scale it down
			// until it's the original size
			// then scale down a bit
			// until it gets small enough
			// and then do the opposite process one more time until it reaches "normal" size
			for(int i = 0; i < 5; i++){
				( (IGLObject) o ).Scale(1.1);
				System.Threading.Thread.Sleep(25);
			}
			for(int i = 0; i < 10; i++){
				( (IGLObject) o ).Scale(0.91);
				System.Threading.Thread.Sleep(25);
			}
			for(int i = 0; i < 5; i++){
				( (IGLObject) o ).Scale(1.1);
				System.Threading.Thread.Sleep(25);
			}
		}
	}
}
