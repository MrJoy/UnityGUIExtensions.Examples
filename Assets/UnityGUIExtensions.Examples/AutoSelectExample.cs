using UnityEngine;
using System;
using System.Collections;

public class AutoSelectExample : MonoBehaviour {
  private string a = "Should auto-select.", b = "Should also auto-select.", c = "Should NOT auto-select.";

  public void OnGUI() {
    GUILayout.BeginArea(new Rect(0, 0, 400, 200));
      GUILayout.BeginHorizontal();
        GUILayout.Label("Auto-selecting text field:", GUILayout.Width(140));
        a = GUILayoutAutoSelect.TextArea("fieldA", a);
      GUILayout.EndHorizontal();
      GUILayout.BeginHorizontal();
        GUILayout.Label("Auto-selecting text field:", GUILayout.Width(140));
        b = GUILayoutAutoSelect.TextArea("fieldB", b);
      GUILayout.EndHorizontal();
      GUILayout.BeginHorizontal();
        GUILayout.Label("Plain text field:", GUILayout.Width(140));
        c = GUILayout.TextArea(c);
      GUILayout.EndHorizontal();
      GUILayout.Label("The auto-selecting text fields above will automatically select their contents when they gain focus, so by default the user will be able to replace the contents without having to erase them explicitly.");
    GUILayout.EndArea();
  }
}
