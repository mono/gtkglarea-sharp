SOURCE_FILES = \
	Lesson02.cs \
	$(top_srcdir)/examples/glade/GladeExample.cs

CLEANFILES = \
	Lesson02.exe \
	Lesson02.exe.pidb


all: \
	Lesson02.exe

run: Lesson02.exe
	MONO_PATH=$(top_srcdir)/examples/util:$(top_srcdir)/examples/shapes mono Lesson02.exe


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
	-pkg:Tao.OpenGl \
	-r:gtkglarea-sharp.dll \
	-r:shapes.dll \
	-nowarn:0169 \
	-r:gtkglarea-sharp-util.dll

Lesson02.exe: $(SOURCE_FILES)
		$(MCS) \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:Lesson02.exe
