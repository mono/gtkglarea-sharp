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

#ifndef _LOAD_MDL_H_
#define _LOAD_MDL_H_

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <glib.h>

typedef enum { ALIAS_SINGLE = 0, ALIAS_GROUP } aliasframetype_t;

typedef enum { ALIAS_SKIN_SINGLE = 0, ALIAS_SKIN_GROUP } aliasskintype_t;

typedef enum { ST_SYNC = 0, ST_RAND } synctype_t;

typedef unsigned char Uint8;

typedef struct {
	int         ident;
	int         version;
	float       scale[3];
	float       scale_origin[3];
	float       boundingradius;
	float       eyeposition[3];
	int         num_texgroups;
	int         texwidth;
	int         texheight;
	int         numverts; 
	int         numtris;
	int         numframes;
	synctype_t  synctype;
	int         flags;
	float       size;
} mdl_t;

typedef struct {
	int         onseam;
	int         s;
	int         t;
} stvert_t;

typedef struct dtriangle_s {
	int         facesfront;
	int         vertindex[3];
} dtriangle_t;

#define DT_FACES_FRONT				0x0010

// This mirrors trivert_t in trilib.h, is present so Quake knows how to
// load this data

typedef struct {
	Uint8       v[3];
	Uint8       lightnormalindex;
} trivertx_t;

typedef struct {
	trivertx_t  bboxmin;				// lightnormal isn't used
	trivertx_t  bboxmax;				// lightnormal isn't used
	char        name[16];				// frame name from grabbing
} daliasframe_t;

typedef struct {
	int         numposes;
	trivertx_t  bboxmin;				// lightnormal isn't used
	trivertx_t  bboxmax;				// lightnormal isn't used
} daliasgroup_t;

typedef struct {
	aliasframetype_t type;
} daliasframetype_t;

typedef struct {
  guint8 *pixel;  //Pixel data
} tex_struct;

typedef struct {
  aliasskintype_t type;

  long        numtextures; // number of pictures in group
  float       *interval;   // intervals for each picture
  tex_struct  *texture;    // texture data
} texgroup_struct;

typedef struct {
  char       name[16];
  trivertx_t *vertex;  
} pose_struct;

typedef struct {
  aliasframetype_t  type;
  int               numposes;
  float             *interval;
  pose_struct       *pose;
} frame_struct;

typedef struct {
  mdl_t             header;

  texgroup_struct   *texgroup;
  stvert_t          *stvert_data;
  dtriangle_t       *triangle_data;
  frame_struct      *frame;
} mdl_struct;

mdl_struct *
load_mdl(const char *filename);

#endif /* _LOAD_MDL_H_ */
