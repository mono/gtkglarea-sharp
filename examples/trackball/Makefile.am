all: TrackballExample.exe

run: TrackballExample.exe
	MONO_PATH=$(srcdir)/../util mono TrackballExample.exe

SOURCE_FILES = \
	TrackballExample.cs \
	TrackballWidget.cs \
	Trackball.cs

TrackballExample.exe: \
	$(SOURCE_FILES) \
	$(srcdir)/../resources/glwidget.glade
	$(MCS) -debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:$(srcdir)/../util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:TrackballExample.exe \
	-resource:$(srcdir)/../resources/glwidget.glade \
		$(SOURCE_FILES)

CLEANFILES = \
	TrackballExample.exe \
	TrackballExample.exe.mdb


EXTRA_DIST = \
	$(SOURCE_FILES)

