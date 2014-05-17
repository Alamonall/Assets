using UnityEngine;
using System.Collections;

public class TestingGUI : MonoBehaviour {
	public Texture2D iCon;
	public GUIStyle MyStule;
	void OnGUI () {
		GUI.BeginGroup(new Rect(Screen.width/2, Screen.height/2,600,700),MyStule);
			GUI.Box (new Rect(10,10,400, 500),iCon);
		GUI.EndGroup();
	}
}
