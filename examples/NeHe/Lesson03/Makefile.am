SRCS = Lesson03.cs

CLEANFILES = Lesson03.exe

all: Lesson03.exe

Lesson03.exe: $(SRCS)
	mcs \
		-pkg:gtk-sharp-2.0 \
		-pkg:gtkgl-sharp-2.0 \
		-pkg:tao \
		$(SRCS) \
		-out:Lesson03.exe


