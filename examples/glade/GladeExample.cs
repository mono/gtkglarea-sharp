using System;

using Gtk;
using Glade;

using GtkGL;

namespace GtkGL
{
	public class GladeExample
	{
		[Widget]
		public GLWidget glw;

		[Widget]
		public Window glwidget;

		public GladeExample ()
		{
			// Load the file glwidget.glade and collect all the objects starting from
			// the specified "glwidget" root. This is the name of the Window in the
			// glade file.
			Glade.XML gxml = new Glade.XML(null, "glwidget.glade", "glwidget", null);

			// Connect the Signals defined in Glade. Also connect any matching names
			// of objects to properties of the object specified ('this') that are marked
			// with the [Widget] attribute. In turn this makes this.glwidget get assigned
			// to the glwidget Gtk.Window object.
			gxml.Autoconnect(this);

			// Create a new glw widget and request a size.
			glw = new GLWidget();

			// Create a new Vertical Box that the glw can live in.
			VBox vb = (Gtk.VBox)gxml["vbox1"];

			// Pack the glw widget into the VBox.
			vb.PackStart (glw);

			// Create a new clickable button.
			Button butModal = new Button();
			butModal.Label = "Press For Modal!";
			butModal.Clicked += new EventHandler(butModal_Clicked);

			// Stick the button into the vbox.
			vb.PackStart(butModal);
		}

		// Called when the butModal button is clicked on.
		void butModal_Clicked(object sender, EventArgs e)
		{
			// Optionally desensitize the parent dialog to
			// better suggest focus.
			glwidget.Sensitive = false;

			// Create a dialog out of the wrapped up class.
			DialogExample dlg = new DialogExample(glwidget);

			// Get the response from the dialog.
			ResponseType rt = (ResponseType)dlg.dlgExample.Run();

			// If it was OK, instead of cancel, we do something.
			if (rt == ResponseType.Ok) Console.WriteLine("Accepted!");

			// Destroying the dialog regardless of response.
			dlg.dlgExample.Destroy();

			// Bring our sensitivity and thus, focus, back to our
			// parent window.
			glwidget.Sensitive = true;
		}

		private void OnQuit (object o, System.EventArgs e)
		{
			// Terminate the entire application.
			Application.Quit();
		}

		private void OnWindowDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit();
			a.RetVal = true;
		}
	}

	class DialogExample
	{
		[Widget]
		public Dialog dlgExample = null;

		public DialogExample(Window parent)
		{
			Glade.XML gxml = new Glade.XML(null, "glwidget.glade", "dlgExample", null);
			gxml.Autoconnect(this);

			dlgExample.TransientFor = parent;
		}
	}
}