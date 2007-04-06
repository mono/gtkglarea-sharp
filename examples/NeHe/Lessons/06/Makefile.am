SOURCE_FILES = \
	Lesson06.cs

CLEANFILES = \
	Lesson06.exe \
	Lesson06.exe.pidb


all: \
	Lesson06.exe

EXTRA_DIST = \
	$(SOURCE_FILES)

MCS_ARGS = \
	-lib:$(top_srcdir)/examples/util/ \
	-lib:$(top_srcdir)/gtkglarea/ \
	-resource:$(top_srcdir)/examples/resources/glwidget.glade \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-pkg:Tao.OpenGl \
	-r:gtkglarea-sharp.dll \
	-nowarn:0169 \
	-r:gtkglarea-sharp-util.dll

Lesson06.exe: Lesson06.cs
		$(MCS) \
		$(MCS_ARGS) \
		Lesson06.cs \
		-out:Lesson06.exe
