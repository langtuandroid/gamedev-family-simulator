using UnityEngine;

namespace Inhouse.AdsIH.Scripts
{
	public class Load : MonoBehaviour {

		// Use this for initialization
		void Start () {
			Invoke (nameof(Now), 4f);

		}
	
		// Update is called once per frame
		private void Now () {
			gameObject.SetActive (false);
		}
	}
}
