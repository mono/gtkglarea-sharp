SUBDIRS = NeHe

EXTRA_DIST = shapes.cs Teapot.cs Trackball.cs
CLEANFILES = shapes.exe

all: shapes.exe

shapes.exe: $(srcdir)/shapes.cs $(srcdir)/Teapot.cs $(srcdir)/Trackball.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-out:shapes.exe $(srcdir)/shapes.cs $(srcdir)/Teapot.cs $(srcdir)/Trackball.cs
