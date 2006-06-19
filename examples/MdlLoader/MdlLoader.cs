using System;
using System.IO;

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
	
	struct stvert_t {
        public int         onseam;
        public int         s;
        public int         t;
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
		public byte[] pixel;  //Pixel data
	};

	struct texgroup_struct {
		public aliasskintype_t type;

		public long			numtextures; // number of pictures in group
		public float[]			interval;   // intervals for each picture
		public tex_struct[]	texture;    // texture data
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
	
	class Mdl {
		public Mdl() { }
	}

	class MdlLoader {
		const int DT_FACES_FRONT = 0x0010;
		
		public MdlLoader() {
		}

		public Mdl Load(string fileName) {
			System.IO.FileStream fstream;
			
			try {
				fstream = File.Open(fileName, System.IO.FileMode.Open);
				
			} catch(IOException e) {
				System.Console.WriteLine("Oops.  We got an exception: {0}", e.ToString());
				return null;
			}

			mdl_t mdlTStruct      = readMdlT(fstream);
			texgroup_struct[] tgs = readTexGroupStructs(mdlTStruct, fstream);
			stvert_t[] vertData   = readVertData(mdlTStruct, fstream);			
			
			Mdl myMdl  = new Mdl();
			
			return myMdl;
		}
		
		private stvert_t[] readVertData(mdl_t mdlTStruct, System.IO.FileStream fstream)
		{
			stvert_t[] vertData = new stvert_t[mdlTStruct.numverts];
			for(int vert_num = 0; vert_num < mdlTStruct.numverts; vert_num++)
			{
				vertData[vert_num].onseam = readInt(fstream);
				vertData[vert_num].s      = readInt(fstream);
				vertData[vert_num].t      = readInt(fstream);
			}
			
			return vertData;
		}
		
		private texgroup_struct[] readTexGroupStructs(mdl_t mdlTStruct, System.IO.FileStream fstream)
		{
			texgroup_struct[] tgs = new texgroup_struct[ mdlTStruct.num_texgroups ];
			
			for(int texgroup_num = 0; texgroup_num < mdlTStruct.num_texgroups; texgroup_num++){
				tgs[texgroup_num].type = (aliasskintype_t) readInt(fstream);
				Console.WriteLine("Type for tgs[{0}] is {1}", texgroup_num, tgs[texgroup_num].type);
				
				if(tgs[texgroup_num].type == aliasskintype_t.ALIAS_SKIN_SINGLE){
					//if type is ALIAS_SKIN_SINGLE, set number of textures to 1
					tgs[texgroup_num].numtextures = 1;
					
					//and interval[0] to 0
					tgs[texgroup_num].interval = new float[1];
					tgs[texgroup_num].interval[0] = 0.0f;
				}else{
					// Determine how many textures there are in this group
					tgs[texgroup_num].numtextures = readInt(fstream);
					Console.WriteLine("numtextures for tgs[{0}] is {1}", texgroup_num, tgs[texgroup_num].numtextures);
					tgs[texgroup_num].interval = new float[tgs[texgroup_num].numtextures];
					
					// Read the intervals between each texture
					for(int i = 0; i < tgs[texgroup_num].numtextures; i++){
						tgs[texgroup_num].interval[i] = readSingle(fstream);
					}
				}
				
				// Read texture data
				tgs[texgroup_num].texture = new tex_struct[tgs[texgroup_num].numtextures];
				
				for(int tex_num = 0; tex_num < tgs[texgroup_num].numtextures; tex_num++){
					tgs[texgroup_num].texture[tex_num].pixel = new byte[mdlTStruct.texheight * mdlTStruct.texwidth];
					fstream.Read(tgs[texgroup_num].texture[tex_num].pixel, 0, mdlTStruct.texheight * mdlTStruct.texwidth);
				}
			}
			
			return tgs;
		}
		
		private mdl_t readMdlT(System.IO.FileStream fstream)
		{
			mdl_t mdlTStruct = new mdl_t();
			
			mdlTStruct.ident = readInt(fstream);
			Console.WriteLine("Ident is {0}", mdlTStruct.ident);
			
			mdlTStruct.version = readInt(fstream);
			Console.WriteLine("Version is {0}", mdlTStruct.version);
						
			mdlTStruct.scale = new float[3];
			
			for(int i = 0; i <= 2; i++){
				mdlTStruct.scale[i] = readSingle(fstream);
				Console.WriteLine("Scale[{0}]: {1}", i, mdlTStruct.scale[i]);
			}
			
			mdlTStruct.scale_origin = new float[3];
			for(int i = 0; i <= 2; i++){
				mdlTStruct.scale_origin[i] = readSingle(fstream);
				Console.WriteLine("scale_origin[{0}]: {1}", i, mdlTStruct.scale_origin[i]);
			}
			
			mdlTStruct.boundingradius = readSingle(fstream);
			Console.WriteLine("bounding radius is {0}", mdlTStruct.boundingradius);
			
			mdlTStruct.eyeposition = new float[3];
			for(int i = 0; i <= 2; i++){
				mdlTStruct.eyeposition[i] = readSingle(fstream);
				Console.WriteLine("eyeposition[{0}]: {1}", i, mdlTStruct.eyeposition[i]);
			}
			
			mdlTStruct.num_texgroups = readInt(fstream);
			Console.WriteLine("Number of texgroups: {0}", mdlTStruct.num_texgroups);

			mdlTStruct.texwidth = readInt(fstream);
			Console.WriteLine("texture width: {0}", mdlTStruct.texwidth);

			mdlTStruct.texheight = readInt(fstream);
			Console.WriteLine("texture height: {0}", mdlTStruct.texheight);

			mdlTStruct.numverts = readInt(fstream);
			Console.WriteLine("numverts: {0}", mdlTStruct.numverts);

			mdlTStruct.numtris = readInt(fstream);
			Console.WriteLine("numtris: {0}", mdlTStruct.numtris);

			mdlTStruct.numframes = readInt(fstream);
			Console.WriteLine("numframes: {0}", mdlTStruct.numframes);
			
			mdlTStruct.synctype = (synctype_t) readInt(fstream);
			Console.WriteLine("synctype: {0}", mdlTStruct.synctype);

			mdlTStruct.flags = readInt(fstream);
			Console.WriteLine("flags: {0}", mdlTStruct.flags);
			
			mdlTStruct.size = readSingle(fstream);
			Console.WriteLine("size: {0}", mdlTStruct.size);
			
			return mdlTStruct;
		}
		
		private float readSingle(System.IO.FileStream fstream)
		{
			byte[] buff = new byte[4];
			fstream.Read(buff, 0, 4);
			return System.BitConverter.ToSingle(buff, 0);
		}
		
		private int readInt(System.IO.FileStream fstream)
		{
			byte[] buff = new byte[4];
			fstream.Read(buff, 0, 4);
			return System.BitConverter.ToInt32(buff, 0);
		}
	}
}