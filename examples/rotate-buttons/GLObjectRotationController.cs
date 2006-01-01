namespace GtkGL {
    using System;
	using Gtk;
    
    public class GLObjectRotationController {
    
    	Glade.XML controlXML;
    	Gtk.Window controlWindow;
		GtkGL.IGLObject glOb;    	
        System.Collections.Hashtable entryMap;
        EulerRotation eRot;
        
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
        
        public void UpdateEntryFields()
        {
        	// Normalize the values before displaying
        	eRot.x = (eRot.x + 360) % 360;
        	eRot.y = (eRot.y + 360) % 360;
        	eRot.z = (eRot.z + 360) % 360;
        
        	((Gtk.Entry) entryMap['x']).Text = eRot.x.ToString();
        	((Gtk.Entry) entryMap['y']).Text = eRot.y.ToString();
        	((Gtk.Entry) entryMap['z']).Text = eRot.z.ToString();       	
        }
        
        public GLObjectRotationController(IGLObject glObject) {
        	// Set our member variable to the passed glObject
        	glOb = glObject;
        	
        	// Update the Rotation values when the glOb is updated
        	// glOb.Updated += this.UpdateRotationValues;
        	
        	// Grab the controlWindow widget from the glade xml
			controlXML = new Glade.XML (null, "rotation-controller.glade", "controlWindow", null);
			controlWindow = (Gtk.Window)controlXML["controlWindow"];

        	// Make a map to these widgets
        	entryMap = new System.Collections.Hashtable(3);
        	
        	foreach (char c in new char [] {'x', 'y', 'z'}) {
        		string widgetName = c+"RotEntry";
        		
        		Gtk.Entry e = (Gtk.Entry) controlXML[widgetName];

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
