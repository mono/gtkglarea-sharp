SUBDIRS = gtkglarea sources

if HAS_TAO_OPENGL
SUBDIRS += examples
endif

pkgconfigdir = $(prefix)/lib/pkgconfig
pkgconfig_DATA = gtkglarea-sharp.pc

EXTRA_DIST = \
	Makefile.in \
	gtkglarea-sharp.pc.in \
	README \
	AUTHORS \
	COPYING \
	license \
	x11_license \
	gtkglarea-sharp.pub \
	gtkglarea-sharp.snk

DISTCLEANFILES = \
	gtkglarea-sharp-2.0.pc
