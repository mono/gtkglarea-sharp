SOURCE_FILES = \
	Lesson01.cs \
	Lesson02.cs \
	Lesson03.cs \
	Lesson04.cs \
	Lesson05.cs \
	Lesson06.cs

CLEANFILES = \
	Lesson01.exe \
	Lesson02.exe \
	Lesson03.exe \
	Lesson04.exe \
	Lesson05.exe \
	Lesson06.exe


all: \
	Lesson01.exe \
	Lesson02.exe \
	Lesson03.exe \
	Lesson04.exe \
	Lesson05.exe \
	Lesson06.exe

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

Lesson01.exe: Lesson01.cs
	mcs \
		$(MCS_ARGS) \
		Lesson01.cs \
		-out:Lesson01.exe

Lesson02.exe: Lesson02.cs
	mcs \
		$(MCS_ARGS) \
		Lesson02.cs \
		-out:Lesson02.exe

Lesson03.exe: Lesson03.cs
	mcs \
		$(MCS_ARGS) \
		Lesson03.cs \
		-out:Lesson03.exe

Lesson04.exe: Lesson04.cs
	mcs \
		$(MCS_ARGS) \
		Lesson04.cs \
		-out:Lesson04.exe

Lesson05.exe: Lesson05.cs
	mcs \
		$(MCS_ARGS) \
		Lesson05.cs \
		-out:Lesson05.exe

Lesson06.exe: Lesson06.cs
	mcs \
		$(MCS_ARGS) \
		Lesson06.cs \
		-out:Lesson06.exe


