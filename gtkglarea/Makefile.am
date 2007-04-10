UNMANAGED_NAME = gtkglarea
ASSEMBLY_NAME = $(UNMANAGED_NAME)-sharp
ASSEMBLY_TYPE = library
ASSEMBLY_EXT = dll
ASSEMBLY = $(ASSEMBLY_NAME).$(ASSEMBLY_EXT)

MCS_ARGS = \
	-nowarn:169 \
	-unsafe \
	-target:$(ASSEMBLY_TYPE) \
	$(GTK_SHARP_LIBS)

if ENABLE_MONODOC
MCS_ARGS += -doc:docs/GtkGLAreaSharp.xml
endif

SOURCE_FILES = AssemblyInfo.cs

# Files that contain custom code which gapi will merge into generated/*
CUSTOMS = \
	GLArea.custom

ASSEMBLY_DEPS = \
	$(SOURCE_FILES) \
	generated-stamp \
	$(ASSEMBLY_NAME).snk \
	$(CUSTOMS)

RAW_API=$(UNMANAGED_NAME)-api.raw
API=$(UNMANAGED_NAME)-api.xml
METADATA=$(ASSEMBLY_NAME).metadata

if WINBUILD
CODEGEN=gapi_codegen.exe
FIXUP=gapi-fixup.exe
else
CODEGEN=gapi2-codegen
FIXUP=gapi2-fixup
endif

CLEANFILES = \
	$(ASSEMBLY) \
	generated-stamp \
	generated/*.cs \
	$(API) \
	$(ASSEMBLY_NAME).snk \
	$(ASSEMBLY).config

DISTCLEANFILES = \
	AssemblyInfo.cs \
	docs/GtkGLAreaSharp.xml

noinst_DATA = $(ASSEMBLY)

EXTRA_DIST = \
	$(RAW_API) \
	$(SOURCE_FILES) \
	$(METADATA) \
	AssemblyInfo.cs.in \
	$(ASSEMBLY).config.in \
	$(API) \
	$(CUSTOMS)

all: $(ASSEMBLY)

$(API): $(srcdir)/$(RAW_API) $(srcdir)/$(METADATA)
	cp $(srcdir)/$(RAW_API) $@
	chmod u+w $@
	$(FIXUP) --api=$@ --metadata=$(srcdir)/$(METADATA)

generated-stamp: $(API)
	mkdir -p generated && \
	$(CODEGEN) --generate \
		$(srcdir)/$(API) \
		$(GTK_SHARP_CFLAGS) \
		--outdir=generated \
		--customdir=$(srcdir) \
		--assembly-name=$(ASSEMBLY_NAME) && \
	touch generated-stamp

$(ASSEMBLY_NAME).snk: $(top_srcdir)/$(ASSEMBLY_NAME).snk
	cp $(top_srcdir)/$(ASSEMBLY_NAME).snk .

$(ASSEMBLY): $(ASSEMBLY_DEPS)
	mkdir -p docs && \
	$(MCS) \
		$(MCS_ARGS) \
		$(addprefix $(srcdir)/, $(SOURCE_FILES)) \
		$(srcdir)/generated/*.cs \
		-out:$@

install-data-local: $(ASSEMBLY)
	echo "$(GACUTIL) /package $(ASSEMBLY_NAME) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS)";  \
        $(GACUTIL) /package $(ASSEMBLY_NAME) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS) || exit 1;

uninstall-local:
	echo "$(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS)"; \
        $(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS) || exit 1;
