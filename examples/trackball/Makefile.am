all: TrackballExample.exe

run: TrackballExample.exe
	MONO_PATH=$(srcdir)/../util mono --debug TrackballExample.exe

SOURCE_FILES = \
	TrackballExample.cs \
	TrackballWidget.cs \
	Trackball.cs

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:@top_builddir@/examples/util \
	-lib:@top_builddir@/examples/shapes \
	-r:gtkglarea-sharp.dll \
	-r:shapes.dll \
	-r:gtkglarea-sharp-util.dll \
	-r:$(top_srcdir)/examples/shapes/shapes.dll \
	-pkg:Tao.OpenGl \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:TrackballExample.exe \
	-resource:$(srcdir)/../resources/glwidget.glade 

TrackballExample.exe: \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		$(srcdir)/../resources/glwidget.glade
	$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES))

CLEANFILES = \
	TrackballExample.exe \
	TrackballExample.exe.mdb \
	TrackballExample.exe.pidb


EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES))

