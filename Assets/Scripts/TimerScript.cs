using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TimerScript : MonoBehaviour {

	[SerializeField] private Text timerText;
	[SerializeField] private float time = 1200;
	[SerializeField] private bool stopTimer,stopSpinner;

	[Inject] private GameplayHandler _gameplayHandler;
	
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
			_gameplayHandler.RewardedAd.SetActive (true);
		}
		if (stopTimer) {
			if(!stopSpinner)
				_gameplayHandler.SpinnerImage.fillAmount += 0.004f;
			if (_gameplayHandler.SpinnerImage.fillAmount > 0.99f) {
				_gameplayHandler.LevelFail_CompleteStatusEvent (false);
				gameObject.SetActive (false);
			}
		}
	}

}
