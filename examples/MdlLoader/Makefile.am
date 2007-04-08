EXAMPLE_NAME=MdlLoader

SOURCE_FILES = \
	$(EXAMPLE_NAME).cs \
	Mdl.cs \
	MdlTypes.cs \
	Engine.cs

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
	$(addprefix $(srcdir)/, $(SOURCE_FILES))

MCS_ARGS = \
	-debug \
	-lib:@top_builddir@/gtkglarea \
	-lib:@top_builddir@/examples/util \
	-lib:@top_builddir@/examples/glade \
	-r:gtkglarea-sharp-util.dll \
	-r:gtkglarea-sharp.dll \
	-r:GladeExample.dll \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-resource:$(srcdir)/../resources/glwidget.glade \
	-nowarn:0169

$(EXAMPLE_NAME).exe: $(addprefix $(srcdir)/, $(SOURCE_FILES))
	mcs \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		-out:$(EXAMPLE_NAME).exe
