using System.Collections;
using UnityEngine;

using System.Collections.Generic;


public class BigImageController : MonoBehaviour {
	
	public int mine;
	public GameObject inhouse;

	// Use this for initialization
	void OnEnable ()
	{
		inhouse = GameObject.Find ("IH_Complete").transform.GetChild (1).gameObject;
			mine = Random.Range (0, 3);
					if (inhouse.activeSelf == true) {
						mine=1;
					}
			if (mine == 1) {
			GameObject.Find ("IH_Complete").transform.GetChild (3).gameObject.SetActive (true);
				inhouse.SetActive (true);
			} else {
//				AddsCalling.RewardedVideo ();
			}
//		GoogleAnalyticsV4.instance.LogScreen ("Level Completed" + "/ " + "Scene : " + Application.loadedLevelName);
		}
	}