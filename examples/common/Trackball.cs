/*
 * (c) Copyright 1993, 1994, Silicon Graphics, Inc.
 * Port to C# (c) Copyright 2005 C.J. Collier <cjcollier@colliertech.org>
 * ALL RIGHTS RESERVED
 * Permission to use, copy, modify, and distribute this software for
 * any purpose and without fee is hereby granted, provided that the above
 * copyright notice appear in all copies and that both the copyright notice
 * and this permission notice appear in supporting documentation, and that
 * the name of Silicon Graphics, Inc. not be used in advertising
 * or publicity pertaining to distribution of the software without specific,
 * written prior permission.
 */
 
/*
        Those parts not created by Silicon Graphics, Inc. are
        Copyright (C) 2005 C.J. Collier <cjcollier@colliertech.org>

        This program is free software; you can redistribute it and/or
        modify it under the terms of the GNU Lesser General Public License
        as published by the Free Software Foundation.

        This program is distributed in the hope that it will be useful,
        but WITHOUT ANY WARRANTY; without even the implied warranty of
        MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

        See the GNU Lesser General Public License for more details.
        http://www.gnu.org/licenses/lgpl.html

        You should have received a copy of the GNU Lesser General Public License
        along with this program; if not, write to:

                Free Software Foundation, Inc.
                59 Temple Place - Suite 330
                Boston, MA  02111-1307, USA

*/

 
using System;

public class Trackball {
	/*
	 * This size should really be based on the distance from the center of
	 * rotation to the point on the object underneath the mouse.  That
	 * point would then track the mouse as closely as possible.  This is a
	 * simple example, though, so that is left as an Exercise for the
	 * Programmer.
	 */
	const float TRACKBALLSIZE = 0.8f;

	/*
 	 * Pass the x and y coordinates of the last and current positions of
 	 * the mouse, scaled so they are from (-1.0 ... 1.0).
 	 *
 	 * The resulting rotation is returned as a quaternion rotation in the
 	 * first paramater.
 	 */
 	public void trackball(ref float[] q, float p1x, float p1y, float p2x, float p2y)
 	{
	    float[] a = new float[3]; /* Axis of rotation */
    	float phi;  /* how much to rotate about axis */
    	float[] p1 = new float[3], p2 = new float[3], d = new float[3];
    	float t;

    	if (p1x == p2x && p1y == p2y) {
	        /* Zero rotation */
        	vzero(ref q);
        	q[3] = 1.0f;
        	return;
    	}

    	/*
     	 * First, figure out z-coordinates for projection of P1 and P2 to
     	 * deformed sphere
     	 */
	    vset(ref p1,p1x,p1y,tb_project_to_sphere(TRACKBALLSIZE,p1x,p1y));
    	vset(ref p2,p2x,p2y,tb_project_to_sphere(TRACKBALLSIZE,p2x,p2y));

    	/*
     	 *  Now, we want the cross product of P1 and P2
     	 */
    	vcross(p2,p1, ref a);

    	/*
     	 *  Figure out how much to rotate around that axis.
	     */
	    vsub(p1,p2, ref d);
	    t = vlength(d) / (2.0f*TRACKBALLSIZE);

	    /*
	     * Avoid problems with out-of-control values...
	     */
	    if (t > 1.0f) t = 1.0f;
	    if (t < -1.0f) t = -1.0f;
	    phi = 2.0f * (float)System.Math.Asin(t);
	
	    axis_to_quat(a,phi, ref q);
	}

 	/*
	 * Given two rotations, q1 and q2, expressed as quaternion rotations,
	 * figure out the equivalent single rotation and stuff it into dest.
	 * Adding quaternions to get a compound rotation is analagous to adding
 	 * translations to get a compound translation.  When incrementally
 	 * adding rotations, the first argument here should be the new
	 * rotation, the second and third the total rotation (which will be
 	 * over-written with the resulting new total rotation).
 	 *
	 * This routine also normalizes the result every RENORMCOUNT times it is
	 * called, to keep error from creeping in.
	 *
	 * NOTE: This routine is written so that q1 or q2 may be the same
	 * as dest (or each other).
	 */

	const int RENORMCOUNT = 97;
 	static int count=0;
	public void add_quats(float[] q1, float[] q2, ref float[] dest)
	{
	    float[] t1 = new float[4], t2 = new float[4], t3 = new float[4];
	    float[] tf = new float[4];

	    vcopy(q1,ref t1);
    	vscale(ref t1,q2[3]);

    	vcopy(q2,ref t2);
	    vscale(ref t2,q1[3]);

	    vcross(q2,q1,ref t3);
	    vadd(t1,t2,ref tf);
	    vadd(t3,tf,ref tf);
	    tf[3] = q1[3] * q2[3] - vdot(q1,q2);

	    dest[0] = tf[0];
	    dest[1] = tf[1];
	    dest[2] = tf[2];
	    dest[3] = tf[3];
	    
	    if (++count > RENORMCOUNT) {
        	count = 0;
        	normalize_quat(ref dest);
    	}
	}
	
