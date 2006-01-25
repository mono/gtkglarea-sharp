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
			
	   		// Create a new Pyramid object, translate it a bit and add it to our gl window
			Pyramid pyramid = new Pyramid();
			pyramid.Translate(-1.5,0.0,-3);
			example.glw.AddGLObject( pyramid );
			pyramid.SelectedEvent += Throb;

			// Create a new Cube object, translate it a bit and add it to our gl window
			Cube cube = new GtkGL.Cube();
			cube.Translate(1.5,0.0,-3);
			example.glw.AddGLObject( cube );
			
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
			double scaleFactor = 0.01;
			int scaleMultiplier;
			
			// This function should scale object o down on x,y and z
			// then it should scale down further
			// until it gets small enough
			// then it should scale it up
			// until it's the original size
			// then scale up a bit
			// until it gets large enough
			// and then do the opposite process one more time until it reaches "normal" size
			for(scaleMultiplier = -1; scaleMultiplier > -10; scaleMultiplier--){
				( (IGLObject) o ).Scale(scaleFactor * scaleMultiplier);
			}
			for(scaleMultiplier = -10; scaleMultiplier < 10; scaleMultiplier++){
				( (IGLObject) o ).Scale(scaleFactor * scaleMultiplier);
			}
			for(scaleMultiplier = 10; scaleMultiplier > -1; scaleMultiplier--){
				( (IGLObject) o ).Scale(scaleFactor * scaleMultiplier);
			}
		}
	}
}
