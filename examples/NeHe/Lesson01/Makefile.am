SRCS = Lesson01.cs

CLEANFILES = Lesson01.exe

all: Lesson01.exe

Lesson01.exe: $(SRCS)
	mcs \
		-pkg:gtk-sharp-2.0 \
		-pkg:gtkgl-sharp-2.0 \
		-pkg:tao \
		$(SRCS) \
		-out:Lesson01.exe


