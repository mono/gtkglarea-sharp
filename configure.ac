## Bump this for every release
AC_INIT([gtkglarea-sharp],[0.0.17])
AC_CANONICAL_SYSTEM

## 1.9 is probably not required.  let me know if it effects you
## we're using tar-ustar in case file names get longer than 99 chars
AM_INIT_AUTOMAKE([1.9 tar-ustar])
AM_MAINTAINER_MODE

## Don't bump this unless the API changes.
API_VERSION=0.0.3.0
AC_SUBST(API_VERSION)

AC_MSG_CHECKING([whether we're compiling from an RCS])
if test -f "$srcdir/.cvs_version" ; then
	from_rcs=cvs
else
if test -f "$srcdir/.svn" ; then
	from_rcs=svn
else
	from_rcs=no
fi
fi
AC_MSG_RESULT($from_rcs)

AC_PATH_PROG(RUNTIME, mono, no)
AC_PATH_PROG(MCS, gmcs, no)
if test "x$RUNTIME" = "x" ; then
  AC_MSG_ERROR([Can't find "mono" in your PATH])
fi
if test "x$MCS" = "x" ; then
  AC_MSG_ERROR([Can't find "gmcs" in your PATH])
fi
AC_SUBST(MCS)
AC_SUBST(PATH)
AC_SUBST(LD_LIBRARY_PATH)

dnl Find pkg-config
AC_PATH_PROG(PKG_CONFIG, pkg-config, no)
if test "x$PKG_CONFIG" = "xno"; then
        AC_MSG_ERROR([You need to install pkg-config])
fi

## I think this is ugly and that there's a better way to do it using
## PKG_CHECK_MODULES, but I'm too lazy to research right now
dnl Find GTK
BUILD_GTK_CFLAGS=`$PKG_CONFIG --cflags gtk+-2.0`
BUILD_GTK_LIBS=`$PKG_CONFIG --libs gtk+-2.0`
AC_SUBST(BUILD_GTK_CFLAGS)
AC_SUBST(BUILD_GTK_LIBS)

## We'd probably be safe with 1.3, but let's say 2.0 anyway
GTK_REQUIRED_VERSION=2.0.0

## Tao requires generics, so let's say 1.2
MONO_REQUIRED_VERSION=1.2

## I like the 2.1.0 build of Tao.OpenGl
TAO_OPENGL_REQUIRED_VERSION=2.1.0

PKG_CHECK_MODULES(BASE_DEPENDENCIES, gtk+-2.0 >= $GTK_REQUIRED_VERSION gmodule-2.0 >= $GTK_REQUIRED_VERSION) 
PKG_CHECK_MODULES(MONO_DEPENDENCY, mono >= $MONO_REQUIRED_VERSION, has_mono=true, has_mono=false)
PKG_CHECK_MODULES(TAO_OPENGL_DEPENDENCIES, Tao.OpenGl >= $TAO_OPENGL_REQUIRED_VERSION, has_tao_opengl=yes, has_tao_opengl=no) 

if test "x$has_tao_opengl" = "xno"; then
  AC_MSG_WARN([Tao.OpenGl not detected; examples will not be built])
else
  AC_MSG_NOTICE([Tao.OpenGl found; examples will be build])
fi

AM_CONDITIONAL(HAS_TAO_OPENGL, test x$has_tao_opengl = xyes)

#if test `uname -s` = "Darwin"; then
#	LIB_PREFIX=
#	LIB_SUFFIX=.dylib
#else
	LIB_PREFIX=.so
	LIB_SUFFIX=
#fi

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

AC_CONFIG_FILES([
Makefile
gtkglarea/gtkglarea-sharp.dll.config
gtkglarea/Makefile
gtkglarea/AssemblyInfo.cs
examples/Makefile
examples/rotate-buttons/Makefile
examples/glade/Makefile
examples/trackball/Makefile
examples/util/Makefile
examples/shapes/Makefile
examples/pick-object/Makefile
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

# Frickin' annoying.
MONO_PATH=$$MONO_PATH
MCS_ARGS=
RESOURCE_FILES=
CLEANFILES=
EXTRA_DIST=
ASSEMBLY_DEPS=
AC_SUBST(MONO_PATH)
AC_SUBST(MCS_ARGS)
AC_SUBST(RESOURCE_FILES)
AC_SUBST(CLEANFILES)
AC_SUBST(EXTRA_DIST)
AC_SUBST(ASSEMBLY_DEPS)

AC_OUTPUT

echo "---"
echo "Configuration summary"
echo ""
echo "   * Installation prefix: $prefix"
echo "   * compiler: $MCS"
echo "   * Tao.OpenGl: $has_tao_opengl"
echo "   * Documentation: $enable_monodoc ($MONODOC)"
echo ""
echo "---"

