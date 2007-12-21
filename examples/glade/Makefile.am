DLL_ASSEMBLY_NAME = GladeExample
DLL_ASSEMBLY_TYPE = library
DLL_ASSEMBLY_EXT = dll
DLL_SOURCE_FILES = \
	GladeExample.cs

DLL_RESOURCE_FILES = $(top_srcdir)/examples/resources/glwidget.glade

DLL_MCS_ARGS = \
	-lib:@top_builddir@/gtkglarea \
	-r:gtkglarea-sharp.dll \
	-lib:@top_builddir@/examples/util \
	-r:gtkglarea-sharp-util.dll \
	-pkg:tao-opengl \
	-pkg:gtk-sharp-2.0 \
	-pkg:glade-sharp-2.0 \
	-nowarn:0169 \
	-target:$(DLL_ASSEMBLY_TYPE) \
	$(addprefix -resource:, $(RESOURCE_FILES))

DLL_ASSEMBLY = $(DLL_ASSEMBLY_NAME).$(DLL_ASSEMBLY_EXT)

DLL_ASSEMBLY_DEPS = \
	@top_builddir@/gtkglarea/gtkglarea-sharp.dll \
	@top_builddir@/examples/util/gtkglarea-sharp-util.dll \
	$(addprefix $(srcdir)/, $(DLL_SOURCE_FILES)) \
	$(DLL_RESOURCE_FILES)

$(DLL_ASSEMBLY): $(DLL_ASSEMBLY_DEPS)
	$(MCS) \
		$(DLL_MCS_ARGS) \
		$(addprefix $(srcdir)/, $(DLL_SOURCE_FILES)) \
		-out:$(DLL_ASSEMBLY)

ASSEMBLY_NAME = GladExample
ASSEMBLY_TYPE = winexe
ASSEMBLY_EXT = exe
ASSEMBLY_DEPS = $(DLL_ASSEMBLY)
SOURCE_FILES = Engine.cs

CLEANFILES += \
	GladeExample.dll \
	GladeExample.dll.mdp

include $(top_srcdir)/examples/util/Include.am
include $(top_srcdir)/examples/glade/Include.am
include $(top_srcdir)/examples/Include.am

EXTRA_DIST += \
	$(addprefix $(srcdir)/, $(DLL_SOURCE_FILES))
