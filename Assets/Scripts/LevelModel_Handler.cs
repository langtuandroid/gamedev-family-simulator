using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LevelModelHandler : MonoBehaviour {

	[Header("Level Objectives Objectives")]
	[SerializeField] private string primaryObjective;
	[SerializeField] private string[] secondaryObjectives;
	[SerializeField] private GameObject mother;
	[SerializeField] private RuntimeAnimatorController[] motherControllers;
	[FormerlySerializedAs("ShowObjective")] [Space(20)]
	[SerializeField] private UnityEvent showObjective;

	public string PrimaryObjective => primaryObjective;

	private void Start()
	{
		showObjective.Invoke();
		GameplayHandler.Gsh.SetScaleOne();
	}


	public void ShowSecondaryObjective(int secondary)
    {
		GameplayHandler.Gsh.SecondaryText.text = secondaryObjectives[secondary];
    }

	public void ChangeMotherController()
    {
		mother.GetComponent<Animator>().runtimeAnimatorController = motherControllers[1] as RuntimeAnimatorController;
    }
}
