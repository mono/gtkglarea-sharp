SOURCE_FILES = \
	Lesson02.cs

CLEANFILES = \
	Lesson02.exe \
	Lesson02.exe.pidb \
	Lesson02.exe.mdb


all: \
	Lesson02.exe

run: Lesson02.exe
	MONO_PATH=$(top_srcdir)/examples/util:$(top_srcdir)/examples/shapes mono Lesson02.exe


EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES))

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/examples/glade \
	-lib:@top_builddir@/examples/shapes \
	-lib:@top_builddir@/examples/util \
	-lib:@top_builddir@/gtkglarea \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:Tao.OpenGl \
	-nowarn:0169 \
	-r:gtkglarea-sharp.dll \
	-r:shapes.dll \
	-r:GladeExample.dll \
	-r:gtkglarea-sharp-util.dll

Lesson02.exe: $(addprefix $(srcdir)/, $(SOURCE_FILES))
		$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:Lesson02.exe
