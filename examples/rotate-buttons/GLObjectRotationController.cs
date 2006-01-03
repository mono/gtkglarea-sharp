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
        	GtkGL.Rotation rotation = ( (GtkGL.ObjectRotationButton)o ).rotation;
        	int direction = -1;
        	
        	if(rotation.dir == Rotation.Direction.Clockwise){
        		direction = 1;
        	}
        	
        	eRot.x += (direction * rotation.xRot);
        	eRot.y += (direction * rotation.yRot);
        	eRot.z += (direction * rotation.zRot);
        	
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
        	
        	// First on the X axis, don't fire the Update handlers
        	Rotation tmpRot = new Rotation(Rotation.Direction.Clockwise, 1.0f, 0.0f, 0.0f);
        	eRot.x = (float) Convert.ToSingle(((Gtk.Entry)entryMap['x']).Text.ToString());
        	glOb.Rotate(eRot.x, tmpRot, false);
        	
        	// Then on the Y axis, don't fire the Update handlers
        	tmpRot = new Rotation(Rotation.Direction.Clockwise, 0.0f, 1.0f, 0.0f);
        	eRot.y = (float) Convert.ToSingle(((Gtk.Entry)entryMap['y']).Text.ToString());
        	glOb.Rotate(eRot.y, tmpRot, false);
        	
        	// Then on the Z axis.  This time, fire the Update handlers
        	tmpRot = new Rotation(Rotation.Direction.Clockwise, 0.0f, 0.0f, 1.0f);
        	eRot.z = (float) Convert.ToSingle(((Gtk.Entry)entryMap['z']).Text.ToString());
        	glOb.Rotate(eRot.z, tmpRot);
        	
        }
        
        // This method updates the values stored in the X, Y and Z entry fields.
        // It is used more than once, so it gets its own handle :)
        public void UpdateEntryFields()
        {
        	// Normalize negative values before displaying
        	eRot.x = (eRot.x + 360) % 360;
        	eRot.y = (eRot.y + 360) % 360;
        	eRot.z = (eRot.z + 360) % 360;
        
        	((Gtk.Entry) entryMap['x']).Text = eRot.x.ToString();
        	((Gtk.Entry) entryMap['y']).Text = eRot.y.ToString();
        	((Gtk.Entry) entryMap['z']).Text = eRot.z.ToString();       	
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
										 new GtkGL.Rotation(Rotation.Direction.CounterClockwise, 1.0f, 0.0f, 0.0f)
										);

			t.Attach(btnXMinus, 2, 3, 0, 1);
			btnXMinus.Rotated += UpdateRotationValues;
			

			// Create a clockwise rotation (on the X axis) button and place it in the table
			ObjectRotationButton btnXPlus =
				new ObjectRotationButton(new Image(Stock.Add, IconSize.Button),
										 glOb,
										 new GtkGL.Rotation(Rotation.Direction.Clockwise, 1.0f, 0.0f, 0.0f)
										);
				
			t.Attach(btnXPlus, 3, 4, 0, 1);
			btnXPlus.Rotated += UpdateRotationValues;

			// Create a counter-clockwise rotation (on the Y axis) button and place it in the table
			ObjectRotationButton btnYMinus =
				new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button),
										 glOb,
										 new GtkGL.Rotation(Rotation.Direction.CounterClockwise, 0.0f, 1.0f, 0.0f)
										);

			t.Attach(btnYMinus, 2, 3, 1, 2);
			btnYMinus.Rotated += UpdateRotationValues;
			
			// Create a clockwise rotation (on the Y axis) button and place it in the table
			ObjectRotationButton btnYPlus =
				new ObjectRotationButton(new Image(Stock.Add, IconSize.Button),
										 glOb,
										 new GtkGL.Rotation(Rotation.Direction.Clockwise, 0.0f, 1.0f, 0.0f)
										 );
				
			t.Attach(btnYPlus, 3, 4, 1, 2);
			btnYPlus.Rotated += UpdateRotationValues;
			
			// Create a counter-clockwise rotation (on the Z axis) button and place it in the table
			ObjectRotationButton btnZMinus =
				new ObjectRotationButton(new Image(Stock.Remove, IconSize.Button),
										 glOb,
										 new GtkGL.Rotation(Rotation.Direction.CounterClockwise, 0.0f, 0.0f, 1.0f)
										);

			t.Attach(btnZMinus, 2, 3, 2, 3);
			btnZMinus.Rotated += UpdateRotationValues;
			
			// Create a clockwise rotation (on the Z axis) button and place it in the table
			ObjectRotationButton btnZPlus =
				new ObjectRotationButton(new Image(Stock.Add, IconSize.Button),
										 glOb,
										 new GtkGL.Rotation(Rotation.Direction.Clockwise, 0.0f, 0.0f, 1.0f)
										 );
				
			t.Attach(btnZPlus, 3, 4, 2, 3);
			btnZPlus.Rotated += UpdateRotationValues;
			
			// Show the controlWindow and all of its children			
			controlWindow.ShowAll();
        
        }
        
        // The handler for the reset button
		public void ResetRotationHandler(object o, System.EventArgs e)
		{
			eRot.x = eRot.y = eRot.z = 0.0f;
			UpdateEntryFields();
			glOb.ResetRotation();
		}

        
    }
    
}
