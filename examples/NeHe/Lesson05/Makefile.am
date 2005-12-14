SRCS = Lesson05.cs

CLEANFILES = Lesson05.exe

all: Lesson05.exe

Lesson05.exe: $(SRCS)
	mcs \
		-pkg:gtk-sharp-2.0 \
		-pkg:gtkgl-sharp-2.0 \
		-pkg:tao \
		$(SRCS) \
		-out:Lesson05.exe


