SOURCE_FILES = \
	Lesson04.cs \
	$(top_srcdir)/examples/glade/GladeExample.cs

CLEANFILES = \
	Lesson04.exe \
	Lesson04.exe.pidb


all: \
	Lesson04.exe

run: Lesson04.exe
	MONO_PATH=$(top_srcdir)/examples/util mono Lesson04.exe

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
	-r:gtkglarea-sharp-util.dll \
	-r:shapes.dll

Lesson04.exe: $(SOURCE_FILES)
	mcs \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:Lesson04.exe
