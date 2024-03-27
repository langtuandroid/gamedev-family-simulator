using UnityEngine;


public class Demo : MonoBehaviour {


	public GameObject internetPanel;
	
	private static Demo _instance;
    private void Start()
    {
		_instance = this;

		DontDestroyOnLoad(gameObject);
		CheckInternet();
	}


	public void CheckInternet()
    {
		if (Application.internetReachability != NetworkReachability.NotReachable)
		{
			internetPanel.SetActive(false);
		}
		else
		{
			internetPanel.SetActive(true);
		}
	}
	
}
