SOURCE_FILES = \
	GLWidget.cs \
	IGLObject.cs \
	GLObjectBase.cs \
	EulerRotation.cs \
	Rotation.cs \
	Vector.cs \
	Quaternion.cs \
	RotationMatrix.cs \
	Teapot.cs


EXTRA_DIST = $(SOURCE_FILES)

CLEANFILES = \
	gtkglarea-sharp-util.dll \
	gtkglarea-sharp-util.dll.mdb

all: gtkglarea-sharp-util.dll

gtkglarea-sharp-util.dll: \
	$(SOURCE_FILES)
	$(MCS) -debug \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:tao-opengl-glu \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-target:library \
	-out:gtkglarea-sharp-util.dll \
		$(SOURCE_FILES)

