using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelModel_Handler : MonoBehaviour {

	[Header("Level Objectives Objectives")]
	public string primaryObjective;
	public string[] secondaryObjectives;
	[SerializeField] GameObject mother;
	[SerializeField] RuntimeAnimatorController[] motherControllers;
	//[Header("Cut Scene Init")]
	//public bool containsCutScene;
	//public bool skipCutScene;
	//public float cutSceneDuration;
	//public UnityEngine.Events.UnityEvent onCutSceneEndEvent;
	//[Header("Tmer per level aloted")]
	//public float timeAloted;
	//public TimerScript timerScript;
	[Space(20)]
	public UnityEvent ShowObjective;

	void Start()
	{
		ShowObjective.Invoke();
		GameplayScript_Handler.gsh.SetScaleOne();
		//timerScript.time = timeAloted;
		//timerScript.timerText = GameplayScript_Handler.gsh.timerText;
	}


	public void ShowSecondaryObjective(int secondary)
    {
		GameplayScript_Handler.gsh.secondaryText.text = secondaryObjectives[secondary];
    }

	public void ChangeMotherController()
    {
		mother.GetComponent<Animator>().runtimeAnimatorController = motherControllers[1] as RuntimeAnimatorController;
    }


	//public IEnumerator cutSceneCalled()
	//{
	//	yield return new WaitForSeconds (cutSceneDuration);
	//	//if (!skipCutScene) {
	//	//	GameplayScript_Handler.gsh.showButtons ();
	//	//	GameplayScript_Handler.gsh.ObjectiveDialoug ();
	//	//	//onCutSceneEndEvent.Invoke ();
	//	//}
	//}

	//public void skipCutSceneHandler()
	//{
	//	//skipCutScene = true;
	//	GameplayScript_Handler.gsh.showButtons ();
	//	GameplayScript_Handler.gsh.ObjectiveDialoug ();
	//	//onCutSceneEndEvent.Invoke ();
	//}
}
