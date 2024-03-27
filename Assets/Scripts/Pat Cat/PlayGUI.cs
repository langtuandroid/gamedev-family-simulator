using UnityEngine;
using System.Collections;

public class PlayGUI : MonoBehaviour {
	public Transform[] transforms;
	
	public GUIContent[] GUIContents;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transforms.Length; i++) {
			transforms[i].GetComponent<Animation>()["idle0"].layer = -1;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUILayout.BeginVertical("box");
		for (int i = 0; i < GUIContents.Length; i++) {
			if (GUILayout.Button(GUIContents[i])) {
				for (int j = 0; j < transforms.Length; j++) {
					transforms[j].GetComponent<Animation>().Play(GUIContents[i].text);
				}
			}
		}
		GUILayout.EndVertical();
	}
	
	

}
