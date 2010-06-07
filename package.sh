#!/bin/sh
if [ "$UNITY_DIR" == "" ]; then
  export UNITY_DIR=/Applications/Unity
fi
UNITY_BIN="$UNITY_DIR/Unity.app/Contents/MacOS/Unity"
if [ `which "$UNITY_BIN"` == "" ]; then
  echo "Can't find Unity.  Tried: \"$UNITY_BIN\".  Please set UNITY_DIR to the folder containing Unity.app."
  exit 1
fi

echo "Building a .unitypackage of the library itself."
"$UNITY_BIN" -batchmode -quit -executeMethod ExportPackage.ExportLibrary -projectPath .
echo "Building a .unitypackage of the whole example project, including the library."
"$UNITY_BIN" -batchmode -quit -executeMethod ExportPackage.ExportProject -projectPath .
