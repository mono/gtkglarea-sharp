all: gladeTest.exe

gladeTest.exe: $(srcdir)/gladeTest.cs $(srcdir)/../common/Teapot.cs $(srcdir)/../common/GLWidget.cs
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:gladeTest.exe \
	-resource:$(srcdir)/../common/glwidget.glade \
		$(srcdir)/gladeTest.cs \
		$(srcdir)/../common/Teapot.cs \
		$(srcdir)/../common/GLWidget.cs


EXTRA_DIST = \
	gladeTest.cs \
	glwidget.glade

CLEANFILES = gladeTest.exe
