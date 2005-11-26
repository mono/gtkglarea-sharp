all: 
	echo "only needed by maintainer"

get-source-code:
	wget ftp://ftp.gnome.org/pub/GNOME/sources/gtkglarea/1.99/gtkglarea-1.99.0.tar.bz2 --output-document=- | tar -xj

api: 
	PERLLIB=$(GTKSHARP_PREFIX)/share/perl5  \
	PATH=$(GTKSHARP_PREFIX)/bin:$$PATH \
	gapi2-parser gtkglarea-sharp.sources.xml

