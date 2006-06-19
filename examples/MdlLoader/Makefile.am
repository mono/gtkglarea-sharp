EXAMPLE_NAME=MdlLoader

SOURCE_FILES = \
	$(EXAMPLE_NAME).cs \
	Engine.cs \
	GladeExample.cs

CLEANFILES = \
	$(EXAMPLE_NAME).exe \
	$(EXAMPLE_NAME).exe.mdb \
	$(EXAMPLE_NAME).pidb

all: \
	$(EXAMPLE_NAME).exe

run: MdlLoader.exe
	MONO_PATH="$(top_srcdir)/examples/util" \
	mono $(EXAMPLE_NAME).exe


EXTRA_DIST = \
	$(SOURCE_FILES)

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-lib:$(srcdir)/../util \
	-r:gtkglarea-sharp-util.dll \
	-resource:$(srcdir)/../resources/glwidget.glade \
	-nowarn:0169

$(EXAMPLE_NAME).exe: $(SOURCE_FILES)
	mcs \
		$(MCS_ARGS) \
		$(SOURCE_FILES) \
		-out:$(EXAMPLE_NAME).exe
