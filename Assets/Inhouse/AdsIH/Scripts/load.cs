using UnityEngine;
using System.Collections;

public class load : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("NOW", 4f);

	}
	
	// Update is called once per frame
	void NOW () {
		gameObject.SetActive (false);
	}
}
