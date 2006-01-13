SOURCE_FILES = \
	GLObjectRotationController.cs \
	ObjectRotationButton.cs \
	OldGLObjectBase.cs \
	OldTeapot.cs \
	RotateButtonExample.cs	

EXTRA_DIST = \
	$(SOURCE_FILES) \
	rotation-controller.glade

CLEANFILES = \
	RotateButtonExample.exe \
	RotateButtonExample.exe.mdb \
	RotateButtonExample.pidb

all: 	RotateButtonExample.exe

run: RotateButtonExample.exe
	MONO_PATH=$(srcdir)/../util mono RotateButtonExample.exe


RotateButtonExample.exe: \
	$(SOURCE_FILES) \
	$(srcdir)/../resources/glwidget.glade
	$(MCS) -debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-lib:$(srcdir)/../util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-pkg:tao-opengl-glu \
	-pkg:glade-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-out:RotateButtonExample.exe \
	-resource:$(srcdir)/../resources/glwidget.glade \
	-resource:$(srcdir)/rotation-controller.glade \
	-nowarn:0169 \
	$(SOURCE_FILES)
