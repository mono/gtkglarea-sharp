/*
	Copyright (C) 2001 C.J. Collier (cjcollier@sinclair.net)

	This program is free software; you can redistribute it and/or
	modify it under the terms of the GNU General Public License
	as published by the Free Software Foundation; either version 2
	of the License, or (at your option) any later version.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  

	See the GNU General Public License for more details.

	You should have received a copy of the GNU General Public License
	along with this program; if not, write to:
	
		Free Software Foundation, Inc.
		59 Temple Place - Suite 330
		Boston, MA  02111-1307, USA

*/

#include "mdl.h"

extern int errno;

mdl_struct *
load_mdl(const char *filename)
{
  mdl_struct *mdl;
  FILE *f;
  int texgroup_num, tex_num, frame_num, pose_num;
  daliasframetype_t frametype;
  daliasgroup_t group_data;
  daliasframe_t frame_data;
  

  if( (f = fopen(filename, "r")) == NULL){
    printf("Open failure: %s\n", strerror(errno));
    return NULL;
  }

  mdl = malloc(sizeof(mdl_struct));

  //load the header
  fread( &(mdl->header), sizeof(mdl_t), 1, f);

  //allocate memory for our textures
  mdl->texgroup = malloc(sizeof(texgroup_struct) * mdl->header.num_texgroups);

  for(texgroup_num = 0; texgroup_num < mdl->header.num_texgroups; texgroup_num++){

    //read data into texgroup->type
    fread( &mdl->texgroup->type, sizeof(aliasskintype_t), 1, f);

    //read texture(s)
    if(mdl->texgroup->type == ALIAS_SKIN_SINGLE){
      //if type is ALIAS_SKIN_SINGLE, set number of textures to 1
      mdl->texgroup[texgroup_num].numtextures = 1;
      //and interval[0] to 0
      mdl->texgroup[texgroup_num].interval = malloc(sizeof(float));
      mdl->texgroup[texgroup_num].interval[0] = 0.0;
    }else{
      //read data into tex->numtextures
      fread( &mdl->texgroup[texgroup_num].numtextures, sizeof(long), 1, f);

      //read interval data
      mdl->texgroup[texgroup_num].interval =
	malloc(sizeof(float) * mdl->texgroup[texgroup_num].numtextures);
      fread( mdl->texgroup[texgroup_num].interval,
	     sizeof(float),
	     mdl->texgroup[texgroup_num].numtextures,
	     f);
    }    
    
    //allocate space for the texture data...
    mdl->texgroup[texgroup_num].texture =
      malloc(sizeof(tex_struct) * mdl->texgroup[texgroup_num].numtextures);

    for(tex_num = 0;
	tex_num < mdl->texgroup[texgroup_num].numtextures;
	tex_num++
	){
      //allocate space for the pixel data
      mdl->texgroup[texgroup_num].texture[tex_num].pixel =
	malloc(sizeof(guint8) *
	       mdl->header.texwidth * mdl->header.texheight);

      //read the pixel data
      fread(mdl->texgroup[texgroup_num].texture[tex_num].pixel,
	    sizeof(guint8),
	    mdl->header.texwidth * mdl->header.texheight,
	    f
	    );
    }
    
  
  }

  //read the texture vertices
  mdl->stvert_data = malloc(sizeof(stvert_t) * mdl->header.numverts);
  fread( mdl->stvert_data, sizeof(stvert_t), mdl->header.numverts, f);

  //read the triangle indices
  mdl->triangle_data = malloc(sizeof(dtriangle_t) * mdl->header.numtris);
  fread( mdl->triangle_data, sizeof(dtriangle_t), mdl->header.numtris, f);

  //allocate enough memory for our frames
  mdl->frame = malloc(sizeof(frame_struct) * mdl->header.numframes);

  for(frame_num = 0; frame_num < mdl->header.numframes; frame_num++){
    //read the frame type data
    fread( &frametype, sizeof(daliasframetype_t), 1, f);

    if((mdl->frame[frame_num].type = frametype.type) == ALIAS_SINGLE){
      //no group data for single-frame meshes
      //we want to use the same data structure, though so set numposes
      mdl->frame[frame_num].numposes = 1;

      //no interval data for single-frame meshes - set to zero
      mdl->frame[frame_num].interval = malloc(sizeof(float));
      mdl->frame[frame_num].interval[0] = 0.0;
    }else{
      //read group data for multi-frame meshes
      fread( &group_data, sizeof(daliasgroup_t), 1, f);

      //we only need the number of poses.
      mdl->frame[frame_num].numposes = group_data.numposes;

      //read interval data
      mdl->frame[frame_num].interval =
	malloc(sizeof(float) * mdl->frame[frame_num].numposes);
      fread(&(mdl->frame[frame_num].interval),
	    sizeof(float),
	    mdl->frame[frame_num].numposes,
	    f);
    }

    //allocate enough memory for our poses
    mdl->frame[frame_num].pose = malloc(sizeof(pose_struct) * mdl->frame[frame_num].numposes);    
    
    //read frame(s)
    for(pose_num = 0; pose_num < mdl->frame[frame_num].numposes; pose_num++){

      //read frame data from disk
      fread( &frame_data, sizeof(daliasframe_t), 1, f);
      //copy the name of the pose to our struct
      strncpy( &mdl->frame[frame_num].pose[pose_num].name[0], &frame_data.name[0], 16);
      /*
      printf("(load_mdl.c) phrase: %i, pose: %i, name: %s\n",
	     frame_num,
	     pose_num,
	     mdl->frame[frame_num].pose[pose_num].name);
      */
      //read vertex data
      mdl->frame[frame_num].pose[pose_num].vertex = malloc(sizeof(trivertx_t) * mdl->header.numverts);
      fread( mdl->frame[frame_num].pose[pose_num].vertex, sizeof(trivertx_t), mdl->header.numverts, f);
    }
  }

  return mdl;
}

/*

int
main(int argc, char *argv[])
{
  mdl_struct *mdl = load_mdl("/home/cjcollier/tmp/quake/progs/armor.mdl");

  printf("number of frames: %i\n", mdl->header.numframes);
  printf("Number of poses in first frame: %i\n", mdl->frame[0].numposes);
  return 1;
}

*/
