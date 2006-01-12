SOURCE_FILES = \
	Lesson04.cs

CLEANFILES = \
	Lesson04.exe \
	Lesson04.exe.pidb


all: \
	Lesson04.exe

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

Lesson04.exe: Lesson04.cs
	mcs \
		$(MCS_ARGS) \
		Lesson04.cs \
		-out:Lesson04.exe
