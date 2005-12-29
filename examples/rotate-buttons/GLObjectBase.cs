namespace GtkGL {
    using System;
    using System.Collections;
    using GtkGL;
    
    abstract class GLObjectBase {
    
        protected ArrayList GLAreaList = null;
        
        protected int shapeID;
        
		abstract public bool Draw();
        
    }
    
}
