## Bump this for every release
AC_INIT([gtkglarea-sharp],[0.0.17])
AC_CANONICAL_SYSTEM
AC_SUBST(PROGRAM)

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

## I like the 2.1.0 build of Tao.OpenGl
TAO_OPENGL_REQUIRED_VERSION=2.1.0

PKG_CHECK_MODULES(TAO_OPENGL_DEPENDENCIES,
                  Tao.OpenGl >= $TAO_OPENGL_REQUIRED_VERSION,
		  has_tao_opengl=yes,
		  has_tao_opengl=no
		  )

## Tao 2.1.0 requires generics, so let's say 1.2
MONO_REQUIRED_VERSION=1.2

PKG_CHECK_MODULES(MONO_DEPENDENCY,
                  mono >= $MONO_REQUIRED_VERSION,
		  has_mono=true,
		  has_mono=false
		  )

if test "x$has_tao_opengl" = "xno"; then
  AC_MSG_WARN([Tao.OpenGl not detected; examples will not be built])
else
  AC_MSG_NOTICE([Tao.OpenGl found; examples will be built])
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
GTK_SHARP_REQUIRED_VERSION=1.9.0
PKG_CHECK_MODULES(GTK_SHARP,
                  gtk-sharp-2.0 >= $GTK_SHARP_REQUIRED_VERSION,
		  has_gtk_sharp=yes,
		  has_gtk_sharp=no
		  )
if test "x$has_gtk_sharp" = "xyes" ; then
  AC_SUBST(GTK_SHARP_CFLAGS)
  AC_SUBST(GTK_SHARP_LIBS)
  gtk_sharp_enabled=yes
else
  AC_MSG_ERROR([You need to install gtk-sharp-2.0])
fi

dnl Find monodoc
MONODOC_REQUIRED_VERSION=1.0
AC_SUBST(MONODOC_REQUIRED_VERSION)
PKG_CHECK_MODULES(MONODOC_DEPENDENCY,
                  monodoc >= $MONODOC_REQUIRED_VERSION,
		  enable_monodoc=yes,
		  enable_monodoc=no
		  )

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
   GACUTIL_FLAGS=''
else
   GACUTIL_FLAGS='/package $(PACKAGE) /gacdir $(DESTDIR)$(prefix)/lib'
fi
AC_SUBST(GACUTIL)
AC_SUBST(GACUTIL_FLAGS)

inbuild=no
case "$host" in
     *-*-mingw*|*-*-cygwin*)
	winbuild=yes
	AC_PATH_PROG(CODEGEN, gapi_codegen.exe, no)
	AC_PATH_PROG(FIXUP, gapi-fixup.exe, no)
        ;;
     *)
	AC_PATH_PROG(CODEGEN, gapi2-codegen, no)
	AC_PATH_PROG(FIXUP, gapi2-fixup, no)
	;;

esac
AM_CONDITIONAL(WINBUILD, test x$winbuild = xyes)

dnl Find GAPI
GAPI_REQUIRED_VERSION=1.9.0
PKG_CHECK_MODULES(GAPI_DEPENDENCY,
                  gapi-2.0 >= GAPI_REQUIRED_VERSION,
		  enable_gapi=yes,
		  enable_gapi=no
		  )
if test "x$enable_gapi" = "xyes" ; then
   if test "x$CODEGEN" = "xyes" ; then
      if test "x$FIXUP" = "xyes" ; then
         has_gapi=yes
         AC_SUBST(CODEGEN)
         AC_SUBST(FIXUP)
      fi
   fi
fi
AM_CONDITIONAL(HAS_GAPI, test x$has_gapi = xyes)

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
echo "Configuration summary for $PACKAGE_NAME"
echo ""
echo "   * Installation prefix: $prefix"
echo "   * compiler: $MCS"
echo "   * gtk-sharp-2.0: $gtk_sharp_enabled"
echo "   * Tao.OpenGl: $has_tao_opengl"
echo "   * Documentation: $enable_monodoc ($MONODOC)"
echo ""
echo "---"

