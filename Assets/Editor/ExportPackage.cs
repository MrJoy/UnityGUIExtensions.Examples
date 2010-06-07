using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class ExportPackage {
  private static string LibraryName {
    get {
      return ExampleProjectName.Replace(".Examples", "");
    }
  }
  private static string ExampleProjectName {
    get {
      return Path.GetFileName(Path.GetDirectoryName(Application.dataPath));
    }
  }

  public static string[] LibraryGUIDs {
    get {
      string libraryDir = Path.Combine("Assets", LibraryName);
      string libraryGuid = AssetDatabase.AssetPathToGUID(libraryDir);
      return AssetServerProxy.CollectAllChildren(libraryGuid, new string[] { libraryGuid });
    }
  }
  
  public static string[] ExampleProjectGUIDs {
    get {
      string[] libraryGUIDs = LibraryGUIDs;
      string exampleDir = Path.Combine("Assets", ExampleProjectName);
      string exampleGuid = AssetDatabase.AssetPathToGUID(exampleDir);
      string[] exampleGUIDsTmp = AssetServerProxy.CollectAllChildren(exampleGuid, new string[] { exampleGuid });
      string[] exampleGUIDs = new string[libraryGUIDs.Length + exampleGUIDsTmp.Length];
      for(int i = 0; i < libraryGUIDs.Length; i++) exampleGUIDs[i] = libraryGUIDs[i];
      for(int i = 0; i < exampleGUIDsTmp.Length; i++) exampleGUIDs[libraryGUIDs.Length + i] = exampleGUIDsTmp[i];
      return exampleGUIDs;
    }
  }
  public static void ExportLibraryPackage() {
    EditorApplication.LockReloadAssemblies();
    Export(LibraryGUIDs, LibraryName + ".unitypackage");
  }

  public static void ExportExampleProjectPackage() {
    EditorApplication.LockReloadAssemblies();
    Export(ExampleProjectGUIDs, ExampleProjectName.Replace(".", "_") + ".unitypackage");
  }

  private static void Export(string[] guids, string fname) {
    int state = 0;
    Application.runInBackground = true;
    EditorApplication.CallbackFunction cf = delegate() {
      if(state == 0) {
Console.WriteLine("Exporting " + guids.Length + " assets to library package...");
        AssetServerProxy.ExportPackage(guids, fname);
      } else if(state == 3) {
Console.WriteLine("Exiting...");
        EditorApplication.update = null;
        EditorApplication.Exit(0);
        return;
      }
      state += 1;
Console.WriteLine("Transitioned to state: " + state + ", Time.frameCount=" + Time.frameCount);
      EditorApplication.Step();
    };

Console.WriteLine("Registering callback... " + EditorApplication.isPlaying + " / " + EditorApplication.isPlayingOrWillChangePlaymode + " / " + EditorApplication.isPaused + " / " + EditorApplication.isCompiling);
    EditorApplication.update = cf;
    EditorApplication.Step();
  }

  internal class AssetServerProxy {
    private static Type _AssetServer;
    private static MethodInfo _ExportPackage, _CollectAllChildren;

    static AssetServerProxy() {
      Assembly a = typeof(EditorApplication).Assembly;
      _AssetServer = a.GetType("UnityEditor.AssetServer");
      _ExportPackage = _AssetServer.GetMethod("ExportPackage");
      _CollectAllChildren = _AssetServer.GetMethod("CollectAllChildren");
    }

    internal static string[] CollectAllChildren(string guid, string[] collection) {
      return (string[])_CollectAllChildren.Invoke(null, new object[] { guid, collection });
    }

    internal static void ExportPackage(string[] guids, string fName) {
      _ExportPackage.Invoke(null, new object[] { guids, fName });
    }
  }
}
