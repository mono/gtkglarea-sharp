SOURCE_FILES = \
	Engine.cs \
	$(top_srcdir)/examples/trackball/Trackball.cs \
	PickWidget.cs \
	ObjectPickExample.cs \
	$(top_srcdir)/examples/glade/GladeExample.cs

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
	-r:shapes.dll \
	-r:gtkglarea-sharp-util.dll

PickExample.exe: $(SOURCE_FILES)
	mcs \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:PickExample.exe
