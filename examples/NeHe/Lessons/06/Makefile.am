SOURCE_FILES = \
	Lesson06.cs

CLEANFILES = \
	Lesson06.exe \
	Lesson06.exe.pidb \
	Lesson06.exe.mdb


all: \
	Lesson06.exe

EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES))

MCS_ARGS = \
	-lib:@top_builddir@/examples/shapes \
	-lib:@top_builddir@/examples/util \
	-lib:@top_builddir@/gtkglarea \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:Tao.OpenGl \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-nowarn:0169

Lesson06.exe: Lesson06.cs
		$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:Lesson06.exe
