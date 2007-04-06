SOURCE_FILES = \
	Lesson01.cs \
	$(top_srcdir)/examples/glade/GladeExample.cs

CLEANFILES = \
	Lesson01.exe \
	Lesson01.exe.pidb


all: \
	Lesson01.exe

run: Lesson01.exe
	MONO_PATH=$(top_srcdir)/examples/util mono Lesson01.exe


EXTRA_DIST = \
	$(SOURCE_FILES)

MCS_ARGS = \
	-debug \
	-lib:$(top_srcdir)/examples/util/ \
	-lib:$(top_srcdir)/gtkglarea/ \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:Tao.OpenGl \
	-r:gtkglarea-sharp.dll \
	-nowarn:0169 \
	-r:gtkglarea-sharp-util.dll

Lesson01.exe: Lesson01.cs
	$(MCS) \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:Lesson01.exe
