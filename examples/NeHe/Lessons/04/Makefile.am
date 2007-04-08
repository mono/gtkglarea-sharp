SOURCE_FILES = \
	Lesson04.cs

CLEANFILES = \
	Lesson04.exe \
	Lesson04.exe.pidb \
	Lesson04.exe.mdb

all: \
	Lesson04.exe

run: Lesson04.exe
	MONO_PATH=$(top_srcdir)/examples/util:$(top_srcdir)/examples/shapes mono Lesson04.exe

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
	-r:gtkglarea-sharp-util.dll \
	-r:GladeExample.dll \
	-r:shapes.dll

Lesson04.exe: $(addprefix $(srcdir)/, $(SOURCE_FILES))
		$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:Lesson04.exe
