SOURCE_FILES = \
	Lesson03.cs

CLEANFILES = \
	Lesson03.exe \
	Lesson03.exe.pidb \
	Lesson03.exe.mdb


all: \
	Lesson03.exe

run: Lesson03.exe
	MONO_PATH=$(top_srcdir)/examples/util:$(top_srcdir)/examples/shapes mono Lesson03.exe

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
	-r:GladeExample.dll \
	-r:shapes.dll \
	-r:gtkglarea-sharp-util.dll

Lesson03.exe: $(addprefix $(srcdir)/, $(SOURCE_FILES))
		$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:Lesson03.exe
