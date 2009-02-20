PACKAGES = GTKGLAREA

GTKGLAREA_METADATA=$(top_builddir)/gtkglarea/gtkglarea-sharp.metadata

METADATAS = $(GTKGLAREA_METADATA)

GTKGLAREA_RAW_API=$(top_builddir)/gtkglarea/gtkglarea-api.raw

RAW_APIS = $(GTKGLAREA_RAW_API)

GTKGLAREA_API=$(top_builddir)/gtkglarea/gtkglarea-api.xml

APIS = $(GTKGLAREA_API)

GTKGLAREA_SRCES = gtkglarea-sharp.sources.xml

SOURCES = $(GTKGLAREA_SRCES)

GTKGLAREA_MAJ_MIN = 2.0
GTKGLAREA_MICRO = 0
GTKGLAREA_VERSION = $(GTKGLAREA_MAJ_MIN).$(GTKGLAREA_MICRO)

GTKGLAREA_SOURCEURL = \
	ftp://ftp.gnome.org/pub/GNOME/sources/gtkglarea/$(GTKGLAREA_MAJ_MIN)


SOURCEURLS = $(GTKGLAREA_SOURCEURL)

GTKGLAREA_TARBALL = gtkglarea-$(GTKGLAREA_VERSION).tar.bz2

TARBALLS = $(GTKGLAREA_TARBALL)

all: 
	echo "only needed by maintainer"

source.stamp:
	( test -f $(GTKGLAREA_TARBALL) || \
		wget $(GTKGLAREA_SOURCEURL)/$(GTKGLAREA_TARBALL) \
	) && \
	tar xfvj $(GTKGLAREA_TARBALL) && \
	touch source.stamp


api-raw.stamp: source.stamp
	gapi2-parser $(GTKGLAREA_SRCES) && \
	touch api-raw.stamp

pre-api.stamp: api-raw.stamp
	cp $(GTKGLAREA_RAW_API) $(GTKGLAREA_API) && \
	gapi2-fixup --metadata=$(GTKGLAREA_METADATA) --api=$(GTKGLAREA_API) &&\
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
