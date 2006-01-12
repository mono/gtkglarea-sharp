SOURCE_FILES = \
	Lesson05.cs

CLEANFILES = \
	Lesson05.exe \
	Lesson05.exe.pidb


all: \
	Lesson05.exe

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

Lesson05.exe: Lesson05.cs
	mcs \
		$(MCS_ARGS) \
		Lesson05.cs \
		-out:Lesson05.exe
