using UnityEngine;
using System;
using System.Collections;

public class AutoSelectExample : MonoBehaviour {
  private string a = "", b = "", c = "";
  public Font normalFont, boldFont, italicFont;

  public void OnGUI() {
    GUILayout.BeginArea(new Rect(0, 0, 400, 200));
      a = GUILayoutExt.AutoSelectTextArea("fieldA", a);
      b = GUILayoutExt.AutoSelectTextArea("fieldB", b);
      c = GUILayout.TextArea(c);
    GUILayout.EndArea();
    GUILayout.BeginArea(new Rect(0, 210, 400, 500));
      GUICommon.FancyLabel(new Rect(0, 0, 400, 500), c, GUI.skin.label, normalFont, boldFont, italicFont, TextAlignment.Left);
    GUILayout.EndArea();
  }
}
