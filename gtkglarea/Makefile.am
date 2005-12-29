MCS=mcs
RAW_API=gtkglarea-api.raw
API=gtkglarea-api.xml
METADATA=gtkglarea-sharp.metadata

ASSEMBLY_NAME= gtkglarea-sharp
ASSEMBLY= $(ASSEMBLY_NAME).dll

if WINBUILD
CODEGEN=gapi_codegen.exe
FIXUP=gapi-fixup.exe
else
CODEGEN=gapi2-codegen
FIXUP=gapi2-fixup
endif

build_sources = AssemblyInfo.cs

CLEANFILES = \
	gtkglarea-sharp.dll \
	generated-stamp \
	generated/*.cs \
	$(API) \
	gtkglarea-sharp.snk

DISTCLEANFILES= AssemblyInfo.cs

noinst_DATA = gtkglarea-sharp.dll

all: generated-stamp gtkglarea-sharp.dll

$(API): $(srcdir)/$(RAW_API) $(srcdir)/$(METADATA)
	cp $(srcdir)/$(RAW_API) $(API)
	chmod u+w $(API)
	$(FIXUP) --api=$(srcdir)/$(API) --metadata=$(srcdir)/$(METADATA)

generated-stamp: $(API)
	$(CODEGEN) --generate \
	$(srcdir)/$(API) \
	$(GTK_SHARP_CFLAGS) \
	--outdir=generated \
	--customdir=$(srcdir) \
	--assembly-name=gtkglarea-sharp && \
	touch generated-stamp

gtkglarea-sharp.snk: $(top_srcdir)/gtkglarea-sharp.snk
	cp $(top_srcdir)/gtkglarea-sharp.snk .

gtkglarea-sharp.dll: $(build_sources) generated-stamp gtkglarea-sharp.snk
	$(MCS) -nowarn:169 -unsafe -target:library $(GTK_SHARP_LIBS) \
	$(build_sources) generated/*.cs -out:gtkglarea-sharp.dll

install-data-local:
	echo "$(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS)";  \
        $(GACUTIL) /i $(ASSEMBLY) /f $(GACUTIL_FLAGS) || exit 1;

uninstall-local:
	echo "$(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS)"; \
        $(GACUTIL) /u $(ASSEMBLY_NAME) $(GACUTIL_FLAGS) || exit 1;

EXTRA_DIST = \
	$(RAW_API) \
	$(sources) \
	$(customs) \
	$(METADATA) \
	AssemblyInfo.cs.in \
	gtkglarea-sharp.dll.config.in \
	$(API)
