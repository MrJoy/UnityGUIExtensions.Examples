using UnityEngine;
using System;
using System.Collections;

public class AutoSelectExample : MonoBehaviour {
  private string a = "", b = "", c = "";

  public void OnGUI() {
    GUILayout.BeginArea(new Rect(0, 0, 400, 200));
      a = GUILayoutExt.AutoSelectTextArea("fieldA", a);
      b = GUILayoutExt.AutoSelectTextArea("fieldB", b);
      c = GUILayout.TextArea(c);
    GUILayout.EndArea();
  }
}
