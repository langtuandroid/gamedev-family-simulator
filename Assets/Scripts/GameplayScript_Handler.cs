using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Invector.vCamera;
using Invector;
//using Facebook.Unity;

public class GameplayScript_Handler : MonoBehaviour {

    public bool isTesting;
    public int testingLevel;
    [Header("On Scene Load Setting")]
    [SerializeField] GameObject env;
    [Space(5)]
    [SerializeField] GameObject hair01, g02Hair01, g03Hair01;
    [Space(5)]
    [SerializeField] GameObject hair02, g02Hair02, g03Hair02;
    [Space(5)]
    [SerializeField] GameObject hairBand, g02HairBand, g03HairBand;
    [Space(5)]
    [SerializeField] GameObject Ribbon, g02Ribbon, g03Ribbon;
    [Space(10)]
    [SerializeField] vThirdPersonCamera motherCamera;
    [SerializeField] vThirdPersonCamera childCamera;
    [SerializeField] GameObject fadeImage;
    [SerializeField] vThirdPersonCameraListData childList;
    [SerializeField] GameObject vUI;
    [SerializeField] Button skipButton;
    [Header("Objective Dialoug Screen")]
    public GameObject objDialoug;
    public Text objText,secondaryText;
    [Header("Gameplay Buttons")]
    public GameObject[] gpButtons;
    [Header("Loading Screen")]
    public GameObject loadingScreen;
    public Sprite[] loadingBackgrounds;
    public float loadingScreenDisplayTime;
    public Image LoadingImage;
    public bool Isloading;
    public Text loading_Text;
    [Header("Level Pause Screen")]
    public GameObject pauseScreen;
    [Header("Level Failed Screen")]
    public GameObject failedScreen;
    [Header("Level Complete Screen")]
    public GameObject completeScreen;

    [Header("Assign ind. level prefab.")]
    public GameObject levelsParent;
    public GameObject[] Levels;

    [Header("Other Varaibles")]
    bool displayAdOnce, primaryObjectives;
    [SerializeField]
    bool levelCompleteTest, levelFailedTest;
    public int selectedLevel, unlockedLevel;
    public GameObject skipCutScene;
    public Text totalCoinsEarned, timerText, coinText;
    GameObject spawnedLevel;
    LevelModel_Handler currentLevelModel;
    int totalCoins;
    [Header("Rewarded Ad - Timer")]
    public GameObject rewardedAd;
    public Image spinnerImage;
    public float timeToBeAlotedAfterRewardedVideo;

    //[Header("Cut Scenes...")]
    //[SerializeField] GameObject[] levelCutScenes; 
    // ----------- Static Ref. of GamePlay Script Handler Start------------//
    public static GameplayScript_Handler gsh;
    // ----------- Static Ref. of GamePlay Script Handler End------------//
    
    void Awake()
    {
        Time.timeScale = 1f;
        if (gsh == null) {
            gsh = this;
        }
        hideButtons();
    }

    void Start()
    {
        // if (AdsManager_ZL.instance)
        // {
        //     AdsManager_ZL.instance.CallInterstitialAd(Adspref.Menu);
        // }
        if (isTesting)
        {
            PlayerPrefs.SetInt("SelectedLevel", testingLevel);
        }

        env.SetActive(true);
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayGameplaySounds(0.65f);
        }
        if (PlayerPrefs.GetInt("ComingFromGP", 0) == 0) {
            PlayerPrefs.SetInt("ComingFromGP", 1);
        }
        spawnLevel();
        //StartCoroutine(loadingScreenHandler());

