all: shapes.exe

shapes.exe: $(srcdir)/shapes.cs $(srcdir)/../common/Teapot.cs $(srcdir)/../common/Trackball.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-out:shapes.exe \
		$(srcdir)/shapes.cs \
		$(srcdir)/../common/Teapot.cs \
		$(srcdir)/../common/Trackball.cs

EXTRA_DIST = shapes.cs

CLEANFILES = shapes.exe