	/*
 	 * A useful function, builds a rotation matrix in Matrix based on
 	 * given quaternion.
 	 */
	public void build_rotmatrix(ref float[] m, float[] q)
	{
	    m[4 * 0 + 0] = 1.0f - 2.0f * (q[1] * q[1] + q[2] * q[2]);
	    m[4 * 0 + 1] = 2.0f * (q[0] * q[1] - q[2] * q[3]);
	    m[4 * 0 + 2] = 2.0f * (q[2] * q[0] + q[1] * q[3]);
	    m[4 * 0 + 3] = 0.0f;

	    m[4 * 1 + 0] = 2.0f * (q[0] * q[1] + q[2] * q[3]);
	    m[4 * 1 + 1]= 1.0f - 2.0f * (q[2] * q[2] + q[0] * q[0]);
	    m[4 * 1 + 2] = 2.0f * (q[1] * q[2] - q[0] * q[3]);
	    m[4 * 1 + 3] = 0.0f;

	    m[4 * 2 + 0] = 2.0f * (q[2] * q[0] - q[1] * q[3]);
    	m[4 * 2 + 1] = 2.0f * (q[1] * q[2] + q[0] * q[3]);
	    m[4 * 2 + 2] = 1.0f - 2.0f * (q[1] * q[1] + q[0] * q[0]);
	    m[4 * 2 + 3] = 0.0f;

    	m[4 * 3 + 0] = 0.0f;
	    m[4 * 3 + 1] = 0.0f;
    	m[4 * 3 + 2] = 0.0f;
	    m[4 * 3 + 3] = 1.0f;
	}

	/*
 	 * This function computes a quaternion based on an axis (defined by
 	 * the given vector) and an angle about which to rotate.  The angle is
 	 * expressed in radians.  The result is put into the third argument.
 	 */
	public void axis_to_quat(float[] a, float phi, ref float[] q)
	{
	    vnormal(ref a);
	    vcopy(a,ref q);
	    vscale(ref q,(float)Math.Sin(phi/2.0f));
	    q[3] = (float)Math.Cos(phi/2.0f);
	}
	
	/*
 	 * Project an x,y pair onto a sphere of radius r OR a hyperbolic sheet
 	 * if we are away from the center of the sphere.
 	 */
	private static float tb_project_to_sphere(float r, float x, float y)
	{
	    float d, t, z;

	    d = (float)Math.Sqrt(x*x + y*y);
	    if (d < r * 0.70710678118654752440) {    /* Inside sphere */
	        z = (float)Math.Sqrt(r*r - d*d);
    	} else {           /* On hyperbola */
	        t = r / 1.41421356237309504880f;
        	z = t*t / d;
	    }
	    return z;
	}
	
	/*
 	 * Quaternions always obey:  a^2 + b^2 + c^2 + d^2 = 1.0
 	 * If they don't add up to 1.0, dividing by their magnitued will
 	 * renormalize them.
 	 *
 	 * Note: See the following for more information on quaternions:
 	 *
 	 * - Shoemake, K., Animating rotation with quaternion curves, Computer
 	 *   Graphics 19, No 3 (Proc. SIGGRAPH'85), 245-254, 1985.
 	 * - Pletinckx, D., Quaternion calculus as a basic tool in computer
 	 *   graphics, The Visual Computer 5, 2-13, 1989.
 	 */
	private static void normalize_quat(ref float[] q)
	{
	    int i;
	    float mag;

	    mag = (q[0]*q[0] + q[1]*q[1] + q[2]*q[2] + q[3]*q[3]);
	    for (i = 0; i < 4; i++) q[i] /= mag;
	}

	/*
	 * Zero a vector
	 */	 
	private void vzero(ref float[] v)
	{
    	v[0] = 0.0f;
    	v[1] = 0.0f;
    	v[2] = 0.0f;
	}

	/*
	 * Set a vector's scalar values
	 */
	private void vset(ref float[] v, float x, float y, float z)
	{
	    v[0] = x;
	    v[1] = y;
	    v[2] = z;
	}
	
	/*
	 * Subtract one vector from another
	 */
	private void vsub(float[] src1, float[] src2, ref float[] dst)
	{
	    dst[0] = src1[0] - src2[0];
	    dst[1] = src1[1] - src2[1];
	    dst[2] = src1[2] - src2[2];
	}
	
	/*
	 * Copy a vector
	 */
	private void vcopy(float[] v1, ref float[] v2)
	{
	    int i;
	    for (i = 0 ; i < 3 ; i++)
        	v2[i] = v1[i];
	}
	
	/*
	 * Cross-multiply two vectors
	 */
	private void vcross(float[] v1, float[] v2, ref float[] cross)
	{
	    float[] temp = new float[3];

	    temp[0] = (v1[1] * v2[2]) - (v1[2] * v2[1]);
	    temp[1] = (v1[2] * v2[0]) - (v1[0] * v2[2]);
	    temp[2] = (v1[0] * v2[1]) - (v1[1] * v2[0]);
	    vcopy(temp, ref cross);
	}

	/*
	 * Find the length of a vector
	 */
	private float vlength(float[] v)
	{
	    return (float)Math.Sqrt(v[0] * v[0] + v[1] * v[1] + v[2] * v[2]);
	}
	
	/*
	 * Scale a vector
	 */
	private void vscale(ref float[] v, float div)
	{
	    v[0] *= div;
	    v[1] *= div;
	    v[2] *= div;
	}
	
	/*
	 * Normalize a vector
	 */
	private void vnormal(ref float[] v)
	{
	    vscale(ref v,1.0f/vlength(v));
	}
	
	/*
	 * Find dot product of a vector
	 */
	private float vdot(float[] v1, float[] v2)
	{
	    return v1[0]*v2[0] + v1[1]*v2[1] + v1[2]*v2[2];
	}
	
	/*
	 * Add two vectors
	 */
	private void vadd(float[] src1, float[] src2, ref float[] dst)
	{
	    dst[0] = src1[0] + src2[0];
	    dst[1] = src1[1] + src2[1];
	    dst[2] = src1[2] + src2[2];
	}
}