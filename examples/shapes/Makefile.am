SOURCE_FILES = \
	ColoredSquare.cs \
	ColoredTriangle.cs \
	Cube.cs \
	Pyramid.cs \
	Square.cs \
	Teapot.cs \
	Triangle.cs


EXTRA_DIST = $(SOURCE_FILES)

CLEANFILES = \
	shapes.dll \
	shapes.dll.mdb

all: shapes.dll

shapes.dll: $(SOURCE_FILES)
	$(MCS) -debug \
	-lib:$(top_srcdir)/gtkglarea/ \
	-lib:$(top_srcdir)/examples/util/ \
	-r:gtkglarea-sharp.dll \
	-r:gtkglarea-sharp-util.dll \
	-target:library \
	-pkg:Tao.OpenGl \
	-out:shapes.dll \
		$(SOURCE_FILES)

