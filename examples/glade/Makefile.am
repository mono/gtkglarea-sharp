all: GladeExample.exe

run: GladeExample.exe
	MONO_PATH=$(srcdir)/../util mono GladeExample.exe

DLL_SOURCE_FILES = \
	GladeExample.cs

EXE_SOURCE_FILES = \
	Engine.cs

SOURCE_FILES=$(DLL_SOURCE_FILES) $(EXE_SOURCE_FILES)

MCS_ARGS = \
	-lib:@top_builddir@/gtkglarea \
	-lib:@top_builddir@/examples/util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-pkg:Tao.OpenGl \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-nowarn:0169 \
	-resource:$(srcdir)/../resources/glwidget.glade


GladeExample.dll: \
		$(addprefix $(srcdir)/, $(DLL_SOURCE_FILES)) \
		$(srcdir)/../resources/glwidget.glade \
		$(srcdir)/../util/gtkglarea-sharp-util.dll
	$(MCS) \
		$(MCS_ARGS) \
		/target:library \
		$(addprefix $(srcdir)/, $(DLL_SOURCE_FILES)) \
		-out:GladeExample.dll

GladeExample.exe: \
		$(addprefix $(srcdir)/, $(EXE_SOURCE_FILES)) \
		$(srcdir)/../resources/glwidget.glade \
		$(srcdir)/../util/gtkglarea-sharp-util.dll \
		GladeExample.dll
	$(MCS) \
		$(MCS_ARGS) \
		/target:winexe \
		$(addprefix $(srcdir)/, $(EXE_SOURCE_FILES)) \
		-r:GladeExample.dll \
		-out:GladeExample.exe

CLEANFILES = \
	GladeExample.exe \
	GladeExample.exe.mdp \
	GladeExample.dll

EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES))
