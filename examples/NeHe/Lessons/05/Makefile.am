SOURCE_FILES = \
	Lesson05.cs

CLEANFILES = \
	Lesson05.exe \
	Lesson05.exe.pidb \
	Lesson05.exe.mdb

all: \
	Lesson05.exe

run: Lesson05.exe
	MONO_PATH=$(top_srcdir)/examples/util:$(top_srcdir)/examples/shapes mono Lesson05.exe

EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES))

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/examples/shapes \
	-lib:@top_builddir@/examples/glade \
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

Lesson05.exe: $(addprefix $(srcdir)/, $(SOURCE_FILES))
		$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:Lesson05.exe
