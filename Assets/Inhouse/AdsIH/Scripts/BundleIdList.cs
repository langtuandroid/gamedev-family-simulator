using UnityEngine;
using System;
using System.Collections.Generic;

public class BundleIdList : MonoBehaviour {


	public static BundleIdList instance;
	public object locker = new object();
	public object moreAppLocker = new object ();
	public List<string> urls = new List<string> ();
	public List<string> onlyOnDisplay = new List<string> ();
	// Use this for initialization
	void Awake () {
		instance = this;
	}

}