        if (StoreScriptHandler.storeScript)
        {
            
            totalCoins = StoreScriptHandler.storeScript.getTotalEarnedCoins();
            //coinText.text = totalCoins.ToString();
            if (StoreScriptHandler.storeScript.getTotalEarnedCoins() < 300)
            {
                skipButton.interactable = false;
            }
        }
        ObjectiveDialoug();
       
    }
   
    void OnStart()
    {
        //Active Fashion Meshes based on Selection
        if (PlayerPrefs.GetInt("HairBand") == 1)
        {
            hairBand.SetActive(true); g02HairBand.SetActive(true); g03HairBand.SetActive(true);
            Ribbon.SetActive(false); g02Ribbon.SetActive(false); g03Ribbon.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Ribbon") == 1)
        {
            hairBand.SetActive(false); g02HairBand.SetActive(false); g03HairBand.SetActive(false);
            Ribbon.SetActive(true); g02Ribbon.SetActive(true); g03Ribbon.SetActive(true);
        }

        //Active Hair Meshes based on Selection
        if (PlayerPrefs.GetInt("Hair01") == 1)
        {
            hair01.SetActive(true); g02Hair01.SetActive(true) ;g03Hair01.SetActive(true);
            hair02.SetActive(false); g02Hair02.SetActive(false); g03Hair02.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Hair02") == 1)
        {
            hair01.SetActive(false); g02Hair01.SetActive(false); g02Hair01.SetActive(false);
            hair02.SetActive(true); g02Hair02.SetActive(true); g02Hair02.SetActive(true);
        }
    }
    public void AddDiamonds(int diamond)
    {
        if (StoreScriptHandler.storeScript)
        {
            StoreScriptHandler.storeScript.setTotalEarnedCoins(StoreScriptHandler.storeScript.getTotalEarnedCoins() + diamond);
        }
    }
    public void ChangePlayer()
    {
        StartCoroutine(FadeImageDelay());
    }

    IEnumerator FadeImageDelay()
    {
        motherCamera.gameObject.SetActive(false);
        childCamera.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeImage.SetActive(false);
    }
    public void SetScaleZero()
    {
        vUI.transform.localScale = new Vector3(0, 0, 0);
    }
    public void SetScaleOne()
    {
        vUI.transform.localScale = new Vector3(1, 1, 1);
    }
    void spawnLevel()
    {
        Levels = new GameObject[levelsParent.transform.childCount];
        for (int i = 0; i < Levels.Length; i++)
        {
            Levels[i] = levelsParent.transform.GetChild(i).gameObject;
        }
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
        {
            selectedLevel = PlayerPrefs.GetInt("SelectedLevel");
            unlockedLevel = PlayerPrefs.GetInt("TotalLevelsUnlocked");
            Levels[selectedLevel].SetActive(true);
            currentLevelModel = Levels[selectedLevel].GetComponent<LevelModel_Handler>();

        }
        else if (PlayerPrefs.GetInt("SelectedMode") == 1)
        {
            selectedLevel = PlayerPrefs.GetInt("SelectedHouseLevel");
            unlockedLevel = PlayerPrefs.GetInt("TotalHouseLevelsUnlocked");
            Levels[selectedLevel].SetActive(true);
            currentLevelModel = Levels[selectedLevel].GetComponent<LevelModel_Handler>();
        }
       
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            pauseGameDialoug();
        }

#if UNITY_EDITOR
        if (levelCompleteTest) {
            levelCompleteTest = false;
            levelCompleteDialoug();
        }
        if (levelFailedTest) {
            levelFailedTest = false;
            levelFailedDialoug();
        }
#endif
    }

    public LevelModel_Handler returnLevelModelHandler()
    {
        return currentLevelModel;
    }

    IEnumerator loadingScreenHandler()
    {
        int temp = Random.Range(0, loadingBackgrounds.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingBackgrounds[temp];
        yield return new WaitForSeconds(loadingScreenDisplayTime);
        //if (AdsManager.adsManager)
        //{
        //    AdsManager.adsManager.DestroyNative();
        //}
        loadingScreen.SetActive(false);
        
        //if (AdsManager.adsManager)
        //{
        //    AdsManager.adsManager.ShowAdMob();
        //}
    }
    private void FixedUpdate()
    {
        if (Isloading)
        {
            LoadingImage.fillAmount += 0.003f;
            if (LoadingImage.fillAmount==1)
            {
                Isloading = false;
            }
        }
    }
    public void ObjectiveDialoug()
    {
        if (!primaryObjectives) {
            primaryObjectives = true;
            objDialoug.SetActive(true);
            objText.text = currentLevelModel.primaryObjective;
        }
    }

    public void playButtonSound()
    {
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }

    public void skipButtonPressed()
    {
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayButtonClickSound();
        }
        //currentLevelModel.skipCutScene = true;
    }

    public void hideButtons()
    {
        for (int i = 0; i < gpButtons.Length; i++) {
            gpButtons[i].SetActive(false);
        }
    }

    public void showButtons()
    {
        for (int i = 0; i < gpButtons.Length; i++) {
            gpButtons[i].SetActive(true);
        }
    }

    public void displayTimerScript()
    {
        //returnLevelModelHandler().timerScript.gameObject.SetActive(true);
    }

    //public void ShowRewardedVideo()
    //{
    //    if (AdsManager.adsManager)
    //    {
    //        AdsManager.adsManager.Admob_Reward_Video_Show();
    //    }
    //}
    //void OnEnable()
    //{
    //    if (AdsManager.adsManager)
    //        AdsManager.onRewardedAdShown += HandleonRewardedVideoViewedSuccessfull;
    //        AdsManager.rewarded_Admobe_Shown += HandleRewardBasedVideoRewarded;
    //}

    //void OnDisable()
    //{
    //    if (AdsManager.adsManager)
    //        AdsManager.onRewardedAdShown -= HandleonRewardedVideoViewedSuccessfull;
    //        AdsManager.rewarded_Admobe_Shown -= HandleRewardBasedVideoRewarded;
    //}

    //void HandleonRewardedVideoViewedSuccessfull()
    //{
    //    currentLevelModel.timerScript.time += timeToBeAlotedAfterRewardedVideo;
    //    currentLevelModel.timerScript.stopTimer = false;
    //    currentLevelModel.timerScript.stopSpinner = false;
    //    StartCoroutine(currentLevelModel.timerScript.StartCoundownTimer());
    //    rewardedAd.SetActive(false);
    //    spinnerImage.fillAmount = 0;
    //}
    //void HandleRewardBasedVideoRewarded()
    //{
    //    currentLevelModel.timerScript.time += timeToBeAlotedAfterRewardedVideo;
    //    currentLevelModel.timerScript.stopTimer = false;
    //    currentLevelModel.timerScript.stopSpinner = false;
    //    StartCoroutine(currentLevelModel.timerScript.StartCoundownTimer());
    //    rewardedAd.SetActive(false);
    //    spinnerImage.fillAmount = 0;
    //}

    public void pauseGameDialoug()
    {
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayButtonClickSound();
        }
        pauseScreen.SetActive(true);
        Time.timeScale = 0.01f;
       // AdsManager_ZL.instance.CallInterstitialAd(Adspref.GamePause);
    }

    public void resumeGameplay()
    {
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayButtonClickSound();
        }
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void restartGame()
    {
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayButtonClickSound();
        }
        int temp = Random.Range(0, loadingBackgrounds.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingBackgrounds[temp];
        Time.timeScale = 1f;
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StartCoroutine(LoadScene());
    }

    public void home()
    {
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayButtonClickSound();
        }
        int temp = Random.Range(0, loadingBackgrounds.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingBackgrounds[temp];
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void levelFail_CompleteStatusEvent(bool temp)
    {
        StartCoroutine(levelFail_CompleteStatus(temp));
    }

    IEnumerator levelFail_CompleteStatus(bool temp)
    {
        hideButtons();
        if (PlayerPrefs.GetInt("SelectedLevel") == 0 || PlayerPrefs.GetInt("SelectedLevel") == 11 || PlayerPrefs.GetInt("SelectedLevel") == 12 || PlayerPrefs.GetInt("SelectedLevel") == 14 || PlayerPrefs.GetInt("SelectedLevel") == 18)
        {
            yield return new WaitForSeconds(3f);
            if (temp)
            {
                levelCompleteDialoug();
            }
        }
        else if (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (temp)
            {
                levelCompleteDialoug();
            }
            else if (!temp)
            {
                levelFailedDialoug();
            }
        }
        
    }

    void levelCompleteDialoug()
    {
        completeScreen.SetActive(true);
        AddDiamonds(100);

        if (PlayerPrefs.GetInt("SelectedMode") == 0)
        {
            if (PlayerPrefs.GetInt("SelectedLevel") == PlayerPrefs.GetInt("TotalLevelsUnlocked") && PlayerPrefs.GetInt("TotalLevelsUnlocked") < Levels.Length - 1)
            {
                PlayerPrefs.SetInt("TotalLevelsUnlocked", PlayerPrefs.GetInt("TotalLevelsUnlocked") + 1);
                unlockedLevel = PlayerPrefs.GetInt("TotalLevelsUnlocked");
            }
        }
        if (PlayerPrefs.GetInt("SelectedMode") == 1)
        {
            if (PlayerPrefs.GetInt("SelectedHouseLevel") == PlayerPrefs.GetInt("TotalHouseLevelsUnlocked") && PlayerPrefs.GetInt("TotalHouseLevelsUnlocked") < Levels.Length - 1)
            {
                PlayerPrefs.SetInt("TotalHouseLevelsUnlocked", PlayerPrefs.GetInt("TotalHouseLevelsUnlocked") + 1);
                unlockedLevel = PlayerPrefs.GetInt("TotalHouseLevelsUnlocked");
            }
        }

        if (StoreScriptHandler.storeScript) {
            int temp = StoreScriptHandler.storeScript.getRewardOfLevel(selectedLevel);
            StoreScriptHandler.storeScript.setTotalEarnedCoins(temp + StoreScriptHandler.storeScript.getTotalEarnedCoins());
            totalCoins = StoreScriptHandler.storeScript.getTotalEarnedCoins();
            //StartCoroutine(delayedTotalCoinAdder());
        }
        //AdsManager_ZL.instance.CallInterstitialAd(Adspref.GamePause);
    }

    IEnumerator delayedTotalCoinAdder()
    {
        for (int i = 0; i < totalCoins;) {
            yield return new WaitForSeconds(0.01f);
            totalCoinsEarned.text = i.ToString();
            i += 25;
        }
        //Time.timeScale = 0.01f;
    }
    public void coinCollect(int coin)
    {
        if (StoreScriptHandler.storeScript)
        {
            StoreScriptHandler.storeScript.setTotalEarnedCoins(coin + StoreScriptHandler.storeScript.getTotalEarnedCoins());
            coinText.text = StoreScriptHandler.storeScript.getTotalEarnedCoins().ToString();
            //Debug.Log("Coin Collected "+ coin);
        }
    }
    void levelFailedDialoug()
    {
        failedScreen.SetActive(true);
        Time.timeScale = 0.01f;
        //AdsManager_ZL.instance.CallInterstitialAd(Adspref.GamePause);
    }

    public void next()
    {
        if (SoundManager.Instance) {
            SoundManager.Instance.PlayButtonClickSound();
        }
        Time.timeScale = 1f;
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
        {
            if (PlayerPrefs.GetInt("SelectedLevel") < Levels.Length - 1)
            {
                PlayerPrefs.SetInt("SelectedLevel", PlayerPrefs.GetInt("SelectedLevel") + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                home();
            }
        }
       if (PlayerPrefs.GetInt("SelectedMode") == 1)
        {
            if (PlayerPrefs.GetInt("SelectedHouseLevel") < Levels.Length - 1)
            {
                PlayerPrefs.SetInt("SelectedHouseLevel", PlayerPrefs.GetInt("SelectedHouseLevel") + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                home();
            }
        }
       
       // AdsManager.adsManager.ShowAdMob();
    }

    public void SkipLevel()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
        Time.timeScale = 1f;
        
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
        {
            if (StoreScriptHandler.storeScript.getTotalEarnedCoins() >= 300)
            {
                StoreScriptHandler.storeScript.setTotalEarnedCoins(StoreScriptHandler.storeScript.getTotalEarnedCoins() - 300);
                if (PlayerPrefs.GetInt("SelectedLevel") == PlayerPrefs.GetInt("TotalLevelsUnlocked") && PlayerPrefs.GetInt("TotalLevelsUnlocked") < Levels.Length - 1)
                {
                    PlayerPrefs.SetInt("TotalLevelsUnlocked", PlayerPrefs.GetInt("TotalLevelsUnlocked") + 1);
                    unlockedLevel = PlayerPrefs.GetInt("TotalLevelsUnlocked");
                }
                if (PlayerPrefs.GetInt("SelectedLevel") < Levels.Length - 1)
                {
                    PlayerPrefs.SetInt("SelectedLevel", PlayerPrefs.GetInt("SelectedLevel") + 1);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
            else
            {
                //Debug.Log("Insufficient Diamonds...");
            }
        }
    }

    IEnumerator LoadScene()
    {
        int temp = Random.Range(0, loadingBackgrounds.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingBackgrounds[temp];
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        while (!asyncOperation.isDone)
        {
            loading_Text.text = (int)(asyncOperation.progress * 100) + "%";
            if (LoadingImage) LoadingImage.fillAmount = asyncOperation.progress;

            yield return null;
        }
        
    }

    


}
