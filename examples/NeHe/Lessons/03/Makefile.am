SOURCE_FILES = \
	Lesson03.cs \
	$(top_srcdir)/examples/glade/GladeExample.cs


CLEANFILES = \
	Lesson03.exe \
	Lesson03.exe.pidb


all: \
	Lesson03.exe

run: Lesson03.exe
	MONO_PATH=$(top_srcdir)/examples/util mono Lesson03.exe

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
	-r:gtkglarea-sharp.dll \
	-r:shapes.dll \
	-nowarn:0169 \
	-r:gtkglarea-sharp-util.dll

Lesson03.exe: $(SOURCE_FILES)
	mcs \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:Lesson03.exe
