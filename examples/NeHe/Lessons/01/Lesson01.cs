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
		public static int Main (string[] args)
		{
			Gtk.Application.Init ();

			GtkGL.GladeExample gladeWindow = new GtkGL.GladeExample ();
			
			// Show GL Window			
			// Is this a bug?  Shouldn't ShowAll do what these two commands do?
			// gladeWindow.window.ShowAll();
			gladeWindow.window.Show();
			gladeWindow.glw.Show();
			
			Gtk.Application.Run ();
			
			return 0;
		}
	}
}
