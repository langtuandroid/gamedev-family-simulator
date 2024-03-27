using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

	[SerializeField] private Text timerText;
	[SerializeField] private float time = 1200;
	[SerializeField] private bool stopTimer,stopSpinner;

	private void Start ()
	{
		StartCoroutine(StartCoundownTimer());
	}

	private IEnumerator StartCoundownTimer()
	{
		if (!stopTimer) {
			time -= Time.deltaTime;
			yield return new WaitForSeconds (0.01f);
			string minutes = Mathf.Floor (time / 60).ToString ("00");
			string seconds = (time % 60).ToString ("00");
			string fraction = ((time * 100) % 100).ToString ("000");
			timerText.text = minutes + ":" + seconds;
			if (time < 10.0f) {
				timerText.color = Color.red;
			}
			StartCoroutine (StartCoundownTimer ());
		}
	}

	private void LateUpdate()
	{
		if (time <= 0.5f && !stopTimer) {
			stopTimer = true;
			GameplayHandler.Gsh.RewardedAd.SetActive (true);
		}
		if (stopTimer) {
			if(!stopSpinner)
				GameplayHandler.Gsh.SpinnerImage.fillAmount += 0.004f;
			if (GameplayHandler.Gsh.SpinnerImage.fillAmount > 0.99f) {
				GameplayHandler.Gsh.LevelFail_CompleteStatusEvent (false);
				gameObject.SetActive (false);
			}
		}
	}

}
