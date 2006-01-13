SOURCE_FILES = \
	Lesson05.cs \
	$(top_srcdir)/examples/glade/GladeExample.cs \
	Cube.cs \
	Pyramid.cs

CLEANFILES = \
	Lesson05.exe \
	Lesson05.exe.pidb


all: \
	Lesson05.exe

run: Lesson05.exe
	MONO_PATH=$(top_srcdir)/examples/util mono Lesson05.exe

EXTRA_DIST = \
	$(SOURCE_FILES)

MCS_ARGS = \
	-debug \
	-lib:$(top_srcdir)/examples/util/ \
	-lib:$(top_srcdir)/examples/shapes/ \
	-lib:$(top_srcdir)/gtkglarea/ \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:tao-opengl-glu \
	-nowarn:0169 \
	-r:gtkglarea-sharp.dll \
	-r:shapes.dll \
	-r:gtkglarea-sharp-util.dll

Lesson05.exe: $(SOURCE_FILES)
	mcs \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:Lesson05.exe
