
using System;

namespace Mdl
{
	
	public class Mdl {
		private mdl_t header;
		private texgroup_struct[] texGroup;
		private stvert_t[] vertex;
		private dtriangle_t[] triangle;
		private frame_struct[] frame;
		
		public Mdl(mdl_t mdlTStruct,
				   texgroup_struct[] tgs,
				   stvert_t[] vertData,
				   dtriangle_t[] triData,
				   frame_struct[] frame
				   )
		{
			this.header = mdlTStruct;
			this.texGroup = tgs;
			this.vertex = vertData;
			this.triangle = triData;
			this.frame = frame;
		}
	}

	
}
