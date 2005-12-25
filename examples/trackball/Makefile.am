all: trackballTest.exe

trackballTest.exe: $(srcdir)/trackballTest.cs $(srcdir)/../common/Teapot.cs $(srcdir)/../common/Trackball.cs $(srcdir)/../common/GLWidget.cs $(srcdir)/TrackballWidget.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:trackballTest.exe \
	-resource:$(srcdir)/../common/glwidget.glade \
		$(srcdir)/trackballTest.cs \
		$(srcdir)/TrackballWidget.cs \
		$(srcdir)/../common/Teapot.cs \
		$(srcdir)/../common/GLWidget.cs \
		$(srcdir)/../common/Trackball.cs

EXTRA_DIST = \
	trackballTest.cs \
	TrackballWidget.cs

CLEANFILES = trackballTest.exe
