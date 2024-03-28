using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class MainMenuHandler : MonoBehaviour 
{
    [Header("Loading Screen")]
    [SerializeField] private bool isLoading;
    [SerializeField] private GameObject logo;
    [SerializeField] private Sprite[] loadingImages;
    [FormerlySerializedAs("LoadingBar")] [SerializeField] private Image loadingBar;
    [SerializeField] private Text loadingText;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private float loadingDelayTime;

    [Header("Main Menu Stuff")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject modeSelectionPanel;
    [SerializeField] private GameObject customizationPanel;
    [SerializeField] private GameObject learningModePanel;
    [SerializeField] private Scrollbar[] scroll;
    [SerializeField] private GameObject parkLevelSelection, houseLevelSelection;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Image soundButton;
    [SerializeField] private Sprite soundOn, soundOff;
    private bool _soundState = true;

    [Header("Levels Parent")]
    [Tooltip("Just Assign the parent the levels array will be populated automatically")]
    [SerializeField] private GameObject parkLevelsParent;
    [SerializeField] private GameObject[] parkLevels;
    
    [SerializeField] private GameObject houseLevelsParent;
    [SerializeField] private GameObject[] houseLevels;
    [SerializeField] private int totalParkLevelsUnlocked, totalHouseLevelsUnlocked;

    [FormerlySerializedAs("UserCoins")]
    [Header("Text Fields to Display User Coins")]
    [SerializeField] private Text[] userCoins;
    [SerializeField] private int coinsToAdd;
    [SerializeField] private SelectionStoreHandler selectionStoreHandler;

    [Header("InApp Buttons")]
    [SerializeField] private Button removeAdsButton;
    [SerializeField] private Button unlockLevelsButton;
    [SerializeField] private Button unlockCostumesButton;
    [SerializeField] private Button unlockEverythingButton;

    [Inject] private SoundManager _soundManager;
    [Inject] private StoreHandler _storeHandler;
    
    private void Start()
    {
        if (PlayerPrefs.GetInt("ShowLearningMode") == 1)
        {
            ShowLearningModePanel();
        }
        OnStart();
    }
    private void LateUpdate()
    {
        if (isLoading)
        {
            loadingBar.fillAmount += 0.006f;
            if (loadingBar.fillAmount == 1)
            {
                isLoading = false;
            }
        }
    }

    private void OnStart()
    {
        Time.timeScale = 1f;
       _soundManager.PlayMainMenuSounds(1f);
      
       // StartCheckInappStats();
       // CheckEveryThingUnlocked();
        if (PlayerPrefs.GetInt("ComingFromGP", 0) == 1)
        {
            PlayerPrefs.SetInt("ComingFromGP", 0);
        }
        _storeHandler.FirstTimeComing(0);
        // StartCoroutine(showLoadingImage());
        UpdateCoins();
  
        parkLevels = new GameObject[parkLevelsParent.transform.childCount];
        for (int i = 0; i < parkLevels.Length; i++)
        {
            parkLevels[i] = parkLevelsParent.transform.GetChild(i).gameObject;
        }
        houseLevels = new GameObject[houseLevelsParent.transform.childCount];
        for (int i = 0; i < houseLevels.Length; i++)
        {
            houseLevels[i] = houseLevelsParent.transform.GetChild(i).gameObject;
        }
    }
    
    private IEnumerator ShowLoadingImage()
    {
        loadingPanel.SetActive(true);
        isLoading = true;
        loadingBar.fillAmount = 0;
        
        yield return new WaitForSeconds(loadingDelayTime);
        loadingPanel.SetActive(false);
    }

    public void UpdateCoins()
    {
        foreach (var t in userCoins)
        {
            t.text = _storeHandler.GetTotalEarnedCoins().ToString();
        }
    }
    
    public void ShowModeSelection()
    {
        modeSelectionPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        customizationPanel.SetActive(false);
        ShowAds();  
    }
    public void BackFromModeSelection()
    {
        modeSelectionPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        customizationPanel.SetActive(false);
    }

    public void ShowLevelSelection()
    {
        InitLevelSelection();
        modeSelectionPanel.SetActive(false);
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
        {
            parkLevelSelection.SetActive(true);
            houseLevelSelection.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("SelectedMode") == 1)
        {
            parkLevelSelection.SetActive(false);
            houseLevelSelection.SetActive(true);
        }
    }

    public void BackFromLevelSelection()
    {
        parkLevelSelection.SetActive(false);
        modeSelectionPanel.SetActive(true);
    }

    public void ShowCustomizationPanel()
    {
        customizationPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void ShopPanel()
    {
        shopPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }
    public void BackFromShopPanel()
    {
        shopPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    public void ShowLearningModePanel()
    {
        learningModePanel.SetActive(true);
        modeSelectionPanel.SetActive(false);
    }
    public void BackFromLearningModePanel()
    {
        learningModePanel.SetActive(false);
        modeSelectionPanel.SetActive(true);
    }

    public void PlayButtonClickSound()
    {
       _soundManager.PlayButtonClickSound();
    }

    private void InitLevelSelection()
    {
        totalParkLevelsUnlocked = PlayerPrefs.GetInt("TotalLevelsUnlocked");
        for (int i = 0; i <= totalParkLevelsUnlocked; i++)
        {
            parkLevels[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        totalHouseLevelsUnlocked = PlayerPrefs.GetInt("TotalHouseLevelsUnlocked");
        for (int i = 0; i <= totalHouseLevelsUnlocked; i++)
        {
            houseLevels[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    
    public void SelectLearningMode(int mode)
    {
        PlayerPrefs.SetInt("SelectedLearningMode", mode);
        StartCoroutine(LoadScene(2));
        PlayerPrefs.SetInt("ShowLearningMode", 0);
    }
    
    public void SelectParkLevel(int temp)
    {
        if (!parkLevels[temp].transform.GetChild(1).gameObject.activeSelf) {
            
            PlayerPrefs.SetInt("SelectedLevel", temp);
            StartCoroutine(LoadScene(1));
        }
    } 
    
    public void SelectHouseLevel(int temp)
    {
        if (!houseLevels[temp].transform.GetChild(1).gameObject.activeSelf) {
            
            PlayerPrefs.SetInt("SelectedHouseLevel", temp);
            StartCoroutine(LoadScene(3));
        }
    }
    
    public void LoadingScreenEffect()
    {
       // logo.GetComponent<Coffee.UIExtensions.UITransitionEffect>().Show(true);
    }
    
    private IEnumerator LoadScene(int scene)
    {
        int temp = Random.Range(0, loadingImages.Length);
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<Image>().sprite = loadingImages[temp];
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        while (!asyncOperation.isDone)
        {
            
            loadingText.text = (int)(asyncOperation.progress * 100) + "%";
            if (loadingBar) loadingBar.fillAmount = asyncOperation.progress;

            yield return null;
        }
    }

    public void ChangeSliderValue(int temp)
    {
        if (temp == 0)
        {
            scroll[1].value = 0f;
            //Debug.Log("Slider 02 Value Changed...");
        }
        else if (temp == 1)
        {
            scroll[1].value = 1f;
            //Debug.Log("Slider 02 Value Changed...");
        }
        else if (temp == 2)
        {
            scroll[0].value = 0f;
            //Debug.Log("Slider 02 Value Changed...");
        }else if (temp == 3)
        {
            scroll[0].value = 1f;
            //Debug.Log("Slider 02 Value Changed...");
        }
    }

    public void SoundToggle()
    {
        _soundState = !_soundState;
        if (_soundState) {
            soundButton.sprite = soundOn;
            AudioListener.volume = 1;
        }
        else if (!_soundState) {
            soundButton.sprite = soundOff;
            AudioListener.volume = 0;
        }
    }
    
    public void ExitBtn()
    {
        Application.Quit();
    }
 
    public void CoinsInApp()
    {
        UpdateCoins();
    }
    
    //All InApp Functions
    public void AddDiamonds(int diamond)
    {
        if (_storeHandler)
        {
            _storeHandler.SetTotalEarnedCoins(_storeHandler.GetTotalEarnedCoins() + diamond);
            UpdateCoins();
        }
    }
    
    public void UnlockAllLevels()
    {
        for (int i = 0; i < parkLevels.Length; i++)
        {
            PlayerPrefs.SetInt("TotalLevelsUnlocked", parkLevels.Length - 1);
            InitLevelSelection();
        }
        unlockLevelsButton.interactable = false;
    }

    private void UnlockAllCostumes()
    {
        //Unlock Girl Clothes
        for (int i = 0; i < 18; i++)
        {
            PlayerPrefs.SetInt("UnlockedGirlItems" + i, 1);
        }
        //Unlock Boy Clothes
        for (int i = 0; i < 8; i++)
        {
            PlayerPrefs.SetInt("UnlockedBoyItems" + i, 1);
        }
        //Unlock Mother Clothes
        for (int i = 0; i < 12; i++)
        {
            PlayerPrefs.SetInt("UnlockedMotherItems" + i, 1);
        }
        unlockCostumesButton.interactable = false;
    }
    
    public void UnlockEverything()
    {
        UnlockAllLevels();
        UnlockAllCostumes();
        removeAdsButton.interactable = false;
        unlockEverythingButton.interactable = false;
    }


    public void OpenLink(string url)
    {
        Application.OpenURL(url);

    }
    public void ShowAds()
    {
       // AdsManager_ZL.instance.CallInterstitialAd(Adspref.Menu);
    }
    
    // #region Code To Un Comment
    //void InAppPurchaseManager_PurchaseSucessFull(string temp)
    //{
    //    if (InAppManager.inAppManager.inApps[0].id.Equals(temp))
    //    {
    //        AdsManager.adsManager.removeAllAds();
    //        for (int i = 0; i < RemoveAdsBtns.Length; i++)
    //        {
    //            RemoveAdsBtns[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    if (InAppManager.inAppManager.inApps[1].id.Equals(temp))
    //    {
    //        PlayerPrefs.SetInt("TotalLevelsUnlocked", park_Levels.Length - 1);
    //        PlayerPrefs.SetInt("TotalHouseLevelsUnlocked", park_Levels.Length - 1);
    //        initLevelSelection();
    //        for (int i = 0; i < unlockAllButton.Length; i++)
    //        {
    //            unlockAllButton[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    if (InAppManager.inAppManager.inApps[2].id.Equals(temp))
    //    {
    //        PlayerPrefs.SetInt("CharUnlocked", 1);
    //        selectionStoreHandler.UnlockAllGuns();
    //        for (int i = 0; i < unlockAllChars.Length; i++)
    //        {
    //            unlockAllChars[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    if (InAppManager.inAppManager.inApps[3].id.Equals(temp))
    //    {
    //        PlayerPrefs.SetInt("UnlockEveryThing", 1);
    //        UnlockEveryThing.GetComponent<Button>().interactable = false;
    //        PlayerPrefs.SetInt("TotalLevelsUnlocked", park_Levels.Length - 1);
    //        PlayerPrefs.SetInt("TotalHouseLevelsUnlocked", park_Levels.Length - 1);
    //        initLevelSelection();
    //        for (int i = 0; i < unlockAllButton.Length; i++)
    //        {
    //            unlockAllButton[i].GetComponent<Button>().interactable = false;
    //        }
    //        AdsManager.adsManager.removeAllAds();
    //        for (int i = 0; i < RemoveAdsBtns.Length; i++)
    //        {
    //            RemoveAdsBtns[i].GetComponent<Button>().interactable = false;
    //        }
    //        PlayerPrefs.SetInt("CharUnlocked", 1);
    //        selectionStoreHandler.UnlockAllGuns();
    //        for (int i = 0; i < unlockAllChars.Length; i++)
    //        {
    //            unlockAllChars[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    CheckEveryThingUnlocked();
    //}
    //public void removeAds()
    //{
    //    InAppManager.inAppManager.BuyInApp(InAppManager.inAppManager.inApps[0].id);
    //}

    //public void unlockLevels()
    //{
    //    InAppManager.inAppManager.BuyInApp(InAppManager.inAppManager.inApps[1].id);
    //}
    //public void unlockAllCharacters()
    //{
    //    InAppManager.inAppManager.BuyInApp(InAppManager.inAppManager.inApps[2].id);
    //}
    //public void UnlockEverything()
    //{
    //    InAppManager.inAppManager.BuyInApp(InAppManager.inAppManager.inApps[3].id);
    //}

    //public void Showad()
    //{
    //    if (AdsManager.adsManager)
    //    {
    //     //   AdsManager.adsManager.ShowAdMob();
    //    }
    //}
    //public void ShowNative()
    //{
    //    if (AdsManager.adsManager)
    //    {
    //        AdsManager.adsManager.ShowNativeAd();
    //    }
    //}
    //void CheckEveryThingUnlocked()
    //{
    //    if (PlayerPrefs.GetInt("TotalLevelsUnlocked") == park_Levels.Length - 1 && PlayerPrefs.GetInt("TotalLevelsUnlocked") == park_Levels.Length - 1 && AdsManager.adsManager.areadsRemoved && PlayerPrefs.GetInt("CharUnlocked") == 1)
    //    {
    //        UnlockEveryThing.GetComponent<Button>().interactable = false;
    //        initLevelSelection();
    //        for (int i = 0; i < unlockAllButton.Length; i++)
    //        {
    //            unlockAllButton[i].GetComponent<Button>().interactable = false;
    //        }
    //        for (int i = 0; i < RemoveAdsBtns.Length; i++)
    //        {
    //            RemoveAdsBtns[i].GetComponent<Button>().interactable = false;
    //        }
    //        for (int i = 0; i < unlockAllChars.Length; i++)
    //        {
    //            unlockAllChars[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //}
    //public void StartCheckInappStats()
    //{
    //    if (AdsManager.adsManager.areadsRemoved)
    //    {
    //        for (int i = 0; i < RemoveAdsBtns.Length; i++)
    //        {
    //            RemoveAdsBtns[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    if (PlayerPrefs.GetInt("TotalLevelsUnlocked") == park_Levels.Length - 1 && PlayerPrefs.GetInt("TotalLevelsUnlocked") == park_Levels.Length - 1)
    //    {
    //        for (int i = 0; i < unlockAllButton.Length; i++)
    //        {
    //            unlockAllButton[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    if (PlayerPrefs.GetInt("CharUnlocked") == 1)
    //    {
    //        for (int i = 0; i < unlockAllChars.Length; i++)
    //        {
    //            unlockAllChars[i].GetComponent<Button>().interactable = false;
    //        }
    //    }
    //    if (PlayerPrefs.GetInt("UnlockEveryThing") == 1)
    //    {
    //        CheckEveryThingUnlocked();
    //    }
    //}

    //public void OpenPrivacyLink()
    //{

    //    AdsManager.adsManager.ShowPrivacyLink();
    //}
    //public void OpenMoreapps()
    //{
    //    AdsManager.adsManager.OpenMoreAppsLink();
    //}

    //public void rateUsApp()
    //{
    //    AdsManager.adsManager.rateUsApp();
    //}
    //#endregion

}
