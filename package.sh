#!/bin/sh
if [ "$UNITY_DIR" == "" ]; then
  export UNITY_DIR=/Applications/Unity
fi
UNITY_BIN="$UNITY_DIR/Unity.app/Contents/MacOS/Unity"
if [ `which "$UNITY_BIN"` == "" ]; then
  echo "Can't find Unity.  Tried: \"$UNITY_BIN\".  Please set UNITY_DIR to the folder containing Unity.app."
  exit 1
fi

echo "Building library package..."
"$UNITY_BIN" -batchmode -executeMethod ExportPackage.ExportLibraryPackage -projectPath "`pwd`"
echo "Building example project package..."
"$UNITY_BIN" -batchmode -executeMethod ExportPackage.ExportExampleProjectPackage -projectPath "`pwd`"

if [ -d "Temp/" ]; then
  rm -rf Temp/
fi

