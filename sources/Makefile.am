PACKAGES = GTKGLAREA GTKGLEXT

GTKGLAREA_METADATA=$(top_builddir)/gtkglarea/gtkglarea-sharp.metadata
GTKGLEXT_METADATA=$(top_builddir)/gtkglext/gtkglext-sharp.metadata
METADATAS = $(GTKGLAREA_METADATA) $(GTKGLEXT_METADATA)

GTKGLAREA_RAW_API=$(top_builddir)/gtkglarea/gtkglarea-api.raw
GTKGLEXT_RAW_API=$(top_builddir)/gtkglext/gtkglext-api.raw
RAW_APIS = $(GTKGLAREA_RAW_API) $(GTKGLEXT_RAW_API)

GTKGLAREA_API=$(top_builddir)/gtkglarea/gtkglarea-api.xml
GTKGLEXT_API=$(top_builddir)/gtkglext/gtkglext-api.xml
APIS = $(GTKGLAREA_API) $(GTKGLEXT_API)

GTKGLAREA_SOURCES = gtkglarea-sharp.sources.xml
GTKGLEXT_SOURCES = gtkglext-sharp.sources.xml
SOURCES = $(GTKGLAREA_SOURCES) $(GTKGLEXT_SOURCES)

GTKGLAREA_SOURCEURL = ftp://ftp.gnome.org/pub/GNOME/sources/gtkglarea/1.99
GTKGLEXT_SOURCEURL = http://easynews.dl.sourceforge.net/sourceforge/gtkglext
SOURCEURLS = $(GTKGLAREA_SOURCEURL) $(GTKGLEXT_SOURCEURL)

GTKGLAREA_TARBALL = gtkglarea-1.99.0.tar.bz2
GTKGLEXT_TARBALL = gtkglext-1.0.6.tar.bz2
TARBALLS = $(GTKGLAREA_TARBALL) $(GTKGLEXT_TARBALL)

all: 
	echo "only needed by maintainer"

source.stamp:
	test -f $(GTKGLAREA_TARBALL) || \
		wget $(GTKGLAREA_SOURCEURL)/$(GTKGLAREA_TARBALL) \
	) && \
	tar xfvj $(GTKGLAREA_TARBALL) && \
	test -f $(GTKGLEXT_TARBALL) || \
		wget $(GTKGLEXT_SOURCEURL)/$(GTKGLEXT_TARBALL) \
	) && \
	tar xfvj $(GTKGLEXT_TARBALL) && \
	touch source.stamp

api-raw.stamp: source.stamp
	gapi2-parser $(GTKGLAREA_SOURCES) && \
	gapi2-parser $(GTKGLEXT_SOURCES) && \
	touch api-raw.stamp

pre-api.stamp: api-raw.stamp
	cp $(GTKGLAREA_RAW_API) $(GTKGLAREA_API) && \
	gapi2-fixup --metadata=$(GTKGLAREA_METADATA) --api=$(GTKGLAREA_API) &&\
	cp $(GTKGLEXT_RAW_API) $(GTKGLEXT_API) && \
	gapi2-fixup --metadata=$(GTKGLEXT_METADATA) --api=$(GTKGLEXT_API) &&\
	touch pre-api.stamp

api: pre-api.stamp
	touch api.stamp

CLEANFILES = \
	api.stamp \
	pre-api.stamp \
	api-raw.stamp \
	source.stamp \
	$(APIS)


DISTCLEANFILES = \
	$(CLEANFILES)

EXTRA_DIST = \
	$(RAW_APIS)
