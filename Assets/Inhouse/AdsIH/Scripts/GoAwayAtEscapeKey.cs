using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoAwayAtEscapeKey : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			gameObject.SetActive (false);
		}
	}
}
