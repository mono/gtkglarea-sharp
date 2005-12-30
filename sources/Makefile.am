METADATA=$(top_builddir)/gtkglarea/gtkglarea-sharp.metadata
RAW_API=$(top_builddir)/gtkglarea/gtkglarea-api.raw
API=$(top_builddir)/gtkglarea/gtkglarea-api.xml

SOURCES = gtkglarea-sharp.sources.xml

SOURCEURL = ftp://ftp.gnome.org/pub/GNOME/sources/gtkglarea/1.99
TARBALL = gtkglarea-1.99.0.tar.bz2

all: 
	echo "only needed by maintainer"

source.stamp:
	( test -f $(TARBALL) || \
	wget $(SOURCEURL)/$(TARBALL) \
	) && \
	tar xfvj $(TARBALL) && \
	touch source.stamp

api-raw.stamp: source.stamp
	PATH=$(GTKSHARP_PREFIX)/bin:$$PATH \
	gapi2-parser gtkglarea-sharp.sources.xml && \
	touch api-raw.stamp

pre-api.stamp: api-raw.stamp
	cp $(RAW_API) $(API) && \
	PATH=$(GTKSHARP_PREFIX)/bin:$$PATH \
	gapi2-fixup --metadata=$(METADATA) --api=$(API) && \
	touch pre-api.stamp

api: pre-api.stamp
	touch api.stamp

CLEANFILES = \
	api.stamp \
	pre-api.stamp \
	api-raw.stamp \
	source.stamp \
	$(API)


DISTCLEANFILES = \
	$(CLEANFILES)

EXTRA_DIST = \
	$(RAW_API)
