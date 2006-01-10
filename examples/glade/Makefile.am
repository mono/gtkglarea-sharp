all: gladeTest.exe

run: gladeTest.exe
	MONO_PATH=$(srcdir)/../util mono gladeTest.exe


SOURCE_FILES = \
	gladeTest.cs

gladeTest.exe: $(srcdir)/gladeTest.cs $(srcdir)/../util/Teapot.cs $(srcdir)/../util/GLWidget.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-lib:$(srcdir)/../util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:gladeTest.exe \
	-resource:$(srcdir)/../resources/glwidget.glade \
		$(SOURCE_FILES)

EXTRA_DIST = \
	$(SOURCE_FILES)

CLEANFILES = gladeTest.exe
