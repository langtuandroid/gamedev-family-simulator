using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

public class SelectionStoreHandler : MonoBehaviour {

	[Header("Objects to be Selected")]
	[SerializeField] private GameObject[] objectSelected;
	[SerializeField] private int currentSelected;
	
	[Header("Assign Camera Looking At player")]
	[SerializeField] private GameObject cameraLookingAtPlayer;
	
	[Header("Left Right Button")]
	[SerializeField] private GameObject leftButton;
	[SerializeField] private GameObject rightButton;
	
	[Header("Girl Outfit")]
	[SerializeField] private GameObject[] customizePanel;
	[SerializeField] private Texture[] girlHair;
	[SerializeField] private Texture[] girlEyes;
	[SerializeField] private Texture[] girlDress;
	[FormerlySerializedAs("fashion_Items")] [SerializeField] private Texture[] fashionItems;
	[SerializeField] private GameObject[] girlHairMeshes;
	[SerializeField] private GameObject[] girlEyeMeshes;
	[SerializeField] private GameObject girlDressMesh, girlHairBandMesh, girlRibbonMesh;
	
	[Tooltip(" 1st material for Girl Hair, 2nd for Girl Eyes, 3rd for Girl Dress & 4th for Girl Hair Band & 7th for Girl Ribbon")]
	[SerializeField] private Material[] girlMaterials;
	[SerializeField] private GameObject[] girlButtons;
	[SerializeField] private GameObject buyPanel;
	[SerializeField] private GameObject notEnoughMoneyPanel;
	private readonly int[]  _girlClothPrices = { 0, 150, 150, 150, 150, 150, 0, 100, 100, 100, 0, 400, 500, 600, 200, 200, 200, 200 };
	private string _buttonName;

	[Header("Boy Outfit")]
	[SerializeField] private GameObject boyHairMesh01;
	[SerializeField] private GameObject boyHairMesh02;
	[SerializeField] private Texture[] boyHair;
	[SerializeField] private GameObject boyLeftEye, boyRightEye;
	[SerializeField] private Texture[] boyEyes;
	[SerializeField] private GameObject[] boyButtons;
	[SerializeField] private Material[] boyMaterials;
	private readonly int[]  _boyClothPrices = { 0, 150, 150, 150, 0, 100, 100, 100 };

	[Header("Mother Outfit")]
	[SerializeField] private GameObject mother;
	[SerializeField] private Texture[] motherHair;
	[SerializeField] private Texture[] motherEyes;
	[SerializeField] private Texture[] motherDress;
	
	[Tooltip("1st Material for Eye, 2nd for Hair & 3rd for Dress")]
	[SerializeField] private Material[] motherMaterials;
	[SerializeField] private GameObject[] motherButtons;
	
	private readonly int[] _motherClothPrices = { 0, 150, 150, 150, 0, 100, 100, 100, 0, 400, 500, 600 };

	[Inject] private MainMenuHandler _mainMenuHandler;
	[Inject] private StoreHandler _storeHandler;
	
	private void Start()
	{
		CheckUnlockedCostumeItems();
		DelayedLeftRightState();
	}

	private void DelayedLeftRightState()
	{
		if (currentSelected < objectSelected.Length - 1) {
			leftButton.SetActive (true);
			rightButton.SetActive (true);
		}
		if (currentSelected == 0) {
			leftButton.SetActive (false);
		}
		if (currentSelected == objectSelected.Length - 1) {
			rightButton.SetActive (false);
		}
	}

