AC_INIT(README)
AC_CANONICAL_SYSTEM

## Bump this for every release
AM_INIT_AUTOMAKE(gtkglarea-sharp, 0.0.12)

AM_MAINTAINER_MODE

## Don't bump this unless the API changes.
API_VERSION=0.0.3.0
AC_SUBST(API_VERSION)

MONO_REQUIRED_VERSION=1.0

AC_MSG_CHECKING([whether we're compiling from CVS])
if test -f "$srcdir/.cvs_version" ; then
	from_cvs=yes
else
if test -f "$srcdir/.svn" ; then
	from_cvs=yes
else
	from_cvs=no
fi
fi

AC_MSG_RESULT($from_cvs)

AC_PATH_PROG(MONO, mono)
AC_PATH_PROG(MCS, mcs)
if test "x$MONO" = "x" ; then
  AC_MSG_ERROR([Can't find "mono" in your PATH])
fi
if test "x$MCS" = "x" ; then
  AC_MSG_ERROR([Can't find "mcs" in your PATH])
fi
AC_SUBST(PATH)
AC_SUBST(LD_LIBRARY_PATH)

dnl Find pkg-config
AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
        AC_MSG_ERROR([You need to install pkg-config])
fi

dnl Find GTK
BUILD_GTK_CFLAGS=`$PKG_CONFIG --cflags gtk+-2.0`
BUILD_GTK_LIBS=`$PKG_CONFIG --libs gtk+-2.0`
AC_SUBST(BUILD_GTK_CFLAGS)
AC_SUBST(BUILD_GTK_LIBS)
GTK_REQUIRED_VERSION=2.0.0
MONO_REQUIRED_VERSION=0.95
TAO_REQUIRED_VERSION=0.0.2

PKG_CHECK_MODULES(BASE_DEPENDENCIES, gtk+-2.0 >= $GTK_REQUIRED_VERSION gmodule-2.0 >= $GTK_REQUIRED_VERSION) 
PKG_CHECK_MODULES(MONO_DEPENDENCY, mono >= $MONO_REQUIRED_VERSION, has_mono=true, has_mono=false)
PKG_CHECK_MODULES(TAO_DEPENDENCIES, tao-opengl >= $TAO_REQUIRED_VERSION, has_tao=yes, has_tao=no) 

if test "x$has_mono" = "xtrue"; then
AC_PATH_PROG(RUNTIME, mono, no)
AC_PATH_PROG(CSC, mcs, no)
if test `uname -s` = "Darwin"; then
	LIB_PREFIX=
	LIB_SUFFIX=.dylib
else
	LIB_PREFIX=.so
	LIB_SUFFIX=
fi
else
AC_PATH_PROG(CSC, csc.exe, no)
if test x$CSC = "xno"; then
	AC_MSG_ERROR([You need to install either mono or .Net])
else
RUNTIME=
LIB_PREFIX=
LIB_SUFFIX=.dylib
fi
fi

AC_SUBST(LIB_PREFIX)
AC_SUBST(LIB_SUFFIX)


AC_SUBST(BASE_DEPENDENCIES_CFLAGS)
AC_SUBST(BASE_DEPENDENCIES_LIBS)

dnl Find Gtk#
PKG_CHECK_MODULES(GTK_SHARP, gtk-sharp-2.0 >= 1.9.0)
AC_SUBST(GTK_SHARP_CFLAGS)
AC_SUBST(GTK_SHARP_LIBS)

dnl Find Gtk#
PKG_CHECK_MODULES(GAPI, gapi-2.0 >= 1.9.0)

dnl Find monodoc
MONODOC_REQUIRED_VERSION=1.0
AC_SUBST(MONODOC_REQUIRED_VERSION)
PKG_CHECK_MODULES(MONODOC_DEPENDENCY, monodoc >= $MONODOC_REQUIRED_VERSION, enable_monodoc=yes, enable_monodoc=no)

if test "x$enable_monodoc" = "xyes"; then
        AC_PATH_PROG(MONODOC, monodoc, no)
        if test x$MONODOC = xno; then
           enable_monodoc=no
        fi
else
        MONODOC=
fi

AC_SUBST(MONODOC)
AM_CONDITIONAL(ENABLE_MONODOC, test "x$enable_monodoc" = "xyes")


AC_PATH_PROG(GACUTIL, gacutil, no)
if test "x$GACUTIL" = "xno" ; then
        AC_MSG_ERROR([No gacutil tool found])
fi

GACUTIL_FLAGS='/package gtkglarea-sharp /gacdir $(DESTDIR)$(prefix)/lib'
AC_SUBST(GACUTIL_FLAGS)

winbuild=no
case "$host" in
       *-*-mingw*|*-*-cygwin*)
               winbuild=yes
               ;;
esac
AM_CONDITIONAL(WINBUILD, test x$winbuild = xyes)

AC_SUBST(GACUTIL)

AC_OUTPUT([
Makefile
gtkglarea/gtkglarea-sharp.dll.config
gtkglarea/Makefile
gtkglarea/AssemblyInfo.cs
examples/Makefile
examples/rotate-buttons/Makefile
examples/glade/Makefile
examples/trackball/Makefile
examples/util/Makefile
examples/resources/Makefile
examples/NeHe/Makefile
examples/NeHe/Lessons/Makefile
examples/NeHe/Lessons/01/Makefile
examples/NeHe/Lessons/02/Makefile
examples/NeHe/Lessons/03/Makefile
examples/NeHe/Lessons/04/Makefile
examples/NeHe/Lessons/05/Makefile
examples/NeHe/Lessons/06/Makefile
sources/Makefile
gtkglarea-sharp.pc
checkbuild.sh
])

echo "---"
echo "Configuration summary"
echo ""
echo "   * Installation prefix: $prefix"
echo "   * compiler: $CSC"
echo "   * Tao.Opengl: $has_tao"
echo "   * Documentation: $enable_monodoc ($MONODOC)"
echo ""
echo "---"

