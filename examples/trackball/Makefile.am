SOURCE_FILES = \
	trackballTest.cs \
	TrackballWidget.cs \
	Trackball.cs

all: trackballTest.exe

run: trackballTest.exe
	MONO_PATH=$(srcdir)/../util mono trackballTest.exe

trackballTest.exe: \
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
	-out:trackballTest.exe \
	-resource:$(srcdir)/../resources/glwidget.glade \
	$(SOURCE_FILES)

CLEANFILES = \
	trackballTest.exe \
	trackballTest.exe.mdb


EXTRA_DIST = \
	$(SOURCE_FILES)

