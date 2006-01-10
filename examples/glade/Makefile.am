all: GladeExample.exe

run: GladeExample.exe
	MONO_PATH=$(srcdir)/../util mono GladeExample.exe

SOURCE_FILES = \
	GladeExample.cs

GladeExample.exe: \
	$(SOURCE_FILES) \
	$(srcdir)/../resources/glwidget.glade \
	$(srcdir)/../util/gtkglarea-sharp-util.dll
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-lib:$(srcdir)/../util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:GladeExample.exe \
	-resource:$(srcdir)/../resources/glwidget.glade \
	$(SOURCE_FILES)

CLEANFILES = \
	GladeExample.exe \
	GladeExample.exe.mdp

EXTRA_DIST = \
	$(SOURCE_FILES)
