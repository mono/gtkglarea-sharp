// created on 6/19/2006 at 1:44 PM
namespace Mdl {
	public enum synctype_t {
		ST_SYNC = 0,
		ST_RAND
	};
	
	public enum aliasframetype_t {
		ALIAS_SINGLE = 0,
		ALIAS_GROUP
	};
	
	public enum aliasskintype_t {
		ALIAS_SKIN_SINGLE = 0,
		ALIAS_SKIN_GROUP
	};
	
	public struct mdl_t {
	
		public int         ident;
        public int         version;
        public float[]     scale;
        public float[]     scale_origin;
        public float       boundingradius;
        public float[]     eyeposition;
        public int         num_texgroups;
        public int         texwidth;
        public int         texheight;
        public int         numverts;
        public int         numtris;
        public int         numframes;
        public synctype_t  synctype;
        public int         flags;
        public float       size;
	};
	
	public struct stvert_t {
        public int         onseam;
        public int         s;
        public int         t;
	};

	public struct dtriangle_t {
        public int         facesfront;
        public int[]       vertindex;
	};
	
	public struct trivertx_t {
        public byte[]		v;
        public byte         lightnormalindex;
	};

	public struct daliasframe_t {
        public trivertx_t  bboxmin;                            // lightnormal isn't used
        public trivertx_t  bboxmax;                            // lightnormal isn't used
        public string      name;                               // frame name from grabbing
	};
	
	public struct daliasgroup_t {
        public int         numposes;
        public trivertx_t  bboxmin;                            // lightnormal isn't used
        public trivertx_t  bboxmax;                            // lightnormal isn't used
	};
	
	public struct daliasframetype_t {
        public aliasframetype_t type;
	};

	public struct tex_struct {
		public byte[] pixel;  //Pixel data
	};

	public struct texgroup_struct {
		public aliasskintype_t type;

		public long			numtextures; // number of pictures in group
		public float[]			interval;   // intervals for each picture
		public tex_struct[]	texture;    // texture data
	};

	public struct pose_struct {
  		public string       name;
  		public trivertx_t[] vertex;
	};

	public struct frame_struct {
  		public aliasframetype_t  type;
  		public int               numposes;
  		public float[]           interval;
  		public pose_struct[]     pose;
	};
}