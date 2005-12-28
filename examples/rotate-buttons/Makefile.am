EXTRA_DIST = \
	rotatebuttons.cs \
	rotateButtonTest.cs

CLEANFILES = \
	rotateButtonTest.exe

all: \
	rotateButtonTest.exe \
	rotatebuttons.cs

rotatebuttons.exe: \
	$(srcdir)/rotatebuttons.cs \
	$(srcdir)/../common/Teapot.cs \
	$(srcdir)/../common/Trackball.cs
	$(MCS) \
        -lib:@top_builddir@/gtkglarea \
        -r:gtkglarea-sharp.dll \
        -pkg:tao-opengl-glu \
        -lib:$(GTKSHARP_PREFIX)/lib \
        -pkg:gtk-sharp-2.0 \
        -out:rotatebuttons.exe \
                $(srcdir)/rotatebuttons.cs \
                $(srcdir)/../common/Teapot.cs \
                $(srcdir)/../common/Trackball.cs

rotateButtonTest.exe: \
	$(srcdir)/rotateButtonTest.cs \
	$(srcdir)/Rotation.cs \
	$(srcdir)/../common/Teapot.cs \
	$(srcdir)/../common/Trackball.cs \
	$(srcdir)/../common/glwidget.glade
	$(MCS) \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:tao-opengl-glu \
	-pkg:glade-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-out:rotateButtonTest.exe \
	-resource:$(srcdir)/../common/glwidget.glade \
		$(srcdir)/rotateButtonTest.cs \
		$(srcdir)/Rotation.cs \
		$(srcdir)/ObjectRotationButton.cs \
		$(srcdir)/../common/GLWidget.cs \
		$(srcdir)/../common/Teapot.cs

