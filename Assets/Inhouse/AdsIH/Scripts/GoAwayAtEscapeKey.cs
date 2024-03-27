using UnityEngine;

namespace Inhouse.AdsIH.Scripts
{
	public class GoAwayAtEscapeKey : MonoBehaviour {
		
		private void Update () {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				gameObject.SetActive (false);
			}
		}
	}
}
