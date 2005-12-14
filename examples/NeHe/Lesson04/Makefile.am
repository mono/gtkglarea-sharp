SRCS = Lesson04.cs

CLEANFILES = Lesson04.exe

all: Lesson04.exe

Lesson04.exe: $(SRCS)
	mcs \
		-pkg:gtk-sharp-2.0 \
		-pkg:gtkgl-sharp-2.0 \
		-pkg:tao \
		$(SRCS) \
		-out:Lesson04.exe


