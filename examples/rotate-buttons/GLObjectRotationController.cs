namespace GtkGL {
    using System;
	using Gtk;
    
    public class GLObjectRotationController {
    
    	Glade.XML controlXML;
    	Gtk.Window controlWindow;
		GtkGL.IGLObject glOb;    	
        System.Collections.Hashtable entryMap;
        EulerRotation eRot;
        
        /*
        // This handler is attached to ObjectRotationButton's Rotated event.
        // Every time the button modifies the object's rotation, the Rotated event fires.
        // When the object's rotation is modified, we modify our copy of the object's rotation.
        // TODO: I personally think that we should get the rotation data from the object directly,
        // but this proves to be difficult, as I don't know how to convert from a rotation matrix
        // to Euler angles without causing gimbal lock.  This will likely be changed when I find
        // a work-around.
        */
        private void UpdateRotationValues(object o, EventArgs e)
        {
        	eRot = glOb.GetEulerRotation();
        	
        	UpdateEntryFields();
        }
        
        /*
        // This handler is attached to the entry fields' Activated event
        // When the user enters a new value and presses enter, this handler fires.
        // TODO: I also need to attach the "tabbed out of" event to this method and do some
        // final checks to make sure the current value is not the same as what "tabbed out of" 
        */
        private void UpdateObjectRotation(object o, EventArgs e)
        {
        	
        	// Clear the rotation state, don't fire the Update handlers
        	glOb.ResetRotation(false);
        
        	// Rotate the object based on our Euler angles
        	
        	// Grab the current rotation from the object
        	eRot = glOb.GetEulerRotation();
 			
 			// Get the new values from the entry fields
 			float newX = Convert.ToSingle(((Gtk.Entry)entryMap['x']).Text.ToString());
 			float newY = Convert.ToSingle(((Gtk.Entry)entryMap['y']).Text.ToString());
 			float newZ = Convert.ToSingle(((Gtk.Entry)entryMap['z']).Text.ToString());
 			
 			// Create a new rotation from these values
 			GtkGL.EulerRotation newRot = new GtkGL.EulerRotation(newX,newY,newZ);
 			
 			// Find the difference between the two
 			GtkGL.EulerRotation diffRot = eRot - newRot;
 										
        	// If the rotation has changed, create a quaternion from the Euler rotation
        	// and apply it to the object
        	if(diffRot != GtkGL.EulerRotation.Identity)
        		glOb.Rotate(diffRot);
        	
        }
        
        // This method updates the values stored in the X, Y and Z entry fields from the object's rotation
        // It is used more than once, so it gets its own handle :)
        public void UpdateEntryFields()
        {
        	// Grab the current rotation
        	eRot = glOb.GetEulerRotation();
        	
        	// Normalize negative values before displaying
        	eRot.X = (eRot.X + 360) % 360;
        	eRot.Y = (eRot.Y + 360) % 360;
        	eRot.Z = (eRot.Z + 360) % 360;
        
        	((Gtk.Entry) entryMap['x']).Text = eRot.X.ToString();
        	((Gtk.Entry) entryMap['y']).Text = eRot.Y.ToString();
        	((Gtk.Entry) entryMap['z']).Text = eRot.Z.ToString();       	
        }
        
        // The constructor.  Takes as its argument the glObject that it will control (and track)
        // the rotation of
        public GLObjectRotationController(IGLObject glObject) {
        	// Set our member variable to the passed glObject
        	glOb = glObject;
        	      	
        	// Grab the controlWindow widget from the glade xml
			controlXML = new Glade.XML (null, "rotation-controller.glade", "controlWindow", null);
			controlWindow = (Gtk.Window)controlXML["controlWindow"];

        	// Make a map to these widgets
        	entryMap = new System.Collections.Hashtable(3);
        	
        	foreach (char c in new char [] {'x', 'y', 'z'}) {
        		string widgetName = c+"RotEntry";
        		
        		Gtk.Entry e = (Gtk.Entry) controlXML[widgetName];
        		
        		// Align left
        		e.Alignment = 0.0f;
        		
        		// When the user presses enter or tab, update the object's rotation
        		e.Activated += UpdateObjectRotation;
        		e.FocusOutEvent += UpdateObjectRotation;

        		entryMap.Add(c, e);
        	}
			
			// The controlWindow is hidden by default.  Make it visible
			controlWindow.Visible = true;
			
			// Attach the reset button click event to our ResetRotationHandler method
			Gtk.Button resetButton = (Gtk.Button)controlXML["rotationResetButton"];
			resetButton.Clicked += this.ResetRotationHandler;
			
			// Get the table from the glade xml
			Gtk.Table t = (Gtk.Table)controlXML["table1"];
					
			// Create a counter-clockwise rotation (on the X axis) button and place it in the table
			ObjectRotationButton btnXMinus =
				new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button),
										 glOb,
										 new GtkGL.EulerRotation(-1.0, 0.0, 0.0)
										);

			t.Attach(btnXMinus, 2, 3, 0, 1);
			btnXMinus.Rotated += UpdateRotationValues;
			

			// Create a clockwise rotation (on the X axis) button and place it in the table
			ObjectRotationButton btnXPlus =
				new ObjectRotationButton(new Image(Stock.Add, IconSize.Button),
										 glOb,
										 new GtkGL.EulerRotation(1.0, 0.0, 0.0)
										);
				
			t.Attach(btnXPlus, 3, 4, 0, 1);
			btnXPlus.Rotated += UpdateRotationValues;

			// Create a counter-clockwise rotation (on the Y axis) button and place it in the table
			ObjectRotationButton btnYMinus =
				new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button),
										 glOb,
										 new GtkGL.EulerRotation(0.0, -1.0, 0.0)
										);

			t.Attach(btnYMinus, 2, 3, 1, 2);
			btnYMinus.Rotated += UpdateRotationValues;
			
			// Create a clockwise rotation (on the Y axis) button and place it in the table
			ObjectRotationButton btnYPlus =
				new ObjectRotationButton(new Image(Stock.Add, IconSize.Button),
										 glOb,
										 new GtkGL.EulerRotation(0.0, 1.0, 0.0)
										 );
				
			t.Attach(btnYPlus, 3, 4, 1, 2);
			btnYPlus.Rotated += UpdateRotationValues;
			
			// Create a counter-clockwise rotation (on the Z axis) button and place it in the table
			ObjectRotationButton btnZMinus =
				new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button),
										 glOb,
										 new GtkGL.EulerRotation(0.0, 0.0, -1.0)
										);

			t.Attach(btnZMinus, 2, 3, 2, 3);
			btnZMinus.Rotated += UpdateRotationValues;
			
			// Create a clockwise rotation (on the Z axis) button and place it in the table
			ObjectRotationButton btnZPlus =
				new ObjectRotationButton(new Image(Stock.Add, IconSize.Button),
										 glOb,
										 new GtkGL.EulerRotation(0.0, 0.0, 1.0)
										 );
				
			t.Attach(btnZPlus, 3, 4, 2, 3);
			btnZPlus.Rotated += UpdateRotationValues;
			
			// Show the controlWindow and all of its children			
			controlWindow.ShowAll();
        
        }
        
        // The handler for the reset button
		public void ResetRotationHandler(object o, System.EventArgs e)
		{
			eRot.X = eRot.Y = eRot.Z = 0.0f;
			UpdateEntryFields();
			glOb.ResetRotation();
		}

        
    }
    
}
