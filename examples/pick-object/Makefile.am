SOURCE_FILES = \
	Engine.cs \
	PickWidget.cs \
	ObjectPickExample.cs

CLEANFILES = \
	PickExample.exe \
	PickExample.exe.pidb \
	PickExample.exe.mdb

all: \
	PickExample.exe

run: PickExample.exe
	MONO_PATH="$(top_srcdir)/examples/util:$(top_srcdir)/examples/shapes" \
	mono PickExample.exe


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

PickExample.exe: $(addprefix $(srcdir)/, $(SOURCE_FILES))
	mcs \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:PickExample.exe
