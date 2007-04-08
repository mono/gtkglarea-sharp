SOURCE_FILES = \
	Lesson01.cs

CLEANFILES = \
	Lesson01.exe \
	Lesson01.exe.pidb \
	Lesson01.exe.mdb


all: \
	Lesson01.exe

run: Lesson01.exe
	MONO_PATH=$(top_srcdir)/examples/util mono Lesson01.exe


EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES))

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/examples/glade \
	-lib:@top_builddir@/examples/util \
	-lib:@top_builddir@/gtkglarea \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:Tao.OpenGl \
	-nowarn:0169 \
	-r:gtkglarea-sharp.dll \
	-r:GladeExample.dll \
	-r:gtkglarea-sharp-util.dll

Lesson01.exe: Lesson01.cs
	$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:Lesson01.exe
