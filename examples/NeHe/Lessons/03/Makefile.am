SOURCE_FILES = \
	Lesson03.cs

CLEANFILES = \
	Lesson03.exe \
	Lesson03.exe.pidb


all: \
	Lesson03.exe

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

Lesson03.exe: Lesson03.cs
	mcs \
		$(MCS_ARGS) \
		Lesson03.cs \
		-out:Lesson03.exe
