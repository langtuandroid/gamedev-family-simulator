using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using Facebook.Unity;

public class MainMenuScriptHandler : MonoBehaviour 
{
    // ----------- Static Ref. of Main Menu Handler Start------------//
    public static MainMenuScriptHandler mmsh;
    // ----------- Static Ref. of Main Menu Handler End------------//


    [Header("Loading Screen")]
    public bool isLoading;
    public GameObject logo;
    [SerializeField] Sprite[] loadingImages;
    public Image LoadingBar;
    public Text loading_Text;
    public GameObject loadingPanel;
    public float loadingDelayTime;

    [Header("Main Menu Stuff")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject modeSelectionPanel;
    [SerializeField] GameObject customizationPanel;
    [SerializeField] GameObject learnigModePanel;
    [SerializeField] Scrollbar[] scroll;
    public GameObject park_levelSelection, house_levelSelection;
    public GameObject shopPanel;
    public Image soundButton;
    public Sprite soundOn, SoundOff;
    bool soundState = true;

    [Header("Levels Parent")]
    [Tooltip("Just Assign the parent the levels array will be populated automatically")]
    public GameObject park_levelsParent;
    public GameObject[] park_Levels;
    
    public GameObject house_levelsParent;
    public GameObject[] house_Levels;
    [SerializeField]
    int totalParkLevelsUnlocked, totalHouseLevelsUnlocked;

    [Header("Text Fields to Display User Coins")]
    public Text[] UserCoins;
    public int coinstoadd;
    public SelectionStoreHandler selectionStoreHandler;

    [Header("InApp Buttons")]
    [SerializeField] Button removeAdsButton;
    [SerializeField] Button unlockLevelsButton;
    [SerializeField] Button unlockCostumesButton;
    [SerializeField] Button unlockEverythingButton;

    void Start()
    {
        if (mmsh == null)
        {
            mmsh = this;
        }
        if (PlayerPrefs.GetInt("ShowLearningMode") == 1)
        {
            ShowLearingModePanel();
        }
        OnStart();
       // FirebaseNotificationController.instance.FireBaseScreenChecker("Main Menu");
    }

    //private void OnEnable()
    //{
    //    AdsManager.onRewardedAdShown += HandleOnRewardedVideoCompleted;
    //    AdsManager.rewarded_Admobe_Shown += HandleRewardBasedVideoRewarded;
    //    InAppManager.onPurchaseWasSucessFull += InAppPurchaseManager_PurchaseSucessFull;
      
    //}
    //private void OnDisable()
    //{
    //    AdsManager.onRewardedAdShown -= HandleOnRewardedVideoCompleted;
    //    AdsManager.rewarded_Admobe_Shown -= HandleRewardBasedVideoRewarded;
    //    InAppManager.onPurchaseWasSucessFull -= InAppPurchaseManager_PurchaseSucessFull;
       
    //}
    private void LateUpdate()
    {
        if (isLoading)
        {
            LoadingBar.fillAmount += 0.006f;
            if (LoadingBar.fillAmount == 1)
            {
                isLoading = false;
            }
        }
    }
   

    public void OnStart()
    {
        Time.timeScale = 1f;
        SoundManager._SoundManager.playMainMenuSounds(1f);
      
       // StartCheckInappStats();
       // CheckEveryThingUnlocked();
        if (PlayerPrefs.GetInt("ComingFromGP", 0) == 1)
        {
            PlayerPrefs.SetInt("ComingFromGP", 0);
        }
        StoreScriptHandler.storeScript.firstTimeComing(0);
        // StartCoroutine(showLoadingImage());
        updateCoins();
       // initLevelSelection();
        park_Levels = new GameObject[park_levelsParent.transform.childCount];
        for (int i = 0; i < park_Levels.Length; i++)
        {
            park_Levels[i] = park_levelsParent.transform.GetChild(i).gameObject;
        }
        house_Levels = new GameObject[house_levelsParent.transform.childCount];
        for (int i = 0; i < house_Levels.Length; i++)
        {
            house_Levels[i] = house_levelsParent.transform.GetChild(i).gameObject;
        }
    }
    
    IEnumerator showLoadingImage()
    {
        loadingPanel.SetActive(true);
        isLoading = true;
        LoadingBar.fillAmount = 0;
      //  ShowNative();
        yield return new WaitForSeconds(loadingDelayTime);
        loadingPanel.SetActive(false);
       // Showad();
       // ShowNative();
        
    }

    public void updateCoins()
    {
        for (int i = 0; i < UserCoins.Length; i++)
        {

            UserCoins[i].text = StoreScriptHandler.storeScript.getTotalEarnedCoins().ToString();
        }
    }

    public void Admob_Unity_Reward_Show()
    {
        //if (AdsManager.adsManager)
        //{
        //    AdsManager.adsManager.Admob_Reward_Video_Show();
        //}
        
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
        initLevelSelection();
        modeSelectionPanel.SetActive(false);
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
        {
            park_levelSelection.SetActive(true);
            house_levelSelection.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("SelectedMode") == 1)
        {
            park_levelSelection.SetActive(false);
            house_levelSelection.SetActive(true);
        }
      //  Showad();   
    }

    public void BackFromLevelSelection()
    {
        park_levelSelection.SetActive(false);
        modeSelectionPanel.SetActive(true);
    }

    public void ShowCustomizationPanel()
    {
        customizationPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
       // Showad();
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

    public void ShowLearingModePanel()
    {
        learnigModePanel.SetActive(true);
        modeSelectionPanel.SetActive(false);
    }
    public void BackFromLearingModePanel()
    {
        learnigModePanel.SetActive(false);
        modeSelectionPanel.SetActive(true);
    }

    public void PlayButtonClickSound()
    {
        SoundManager._SoundManager.playButtonClickSound();
    }

    void initLevelSelection()
    {
        totalParkLevelsUnlocked = PlayerPrefs.GetInt("TotalLevelsUnlocked");
        for (int i = 0; i <= totalParkLevelsUnlocked; i++)
        {
            park_Levels[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        totalHouseLevelsUnlocked = PlayerPrefs.GetInt("TotalHouseLevelsUnlocked");
        for (int i = 0; i <= totalHouseLevelsUnlocked; i++)
        {
            house_Levels[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    
    public void SelectLearningMode(int mode)
    {
        PlayerPrefs.SetInt("SelectedLearningMode", mode);
        StartCoroutine(LoadScene(3));
        PlayerPrefs.SetInt("ShowLearningMode", 0);
    }
    
    public void selectParkLevel(int temp)
    {
        if (!park_Levels[temp].transform.GetChild(1).gameObject.activeSelf) {
            
            PlayerPrefs.SetInt("SelectedLevel", temp);
            StartCoroutine(LoadScene(2));
        }
    } public void selectHouseLevel(int temp)
    {
        if (!house_Levels[temp].transform.GetChild(1).gameObject.activeSelf) {
            
            PlayerPrefs.SetInt("SelectedHouseLevel", temp);
            StartCoroutine(LoadScene(3));
        }
    }
    
    public void LoadingScreenEffect()
    {
       // logo.GetComponent<Coffee.UIExtensions.UITransitionEffect>().Show(true);
    }
    IEnumerator LoadScene(int scene)
    {
        int temp = Random.Range(0, loadingImages.Length);
        loadingPanel.SetActive(true);
        loadingPanel.GetComponent<Image>().sprite = loadingImages[temp];
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        while (!asyncOperation.isDone)
        {
            
            loading_Text.text = (int)(asyncOperation.progress * 100) + "%";
            if (LoadingBar) LoadingBar.fillAmount = asyncOperation.progress;

            yield return null;
        }
       // Showad();
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
        soundState = !soundState;
        if (soundState) {
            soundButton.sprite = soundOn;
            AudioListener.volume = 1;
        }
        else if (!soundState) {
            soundButton.sprite = SoundOff;
            AudioListener.volume = 0;
        }
    }


    public void Exitbtn()
    {
        Application.Quit();
    }
 
    public void CoinsInApp()
    {
        updateCoins();
    }


    //All InApp Functions
    public void AddDiamonds(int diamond)
    {
        if (StoreScriptHandler.storeScript)
        {
            StoreScriptHandler.storeScript.setTotalEarnedCoins(StoreScriptHandler.storeScript.getTotalEarnedCoins() + diamond);
            updateCoins();
        }
    }
    public void UnlockAllLevels()
    {
        for (int i = 0; i < park_Levels.Length; i++)
        {
            PlayerPrefs.SetInt("TotalLevelsUnlocked", park_Levels.Length - 1);
            initLevelSelection();
        }
        unlockLevelsButton.interactable = false;
    }
    public void UnlockAllCostumes()
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