	public void CheckUnlockedCostumeItems()
    {
		//Check Girl Unlocked Items...
        if (PlayerPrefs.GetInt("UnlockedGirlItems0") == 0 || PlayerPrefs.GetInt("UnlockedGirlItems6") == 0
			|| PlayerPrefs.GetInt("UnlockedGirlItems10") == 0)
        {
			PlayerPrefs.SetInt("UnlockedGirlItems0", 1);
			PlayerPrefs.SetInt("UnlockedGirlItems6", 1);
			PlayerPrefs.SetInt("UnlockedGirlItems10", 1);
        }
        for (int i = 0; i < girlButtons.Length; i++)
        {
			if (PlayerPrefs.GetInt("UnlockedGirlItems" + i) == 1)
			{
				girlButtons[i].transform.GetChild(1).gameObject.SetActive(false);
            }
		}

		//Check Boy Unlocked Items...
		if (PlayerPrefs.GetInt("UnlockedBoyItems0") == 0 || PlayerPrefs.GetInt("UnlockedBoyItems4") == 0)
        {
			PlayerPrefs.SetInt("UnlockedBoyItems0", 1);
			PlayerPrefs.SetInt("UnlockedBoyItems4", 1);
        }
        for (int i = 0; i < boyButtons.Length; i++)
        {
			if (PlayerPrefs.GetInt("UnlockedBoyItems" + i) == 1)
			{
				boyButtons[i].transform.GetChild(1).gameObject.SetActive(false);
            }
		}
		
		//Check Mother Unlocked Items...
		if (PlayerPrefs.GetInt("UnlockedMotherItems0") == 0 || PlayerPrefs.GetInt("UnlockedMotherItems4") == 0
			|| PlayerPrefs.GetInt("UnlockedMotherItems8") == 0)
        {
			PlayerPrefs.SetInt("UnlockedMotherItems0", 1);
			PlayerPrefs.SetInt("UnlockedMotherItems4", 1);
			PlayerPrefs.SetInt("UnlockedMotherItems8", 1);
        }
        for (int i = 0; i < motherButtons.Length; i++)
        {
			if (PlayerPrefs.GetInt("UnlockedMotherItems" + i) == 1)
			{
				motherButtons[i].transform.GetChild(1).gameObject.SetActive(false);
            }
		}
    }

	public void LeftRightState(bool temp)
	{
		objectSelected [currentSelected].SetActive (false);
		customizePanel[currentSelected].SetActive(false);
		if (temp) {
			currentSelected++;
			
		}
		else
		{
			currentSelected--;
			
		}

		DelayedLeftRightState();
		objectSelected [currentSelected].SetActive (true);
		customizePanel[currentSelected].SetActive(true);
		//		iTween.MoveTo (cameraLookingAtPlayer,new Vector3(objectSelected[currentSelected].transform.position.x,1,-3),3f);
	}

