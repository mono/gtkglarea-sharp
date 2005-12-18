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
	Lesson01.cs \
	Lesson02.cs \
	Lesson03.cs \
	Lesson04.cs \
	Lesson05.cs \
	Lesson06.cs


Lesson01.exe: Lesson01.cs
	mcs \
		-pkg:gtk-sharp-2.0 \
		-r:$(top_srcdir)/gtkglarea/gtkglarea-sharp.dll \
		-pkg:tao-opengl-glu \
		Lesson01.cs \
		-out:Lesson01.exe

Lesson02.exe: Lesson02.cs
	mcs \
		-pkg:gtk-sharp-2.0 \
		-r:$(top_srcdir)/gtkglarea/gtkglarea-sharp.dll \
		-pkg:tao-opengl-glu \
		Lesson02.cs \
		-out:Lesson02.exe

Lesson03.exe: Lesson03.cs
	mcs \
		-pkg:gtk-sharp-2.0 \
		-r:$(top_srcdir)/gtkglarea/gtkglarea-sharp.dll \
		-pkg:tao-opengl-glu \
		Lesson03.cs \
		-out:Lesson03.exe

Lesson04.exe: Lesson04.cs
	mcs \
		-pkg:gtk-sharp-2.0 \
		-r:$(top_srcdir)/gtkglarea/gtkglarea-sharp.dll \
		-pkg:tao-opengl-glu \
		Lesson04.cs \
		-out:Lesson04.exe

Lesson05.exe: Lesson05.cs
	mcs \
		-pkg:gtk-sharp-2.0 \
		-r:$(top_srcdir)/gtkglarea/gtkglarea-sharp.dll \
		-pkg:tao-opengl-glu \
		Lesson05.cs \
		-out:Lesson05.exe

Lesson06.exe: Lesson06.cs
	mcs \
		-pkg:gtk-sharp-2.0 \
		-r:$(top_srcdir)/gtkglarea/gtkglarea-sharp.dll \
		-pkg:tao-opengl-glu \
		Lesson06.cs \
		-out:Lesson06.exe


