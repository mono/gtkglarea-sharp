SOURCE_FILES = \
	Lesson02.cs \
	Triangle.cs \
	Square.cs


CLEANFILES = \
	Lesson02.exe \
	Lesson02.exe.pidb


all: \
	Lesson02.exe

EXTRA_DIST = \
	$(SOURCE_FILES)

MCS_ARGS = \
	-lib:$(top_srcdir)/examples/util/ \
	-lib:$(top_srcdir)/gtkglarea/ \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:tao-opengl-glu \
	-r:gtkglarea-sharp.dll \
	-nowarn:0169 \
	-r:gtkglarea-sharp-util.dll

Lesson02.exe: $(SOURCE_FILES)
	mcs \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:Lesson02.exe
