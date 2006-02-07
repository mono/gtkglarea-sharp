SOURCE_FILES = \
	LWObjectExample.cs \
	$(top_srcdir)/examples/glade/GladeExample.cs

CLEANFILES = \
	LWObjectExample.exe \
	LWObjectExample.exe.mdb
	LWObjectExample.exe.pidb

EXTRA_DIST = \
	$(SOURCE_FILES) \
	penguin.lwo


run: LWObjectExample.exe
	MONO_PATH=$(srcdir)/../util mono LWObjectExample.exe

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:$(top_srcdir)/examples/util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-pkg:tao-opengl-glu \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:LWObjectExample.exe \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade 

LWObjectExample.exe: $(SOURCE_FILES)
	$(MCS) \
		$(MCS_ARGS) \
		$(SOURCE_FILES)
