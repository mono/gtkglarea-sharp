all: TrackballExample.exe

run: TrackballExample.exe
	MONO_PATH=$(srcdir)/../util mono --debug TrackballExample.exe

SOURCE_FILES = \
	TrackballExample.cs \
	TrackballWidget.cs \
	$(top_srcdir)/examples/shapes/Teapot.cs \
	Trackball.cs

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:$(srcdir)/../util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-r:$(top_srcdir)/examples/shapes/shapes.dll \
	-pkg:tao-opengl-glu \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:TrackballExample.exe \
	-resource:$(srcdir)/../resources/glwidget.glade 

#	-lib:$(top_srcdir)/examples/shapes


TrackballExample.exe: \
		$(SOURCE_FILES) \
		$(srcdir)/../resources/glwidget.glade
	$(MCS) \
		$(MCS_ARGS) \
		$(SOURCE_FILES)

CLEANFILES = \
	TrackballExample.exe \
	TrackballExample.exe.mdb \
	TrackballExample.exe.pidb


EXTRA_DIST = \
	$(SOURCE_FILES)

