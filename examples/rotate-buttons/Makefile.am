SOURCE_FILES = \
	GLObjectRotationController.cs \
	ObjectRotationButton.cs \
	OldGLObjectBase.cs \
	OldTeapot.cs \
	RotateButtonExample.cs	

EXTRA_DIST = \
	$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
	rotation-controller.glade

CLEANFILES = \
	RotateButtonExample.exe \
	RotateButtonExample.exe.mdb \
	RotateButtonExample.pidb

all: 	RotateButtonExample.exe

run: RotateButtonExample.exe
	MONO_PATH=$(srcdir)/../util mono RotateButtonExample.exe


RotateButtonExample.exe: \
	$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
	$(srcdir)/../resources/glwidget.glade
	$(MCS) -debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:@top_builddir@/examples/util \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-pkg:Tao.OpenGl \
	-pkg:glade-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-out:RotateButtonExample.exe \
	-resource:$(srcdir)/../resources/glwidget.glade \
	-resource:$(srcdir)/rotation-controller.glade \
	-nowarn:0169 \
	$(addprefix $(srcdir)/, $(SOURCE_FILES))
