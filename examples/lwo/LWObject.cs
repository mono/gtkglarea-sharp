
namespace LWObjectExample {
    using System;
    
    public class LWObject {
    
		struct lwMaterial{
		  char[] name;
		  float r,g,b;
		}

		struct lwFace {
		  int material;         /* material of this face */
		  int index_cnt;        /* number of vertices */
		  int index;           /* index to vertex */
		  float[] texcoord;      /* u,v texture coordinates */
		}

		struct lwObject {
		  lwFace[] face;
		  lwMaterial material;
		  float[] vertex;

		}


		gint      lw_is_lwobject(const char     *lw_file);
		lwObject *lw_object_read(const char     *lw_file);
		void      lw_object_free(      lwObject *lw_object);
		void      lw_object_show(const lwObject *lw_object);

		GLfloat   lw_object_radius(const lwObject *lw_object);
		void      lw_object_scale (lwObject *lw_object, GLfloat scale);
    
    
    
        private LWObject() {
        }
    }
    
}
