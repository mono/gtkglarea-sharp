SOURCE_FILES = \
	GLObjectRotationController.cs \
	ObjectRotationButton.cs \
	rotateButtonTest.cs	

EXTRA_DIST = \
	$(SOURCE_FILES) \
	rotation-controller.glade

CLEANFILES = \
	rotateButtonTest.exe \
	rotateButtonTest.exe.mdb \
	rotateButtonTest.pidb

all: 	rotateButtonTest.exe

run: rotateButtonTest.exe
	MONO_PATH=$(srcdir)/../util mono rotateButtonTest.exe


rotateButtonTest.exe: \
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
	-out:rotateButtonTest.exe \
	-resource:$(srcdir)/../resources/glwidget.glade \
	-resource:$(srcdir)/rotation-controller.glade \
	-nowarn:0169 \
	$(SOURCE_FILES)
