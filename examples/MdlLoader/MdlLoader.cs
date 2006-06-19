using System;
using System.IO;

// created on 11/26/2005 at 3:33 PM
namespace Mdl {
	
	public class MdlLoader {
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
			
			// Read the header
			mdl_t mdlTStruct = readMdlT(fstream);

			return new Mdl(mdlTStruct,
						   readTexGroupStructs(mdlTStruct, fstream),
						   readVertData(mdlTStruct, fstream),
						   readTriangleData(mdlTStruct, fstream),
						   readFrameData(mdlTStruct, fstream)
						   );
		}
		
		private frame_struct[] readFrameData(mdl_t mdlTStruct, System.IO.FileStream fstream)
		{
			daliasframetype_t frametype;
			frame_struct[] frame = new frame_struct[mdlTStruct.numframes];
			
			for(int frame_num = 0; frame_num < mdlTStruct.numframes; frame_num++){
				frametype.type = (aliasframetype_t) readInt(fstream);
				
				frame[frame_num].type = frametype.type;
				Console.WriteLine("Frame type for frame #{0} is {1}", frame_num, frametype.type);
				
				if(frametype.type == aliasframetype_t.ALIAS_SINGLE){
					//no group data for single-frame meshes
					//we want to use the same data structure, though so set numposes
					frame[frame_num].numposes = 1;
					
					//no interval data for single-frame meshes - set to zero
					frame[frame_num].interval = new float[1];
					frame[frame_num].interval[0] = 0.0f;
				}else{
					//read group data for multi-frame meshes
					daliasgroup_t groupData = readDAliasGroup(fstream);
					frame[frame_num].numposes = groupData.numposes;
					frame[frame_num].interval = new float[groupData.numposes];
					
					for(int i = 0; i < groupData.numposes; i++){
						frame[frame_num].interval[i] = readSingle(fstream);
					}
				}
				
				// Read pose data
				frame[frame_num].pose = new pose_struct[frame[frame_num].numposes];
				
				for(int i = 0; i < frame[frame_num].numposes; i++){
					daliasframe_t dAliasFrame = readDAliasFrame(fstream);
					frame[frame_num].pose[i].name   = dAliasFrame.name;
					Console.WriteLine("Reading pose named {0}", frame[frame_num].pose[i].name);
					frame[frame_num].pose[i].vertex = new trivertx_t[mdlTStruct.numverts];
					
					for(int j = 0; j < mdlTStruct.numverts; j++){
						frame[frame_num].pose[i].vertex[j] = readTriVertX(fstream);
					}
				}
			}
			
			return frame;
		}
		
		private daliasframe_t readDAliasFrame(System.IO.FileStream fstream)
		{
			daliasframe_t dAliasFrame = new daliasframe_t();
			
			dAliasFrame.bboxmax = readTriVertX(fstream); // Not used
			dAliasFrame.bboxmin = readTriVertX(fstream); // Not used
			byte[] buff = new byte[16];
			fstream.Read(buff, 0, 16);
			dAliasFrame.name = System.Text.Encoding.ASCII.GetString(buff);
			
			return dAliasFrame;
		}
		
		private daliasgroup_t readDAliasGroup(System.IO.FileStream fstream)
		{
			daliasgroup_t groupData = new daliasgroup_t();
			
			groupData.numposes = readInt(fstream);
			groupData.bboxmin  = readTriVertX(fstream);
			groupData.bboxmax  = readTriVertX(fstream);
			
			return groupData;
		}
		
		private trivertx_t readTriVertX(System.IO.FileStream fstream)
		{
			trivertx_t triVertData = new trivertx_t();
			
			triVertData.v = new byte[3];
			fstream.Read(triVertData.v, 0, 3);
			byte[] buffer = new byte[1];
			fstream.Read(buffer, 0, 1);
			triVertData.lightnormalindex = buffer[0];
			
			return triVertData;
		}
		
		private dtriangle_t[] readTriangleData(mdl_t mdlTStruct, System.IO.FileStream fstream)
		{
			dtriangle_t[] triData = new dtriangle_t[mdlTStruct.numtris];
			
			for(int tri_num = 0; tri_num < mdlTStruct.numtris; tri_num++){
				triData[tri_num].vertindex = new int[3];
				triData[tri_num].facesfront = readInt(fstream);
				
				for(int i = 0; i < 3; i++){
					triData[tri_num].vertindex[i] = readInt(fstream);
				}
			}
			
			return triData;
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