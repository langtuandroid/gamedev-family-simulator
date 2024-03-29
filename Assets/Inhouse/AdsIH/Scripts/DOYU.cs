using UnityEngine;
using System.Collections;
using Inhouse.AdsIH.Scripts;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DOYU : MonoBehaviour {
	void Start()
	{
//		#if UNITY_ANDROID
//		DontDestroyOnLoad(this);
//		#endif
        
       // StartCoroutine (Timer());
	}
	IEnumerator Timer()
	{
		int time = 30;

		while(time >= 0)
		{
			yield return new WaitForSeconds (1f);
			time -= 1;
		}
		//MoreAppsHandler.instance.StartCoroutine(MoreAppsHandler.instance.UpdateUI());
		StartCoroutine (Timer());
	}
}
