using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuIH : MonoBehaviour {

	void Start(){
		#if UNITY_IOS
		Invoke ("ShowRNow",2.5f);
		#endif
	}


	
	void OnDisable () {

		GameObject.Find("2").transform.GetChild(1).gameObject.SetActive(false);
		GameObject.Find("2").transform.GetChild(2).gameObject.SetActive(false);
		if (PlayerPrefs.GetInt ("AmazonAds") == 1) {
			GameObject.Find ("Ban").transform.GetChild (1).gameObject.SetActive (true);
		}
		GameObject.Find("Big").transform.GetChild(1).gameObject.SetActive(false);
		GameObject.Find("Big").transform.GetChild(1).transform.GetChild(0).gameObject.
		transform.GetChild(0).gameObject.SetActive(false);
	}
}
