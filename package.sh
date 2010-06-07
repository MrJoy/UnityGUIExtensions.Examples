#!/bin/sh
if [ "$UNITY_DIR" == "" ]; then
  export UNITY_DIR=/Applications/Unity
fi
UNITY_BIN="$UNITY_DIR/Unity.app/Contents/MacOS/Unity"
if [ `which "$UNITY_BIN"` == "" ]; then
  echo "Can't find Unity.  Tried: \"$UNITY_BIN\".  Please set UNITY_DIR to the folder containing Unity.app."
  exit 1
fi

DATESTAMP=`date +%Y%m%d`
echo "Building library package..."
"$UNITY_BIN" -batchmode -executeMethod ExportPackage.ExportLibraryPackage -projectPath "`pwd`"
PKG=UnityGUIExtensions.unitypackage
if [ -e $PKG ]; then 
  PKGDIR=${PKG%.unitypackage}_$DATESTAMP
  mkdir $PKGDIR
  mv $PKG $PKGDIR
  zip -9 $PKGDIR.zip $PKGDIR/$PKG
  rm -rf $PKGDIR
fi

echo "Building example project package..."
"$UNITY_BIN" -batchmode -executeMethod ExportPackage.ExportExampleProjectPackage -projectPath "`pwd`"
PKG=UnityGUIExtensions_Examples.unitypackage
if [ -e $PKG ]; then 
  PKGDIR=${PKG%.unitypackage}_$DATESTAMP
  mkdir $PKGDIR
  mv $PKG $PKGDIR
  zip -9 $PKGDIR.zip $PKGDIR/$PKG
  rm -rf $PKGDIR
fi

if [ -d "Temp/" ]; then
  rm -rf Temp/
fi