	//Girl Customization...
	public void ChangeGirlHair(int gh)
    {
        for (int i = 0; i < girlHair.Length; i++)
        {
            if (i == gh)
            {
                if (girlButtons[i].transform.GetChild(1).transform.gameObject.activeSelf)
                {
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + gh + "  " + buttonName);
					if (i < 3)
                    {
						girlHairMeshes[0].GetComponent<Renderer>().material.mainTexture = girlHair[i];
						girlHairMeshes[1].GetComponent<Renderer>().material.mainTexture = girlHair[i];
					}
                    else
                    {
						girlHairMeshes[2].GetComponent<Renderer>().material.mainTexture = girlHair[i];
						girlHairMeshes[3].GetComponent<Renderer>().material.mainTexture = girlHair[i];
					}
                }
				else 
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + gh);
                    if (i < 3)
                    {
						PlayerPrefs.SetInt("Hair01", 1);
						PlayerPrefs.SetInt("Hair02", 0);
						girlHairMeshes[0].GetComponent<Renderer>().material.mainTexture = girlHair[i];
						girlHairMeshes[1].GetComponent<Renderer>().material.mainTexture = girlHair[i];
						girlMaterials[0].mainTexture = girlHair[i];
					}
					else
                    {
						PlayerPrefs.SetInt("Hair01", 0);
						PlayerPrefs.SetInt("Hair02", 1);
						girlHairMeshes[2].GetComponent<Renderer>().material.mainTexture = girlHair[i];
						girlHairMeshes[3].GetComponent<Renderer>().material.mainTexture = girlHair[i];
						girlMaterials[1].mainTexture = girlHair[i];
					}
				}
			}
        }
    }
	public void ChangeGirlEyes(int ge)
    {
        for (int i = 0; i < boyEyes.Length; i++)
        {
            if (i == ge)
            {
				if (girlButtons[i + 6].transform.GetChild(1).transform.gameObject.activeSelf)
				{
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + ge + "  " + buttonName);
					girlEyeMeshes[0].GetComponent<Renderer>().material.mainTexture = boyEyes[i];
					girlEyeMeshes[1].GetComponent<Renderer>().material.mainTexture = boyEyes[i];
				}
				else
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + ge);
					girlEyeMeshes[0].GetComponent<Renderer>().material.mainTexture = boyEyes[i];
					girlEyeMeshes[1].GetComponent<Renderer>().material.mainTexture = boyEyes[i];

					girlMaterials[2].mainTexture = boyEyes[i];
					girlMaterials[2].mainTexture = boyEyes[i];
				}
			}
        }
    }
	public void ChangeGirlDress(int gd)
    {
        for (int i = 0; i < girlDress.Length; i++)
        {
            if (i == gd)
            {
				if (girlButtons[i + 10].transform.GetChild(1).transform.gameObject.activeSelf)
				{
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + gd + "  " + buttonName);
					girlDressMesh.GetComponent<Renderer>().material.mainTexture = girlDress[i];
				}
				else
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + gd);
					girlDressMesh.GetComponent<Renderer>().material.mainTexture = girlDress[i];
					girlMaterials[3].mainTexture = girlDress[i];
				}
			}
        }
    }
	public void ChangeGirlFashion_Items(int f)
    {
        for (int i = 0; i < fashionItems.Length; i++)
        {
            if (i == f)
            {
				if (girlButtons[i + 14].transform.GetChild(1).transform.gameObject.activeSelf)
				{
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + f + "  " + buttonName);
					if (i < 2)
                    {
						girlHairBandMesh.GetComponent<Renderer>().material.mainTexture = fashionItems[i];
                    }
                    else
                    {
						girlRibbonMesh.GetComponent<Renderer>().material.mainTexture = fashionItems[i];
					}
				}
				else
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + f);
					if (i < 2)
					{
						PlayerPrefs.SetInt("HairBand", 1);
						PlayerPrefs.SetInt("Ribbon", 0);
						girlHairBandMesh.GetComponent<Renderer>().material.mainTexture = fashionItems[i];
						girlMaterials[4].mainTexture = fashionItems[i];
					}
					else
					{
						PlayerPrefs.SetInt("HairBand", 0);
						PlayerPrefs.SetInt("Ribbon", 1);
						girlRibbonMesh.GetComponent<Renderer>().material.mainTexture = fashionItems[i];
						girlMaterials[5].mainTexture = fashionItems[i];
					}
				}
			}
        }
    }

	//Boy Customization
	public void ChangeBoyHair(int bh)
    {
        for (int i = 0; i < boyHair.Length; i++)
        {
            if (i == bh)
            {
				if (boyButtons[i].transform.GetChild(1).transform.gameObject.activeSelf)
				{
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + bh + "  " + buttonName);
					boyHairMesh01.GetComponent<Renderer>().material.mainTexture = boyHair[i];
					boyHairMesh02.GetComponent<Renderer>().material.mainTexture = boyHair[i];
				}
				else
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + bh);
					boyHairMesh01.GetComponent<Renderer>().material.mainTexture = boyHair[i];
					boyHairMesh02.GetComponent<Renderer>().material.mainTexture = boyHair[i];
					boyMaterials[0].mainTexture = boyHair[i];
				}
			}
        }
    }
	public void ChangeBoyEyes(int be)
    {
        for (int i = 0; i < boyEyes.Length; i++)
        {
            if (i == be)
            {
				if (boyButtons[i + 4].transform.GetChild(1).transform.gameObject.activeSelf)
				{
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + be + "  " + buttonName);
					boyLeftEye.GetComponent<Renderer>().material.mainTexture = boyEyes[i];
					boyRightEye.GetComponent<Renderer>().material.mainTexture = boyEyes[i];
				}
				else
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + be);
					boyLeftEye.GetComponent<Renderer>().material.mainTexture = boyEyes[i];
					boyRightEye.GetComponent<Renderer>().material.mainTexture = boyEyes[i];
					boyMaterials[1].mainTexture = boyEyes[i];

				}
			}
        }
    }

	//Mother Customization
	public void ChangeMotherHair(int mh)
    {
        for (int i = 0; i < motherHair.Length; i++)
        {
            if (i == mh)
            {
				if (motherButtons[i].transform.GetChild(1).gameObject.activeSelf)
				{
                    buyPanel.SetActive(true);
                    _buttonName = EventSystem.current.currentSelectedGameObject.name;
                    //Debug.Log("If Part... " + mh + "  " + buttonName);
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[1].mainTexture = motherHair[i];
                }
                else
				{
                    buyPanel.SetActive(false);
                    //Debug.Log("else Part... " + mh);
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[1].mainTexture = motherHair[i];
					motherMaterials[1].mainTexture = motherHair[i];
				}
            }
        }
    }
	public void ChangeMotherEyes(int me)
    {
        for (int i = 0; i < motherEyes.Length; i++)
        {
            if (i == me)
            {
				if (motherButtons[i + 4].transform.GetChild(1).gameObject.activeSelf) 
				{
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + me + "  " + buttonName);
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[0].mainTexture = motherEyes[i];
				}
				else
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + me);
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[0].mainTexture = motherEyes[i];
					motherMaterials[0].mainTexture = motherEyes[i];
				}
			}
        }
    }
	public void ChangeMotherDress(int md)
    {
        for (int i = 0; i < motherDress.Length; i++)
        {
            if (i == md)
            {
				if (motherButtons[i + 8].transform.GetChild(1).gameObject.activeSelf)
				{
					buyPanel.SetActive(true);
					_buttonName = EventSystem.current.currentSelectedGameObject.name;
					//Debug.Log("If Part... " + md + "  " + buttonName);
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[2].mainTexture = motherDress[i];
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[3].mainTexture = motherDress[i];
				}
				else
				{
					buyPanel.SetActive(false);
					//Debug.Log("else Part... " + md);
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[2].mainTexture = motherDress[i];
					mother.transform.GetChild(0).transform.GetChild(0).GetComponent<Renderer>().materials[3].mainTexture = motherDress[i];
					motherMaterials[2].mainTexture = motherDress[i];
					motherMaterials[3].mainTexture = motherDress[i];
				}
			}
        }
    }

	// Buy All Player's Items
	public void BuyGirlClothes()
	{
		//Outer if(Buy Girl Clothes)
        if (customizePanel[1].activeSelf)
        {
			for (int i = 0; i < girlButtons.Length; i++)
			{
				if (_buttonName == girlButtons[i].name)
				{
					if (_storeHandler && _storeHandler.GetTotalEarnedCoins() >= _girlClothPrices[i])
					{
						_storeHandler.SetTotalEarnedCoins(_storeHandler.GetTotalEarnedCoins() - _girlClothPrices[i]);
						//Debug.Log("Successfully Purchased...");
						girlButtons[i].transform.GetChild(1).gameObject.SetActive(false);
						PlayerPrefs.SetInt("UnlockedGirlItems" + i, 1);
						buyPanel.SetActive(false);
						_mainMenuHandler.UpdateCoins();
					}
					else
					{
						buyPanel.SetActive(false);
						notEnoughMoneyPanel.SetActive(true);
					}
				}
			}
		}//end of Outer if

		//(Buy Boy Clothes)
		else if (customizePanel[0].activeSelf)//1st else if
        {
			for (int i = 0; i < boyButtons.Length; i++)
			{
				if (_buttonName == boyButtons[i].name)
				{
					if (_storeHandler && _storeHandler.GetTotalEarnedCoins() >= _boyClothPrices[i])
					{
						_storeHandler.SetTotalEarnedCoins(_storeHandler.GetTotalEarnedCoins() - _boyClothPrices[i]);
						//Debug.Log("Successfully Purchased...");
						boyButtons[i].transform.GetChild(1).gameObject.SetActive(false);
						PlayerPrefs.SetInt("UnlockedBoyItems" + i, 1);
						buyPanel.SetActive(false);
						_mainMenuHandler.UpdateCoins();
					}
					else
					{
						buyPanel.SetActive(false);
						notEnoughMoneyPanel.SetActive(true);
					}
				}
			}
		}//end of 1st else if

		//(Buy Mother Clothes)
		else if (customizePanel[2].activeSelf)//2nd else if
        {
			for (int i = 0; i < motherButtons.Length; i++)
			{
				if (_buttonName == motherButtons[i].name)
				{
					if (_storeHandler && _storeHandler.GetTotalEarnedCoins() >= _motherClothPrices[i])
					{
						_storeHandler.SetTotalEarnedCoins(_storeHandler.GetTotalEarnedCoins() - _motherClothPrices[i]);
						//Debug.Log("Successfully Purchased...");
						motherButtons[i].transform.GetChild(1).gameObject.SetActive(false);
						PlayerPrefs.SetInt("UnlockedMotherItems" + i, 1);
						buyPanel.SetActive(false);
						_mainMenuHandler.UpdateCoins();
					}
					else
					{
						buyPanel.SetActive(false);
						notEnoughMoneyPanel.SetActive(true);
					}
				}
			}
		}
		
	}

}
