using UnityEngine;
using System.Collections;

class StateObject {
  public int id;
}
public class TestSemantics : MonoBehaviour {
  private string myTextField = "";
  public void OnGUI() {
/*    GUIUtility.hotControl
    GUIUtility.keyboardControl
    
    static function GetControlID (FocusType.Keyboard) : int
    static function GetControlID (hint : int, FocusType.Keyboard) : int
    */
    GUILayout.BeginArea(new Rect(0,0,500,300), "", "box");
      GUILayout.Label("hotControl: " + GUIUtility.hotControl);
      GUILayout.Label("keyboardControl: " + GUIUtility.keyboardControl);
      
      GUILayout.Button("Click me");
      myTextField = GUILayout.TextArea(myTextField);
      MetaWidget("A");
      MetaWidget("B");
    GUILayout.EndArea();
  }
  
  void MetaWidget(string content) {
    int id = GUIUtility.GetControlID(FocusType.Keyboard);
    StateObject so = (StateObject)GUIUtility.GetStateObject(typeof(StateObject), id);
    if(so.id == 0) {
      Debug.Log("Wiring up state object to control ID " + id);
      so.id = id;
    } else if(so.id != id) {
      Debug.Log("Whoops!  State object has ID of " + so.id + ", not " + id);
    }
    GUILayout.Label(content + ": " + id + " / " + so.GetHashCode());
  }
}
