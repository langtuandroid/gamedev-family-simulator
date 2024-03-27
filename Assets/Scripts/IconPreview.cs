using UnityEngine;
using UnityEngine.UI;

namespace UI.Simple_UI.Samples
{
	public class IconPreview : MonoBehaviour {
		[SerializeField] private Sprite[] icons;

		// Use this for initialization
		void Awake () {
			for (int i = 0; i < icons.Length; i++) {
				var icon = new GameObject ("icon" + i);
				icon.transform.SetParent(this.gameObject.transform);
				icon.AddComponent<RectTransform> ();
				icon.AddComponent<Image> ();
				icon.GetComponent<Image> ().sprite = icons [i];
			}
		}
	}
}
