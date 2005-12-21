SUBDIRS = NeHe

EXTRA_DIST = shapes.cs Teapot.cs Trackball.cs
CLEANFILES = \
	shapes.exe \
	rotatebuttons.exe

all: \
	shapes.exe \
	rotatebuttons.exe

shapes.exe: $(srcdir)/shapes.cs $(srcdir)/Teapot.cs $(srcdir)/Trackball.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-out:shapes.exe $(srcdir)/shapes.cs $(srcdir)/Teapot.cs $(srcdir)/Trackball.cs

rotatebuttons.exe: $(srcdir)/rotatebuttons.cs $(srcdir)/Teapot.cs $(srcdir)/Trackball.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-out:rotatebuttons.exe $(srcdir)/rotatebuttons.cs $(srcdir)/Teapot.cs $(srcdir)/Trackball.cs
