SOURCE_FILES = \
	LWObjectExample.cs

CLEANFILES = \
	LWObjectExample.exe \
	LWObjectExample.exe.mdb
	LWObjectExample.exe.pidb

EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
	penguin.lwo


run: LWObjectExample.exe
	MONO_PATH=$(srcdir)/../util mono LWObjectExample.exe

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:@top_builddir@/examples/glade \
	-lib:@top_builddir@/examples/util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-r:GladeExample.dll \
	-pkg:tao-opengl \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:LWObjectExample.exe \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade 

LWObjectExample.exe: $(addprefix $(srcdir)/, $(SOURCE_FILES))
	$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES))
