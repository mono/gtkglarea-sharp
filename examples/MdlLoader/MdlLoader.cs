// created on 11/26/2005 at 3:33 PM
namespace Mdl {
	enum synctype_t {
		ST_SYNC = 0,
		ST_RAND
	};
	
	enum aliasframetype_t {
		ALIAS_SINGLE = 0,
		ALIAS_GROUP
	};
	
	enum aliasskintype_t {
		ALIAS_SKIN_SINGLE = 0,
		ALIAS_SKIN_GROUP
	};
	
	
	
	struct mdl_t {
		int         ident;
        int         version;
        float[]     scale;
        float[]     scale_origin;
        float       boundingradius;
        float[]     eyeposition;
        int         num_texgroups;
        int         texwidth;
        int         texheight;
        int         numverts;
        int         numtris;
        int         numframes;
        synctype_t  synctype;
        int         flags;
        float       size;
	};
	
	struct stvert_t {
        int         onseam;
        int         s;
        int         t;
	};

	struct dtriangle_t {
        int         facesfront;
        int[]       vertindex;
	};
	
	struct trivertx_t {
        short[]		v;
        short       lightnormalindex;
	};

	struct daliasframe_t {
        trivertx_t  bboxmin;                            // lightnormal isn't used
        trivertx_t  bboxmax;                            // lightnormal isn't used
        char[]      name;                           // frame name from grabbing
	};
	
	struct daliasgroup_t {
        int         numposes;
        trivertx_t  bboxmin;                            // lightnormal isn't used
        trivertx_t  bboxmax;                            // lightnormal isn't used
	};
	
	struct daliasframetype_t {
        aliasframetype_t type;
	};

	struct tex_struct {
		int[] pixel;  //Pixel data
	};

	struct texgroup_struct {
		aliasskintype_t type;

		long			numtextures; // number of pictures in group
		float[]			interval;   // intervals for each picture
		tex_struct[]	texture;    // texture data
	};

	struct pose_struct {
  		string       name;
  		trivertx_t[] vertex;
	};

	struct frame_struct {
  		aliasframetype_t  type;
  		int               numposes;
  		float[]           interval;
  		pose_struct[]     pose;
	};

	struct mdl_struct {
  		mdl_t             header;

  		texgroup_struct[] texgroup;
  		stvert_t[]        stvert_data;
  		dtriangle_t[]     triangle_data;
  		frame_struct[]    frame;
	};

	class MdlLoader {
		const int DT_FACES_FRONT = 0x0010;
		
		
		
	}
}