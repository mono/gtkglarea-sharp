all: trackballTest.exe



trackballTest.exe: \
	trackballTest.cs \
	IGLObject.cs \
	GLObjectBase.cs \
	GLWidget.cs \
	TrackballWidget.cs \
	EulerRotation.cs \
	Quaternion.cs \
	RotationMatrix.cs \
	glwidget.glade \
	Teapot.cs
	$(MCS) -debug \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-lib:$(GTKSHARP_PREFIX)/lib \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-out:trackballTest.exe \
	-resource:$(srcdir)/glwidget.glade \
		trackballTest.cs \
		IGLObject.cs \
		GLObjectBase.cs \
		GLWidget.cs \
		TrackballWidget.cs \
		EulerRotation.cs \
		Quaternion.cs \
		RotationMatrix.cs \
		Trackball.cs \
		Teapot.cs

EXTRA_DIST = \
	trackballTest.cs \
	IGLObject.cs \
	GLObjectBase.cs \
	GLWidget.cs \
	TrackballWidget.cs \
	EulerRotation.cs \
	Quaternion.cs \
	RotationMatrix.cs \
	glwidget.glade \
	Trackball.cs \
	Teapot.cs

CLEANFILES = \
	trackballTest.exe \
	rotateButtonTest.exe.mdb


EXTRA_DIST = \
	trackballTest.cs \
	TrackballWidget.cs

CLEANFILES = trackballTest.exe
