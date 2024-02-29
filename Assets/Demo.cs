using UnityEngine;


public class Demo : MonoBehaviour {


	public GameObject internetPanel;
	
	
	public static Demo instance;
    private void Start()
    {
		instance = this;

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
