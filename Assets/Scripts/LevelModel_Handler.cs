using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;

public class LevelModelHandler : MonoBehaviour {

	[Header("Level Objectives Objectives")]
	[SerializeField] private string primaryObjective;
	[SerializeField] private string[] secondaryObjectives;
	[SerializeField] private GameObject mother;
	[SerializeField] private RuntimeAnimatorController[] motherControllers;
	[FormerlySerializedAs("ShowObjective")] [Space(20)]
	[SerializeField] private UnityEvent showObjective;

	[Inject] private GameplayHandler _gameplayHandler;

	public string PrimaryObjective => primaryObjective;

	private void Start()
	{
		showObjective.Invoke();
		_gameplayHandler.SetScaleOne();
	}


	public void ShowSecondaryObjective(int secondary)
    {
		_gameplayHandler.SecondaryText.text = secondaryObjectives[secondary];
    }

	public void ChangeMotherController()
    {
		mother.GetComponent<Animator>().runtimeAnimatorController = motherControllers[1] as RuntimeAnimatorController;
    }
}
