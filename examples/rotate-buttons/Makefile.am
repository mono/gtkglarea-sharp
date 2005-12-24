EXTRA_DIST = rotatebuttons.cs

CLEANFILES = \
	rotatebuttons.exe

all: \
	rotatebuttons.exe

rotatebuttons.exe: $(srcdir)/rotatebuttons.cs $(srcdir)/../common/Teapot.cs $(srcdir)/../common/Trackball.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-out:rotatebuttons.exe \
		$(srcdir)/rotatebuttons.cs \
		$(srcdir)/../common/Teapot.cs \
		$(srcdir)/../common/Trackball.cs
