EXTRA_DIST = \
	rotatebuttons.cs \
	rotateButtonTest.cs \
	IGLObject.cs \
	GLObjectBase.cs \
	GLWidget.cs \
	Rotation.cs \
	EulerRotation.cs \
	Quaternion.cs \
	RotationMatrix.cs \
	RotationUtil.cs \
	ObjectRotationButton.cs \
	GLObjectRotationController.cs \
	rotation-controller.glade \
	Teapot.cs

CLEANFILES = \
	rotateButtonTest.exe \
	rotatebuttons.exe \
	rotateButtonTest.exe.mdb \
	rotatebuttons.exe.mdb

all: \
	rotateButtonTest.exe \
	rotatebuttons.exe

rotatebuttons.exe: \
	$(srcdir)/rotatebuttons.cs \
	$(srcdir)/../common/Teapot.cs \
	$(srcdir)/../common/Trackball.cs
	$(MCS) -debug \
        -lib:@top_builddir@/gtkglarea \
        -r:gtkglarea-sharp.dll \
        -pkg:tao-opengl-glu \
        -pkg:gtk-sharp-2.0 \
        -out:rotatebuttons.exe \
                $(srcdir)/rotatebuttons.cs \
                $(srcdir)/../common/Teapot.cs \
                $(srcdir)/../common/Trackball.cs

rotateButtonTest.exe: \
	$(srcdir)/rotateButtonTest.cs \
	$(srcdir)/Rotation.cs \
	$(srcdir)/EulerRotation.cs \
	$(srcdir)/Quaternion.cs \
	$(srcdir)/RotationMatrix.cs \
	$(srcdir)/RotationUtil.cs \
	$(srcdir)/IGLObject.cs \
	$(srcdir)/GLObjectBase.cs \
	$(srcdir)/GLWidget.cs \
	$(srcdir)/ObjectRotationButton.cs \
	$(srcdir)/GLObjectRotationController.cs \
	$(srcdir)/Teapot.cs \
	$(srcdir)/../common/glwidget.glade
	$(MCS) -debug \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:tao-opengl-glu \
	-pkg:glade-sharp-2.0 \
	-pkg:gtk-sharp-2.0 \
	-out:rotateButtonTest.exe \
	-resource:$(srcdir)/../common/glwidget.glade \
	-resource:$(srcdir)/rotation-controller.glade \
	-nowarn:0169 \
		$(srcdir)/rotateButtonTest.cs \
		$(srcdir)/Rotation.cs \
		$(srcdir)/EulerRotation.cs \
		$(srcdir)/Quaternion.cs \
		$(srcdir)/RotationMatrix.cs \
		$(srcdir)/RotationUtil.cs \
		$(srcdir)/ObjectRotationButton.cs \
		$(srcdir)/GLObjectRotationController.cs \
		$(srcdir)/GLWidget.cs \
		$(srcdir)/IGLObject.cs \
		$(srcdir)/GLObjectBase.cs \
		$(srcdir)/Teapot.cs

