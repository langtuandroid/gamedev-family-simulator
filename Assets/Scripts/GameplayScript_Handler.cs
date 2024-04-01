using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Invector.vCamera;
using Invector;
using TMPro;
using UnityEngine.Serialization;
using Zenject;

public class GameplayHandler : MonoBehaviour {

    [SerializeField] private bool isTesting;
    [SerializeField] private int testingLevel;
    
    [Header("On Scene Load Setting")]
    [SerializeField] private GameObject env;
    [Space(5)]
    [SerializeField] private GameObject hair01, g02Hair01, g03Hair01;
    [Space(5)]
    [SerializeField] private GameObject hair02, g02Hair02, g03Hair02;
    [Space(5)]
    [SerializeField] private GameObject hairBand, g02HairBand, g03HairBand;
    
    [FormerlySerializedAs("Ribbon")]
    [Space(5)]
    [SerializeField] private GameObject ribbon;

    [Space(5)]
    [SerializeField] private GameObject g02Ribbon, g03Ribbon;

    [Space(10)]
    [SerializeField] private vThirdPersonCamera motherCamera;
    [SerializeField] private vThirdPersonCamera childCamera;
    [SerializeField] private GameObject fadeImage;
    [SerializeField] private vThirdPersonCameraListData childList;
    [SerializeField] private GameObject vUI;
    [SerializeField] private Button skipButton;
    
    [Header("Objective Dialog Screen")]
    [SerializeField] private GameObject objDialog;
    [SerializeField] private TextMeshProUGUI objText;
    [SerializeField] private TextMeshProUGUI secondaryText;
    
    [Header("Gameplay Buttons")]
    public GameObject[] gpButtons;
    
    [Header("Loading Screen")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Sprite[] loadingBackgrounds;
    [SerializeField] private float loadingScreenDisplayTime;
    [SerializeField] private Image loadingImage;
    [SerializeField] private bool isLoading;
    [SerializeField] private Text loadingText;
    
    [Header("Level Pause Screen")]
    [SerializeField] private GameObject pauseScreen;
    
    [Header("Level Failed Screen")]
    [SerializeField] private GameObject failedScreen;
    
    [Header("Level Complete Screen")]
    [SerializeField] private GameObject completeScreen;

    [Header("Assign ind. level prefab.")]
    [SerializeField] private GameObject levelsParent;
    [SerializeField] private GameObject[] Levels;

    [Header("Other Variables")]
    private bool _displayAdOnce, _primaryObjectives;
    
    [SerializeField] bool levelCompleteTest, levelFailedTest;
    [SerializeField] private int selectedLevel, unlockedLevel;
    [SerializeField] private GameObject skipCutScene;
    
    [SerializeField] private TextMeshProUGUI totalCoinsEarned, timerText, coinText;
    [SerializeField] private GameObject spawnedLevel;
    
    private LevelModelHandler _currentLevelModel;
    
    private int _totalCoins;
    
    [Header("Rewarded Ad - Timer")]
    [SerializeField] private GameObject rewardedAd;
    [SerializeField] private Image spinnerImage;
    [SerializeField] private float timeToBeLootedAfterRewardedVideo;

    [Inject] private SoundManager _soundManager;
    [Inject] private StoreHandler _storeHandler;

    public GameObject RewardedAd => rewardedAd;
    public Image SpinnerImage => spinnerImage;

    public TextMeshProUGUI SecondaryText => secondaryText;

    void Awake()
    {
        Time.timeScale = 1f;
        HideButtons();
    }

    private void Start()
    {
        if (isTesting)
        {
            PlayerPrefs.SetInt("SelectedLevel", testingLevel);
        }

        env.SetActive(true);
        if (_soundManager) {
            _soundManager.PlayGameplaySounds(0.65f);
        }
        if (PlayerPrefs.GetInt("ComingFromGP", 0) == 0) {
            PlayerPrefs.SetInt("ComingFromGP", 1);
        }
        SpawnLevel();
        //StartCoroutine(loadingScreenHandler());

        if (_storeHandler)
        {
            _totalCoins = _storeHandler.GetTotalEarnedCoins();
            if (_storeHandler.GetTotalEarnedCoins() < 300)
            {
                skipButton.interactable = false;
            }
        }
        ObjectiveDialog();
       
    }
   
    private void OnStart()
    {
        //Active Fashion Meshes based on Selection
        if (PlayerPrefs.GetInt("HairBand") == 1)
        {
            hairBand.SetActive(true); g02HairBand.SetActive(true); g03HairBand.SetActive(true);
            ribbon.SetActive(false); g02Ribbon.SetActive(false); g03Ribbon.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Ribbon") == 1)
        {
            hairBand.SetActive(false); g02HairBand.SetActive(false); g03HairBand.SetActive(false);
            ribbon.SetActive(true); g02Ribbon.SetActive(true); g03Ribbon.SetActive(true);
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

    private void AddDiamonds(int diamond)
    {
        if (_storeHandler)
        {
            _storeHandler.SetTotalEarnedCoins(_storeHandler.GetTotalEarnedCoins() + diamond);
        }
    }
    public void ChangePlayer()
    {
        StartCoroutine(FadeImageDelay());
    }

    private IEnumerator FadeImageDelay()
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
    private void SpawnLevel()
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
            _currentLevelModel = Levels[selectedLevel].GetComponent<LevelModelHandler>();

        }
        else if (PlayerPrefs.GetInt("SelectedMode") == 1)
        {
            selectedLevel = PlayerPrefs.GetInt("SelectedHouseLevel");
            unlockedLevel = PlayerPrefs.GetInt("TotalHouseLevelsUnlocked");
            Levels[selectedLevel].SetActive(true);
            _currentLevelModel = Levels[selectedLevel].GetComponent<LevelModelHandler>();
        }
       
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PauseGameDialog();
        }

#if UNITY_EDITOR
        if (levelCompleteTest) {
            levelCompleteTest = false;
            LevelCompleteDialog();
        }
        if (levelFailedTest) {
            levelFailedTest = false;
            LevelFailedDialog();
        }
#endif
    }

    public LevelModelHandler ReturnLevelModelHandler()
    {
        return _currentLevelModel;
    }

    private IEnumerator LoadingScreenHandler()
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
        if (isLoading)
        {
            loadingImage.fillAmount += 0.003f;
            if (loadingImage.fillAmount == 1f)
            {
                isLoading = false;
            }
        }
    }

    private void ObjectiveDialog()
    {
        if (!_primaryObjectives) {
            _primaryObjectives = true;
            objDialog.SetActive(true);
            objText.text = _currentLevelModel.PrimaryObjective;
        }
    }

    public void PlayButtonSound()
    {
        if (_soundManager) {
            _soundManager.PlayButtonClickSound();
        }
    }

    public void SkipButtonPressed()
    {
        if (_soundManager) {
            _soundManager.PlayButtonClickSound();
        }
        //currentLevelModel.skipCutScene = true;
    }

    public void HideButtons()
    {
        foreach (var t in gpButtons)
        {
            t.SetActive(false);
        }
    }

    public void ShowButtons()
    {
        foreach (var t in gpButtons)
        {
            t.SetActive(true);
        }
    }

    public void DisplayTimerScript()
    {
        //returnLevelModelHandler().timerScript.gameObject.SetActive(true);
    }
    

    public void PauseGameDialog()
    {
        if (_soundManager) {
            _soundManager.PlayButtonClickSound();
        }
        pauseScreen.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void ResumeGameplay()
    {
        if (_soundManager) {
            _soundManager.PlayButtonClickSound();
        }
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        if (_soundManager) {
            _soundManager.PlayButtonClickSound();
        }
        int temp = Random.Range(0, loadingBackgrounds.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingBackgrounds[temp];
        Time.timeScale = 1f;
        StartCoroutine(LoadScene());
    }

    public void Home()
    {
        if (_soundManager) {
            _soundManager.PlayButtonClickSound();
        }
        int temp = Random.Range(0, loadingBackgrounds.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingBackgrounds[temp];
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void LevelFail_CompleteStatusEvent(bool temp)
    {
        StartCoroutine(LevelFail_CompleteStatus(temp));
    }

    private IEnumerator LevelFail_CompleteStatus(bool temp)
    {
        Debug.Log("SelectedLevel= " + PlayerPrefs.GetInt("SelectedLevel"));
        HideButtons();
        if (PlayerPrefs.GetInt("SelectedLevel") == 0 || PlayerPrefs.GetInt("SelectedLevel") == 11 || PlayerPrefs.GetInt("SelectedLevel") == 12 || PlayerPrefs.GetInt("SelectedLevel") == 14 || PlayerPrefs.GetInt("SelectedLevel") == 18)
        {
            yield return new WaitForSeconds(3f);
            if (temp)
            {
                LevelCompleteDialog();
            }
        }
        else if (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (temp)
            {
                LevelCompleteDialog();
            }
            else
            {
                LevelFailedDialog();
            }
        }
        
    }

    private void LevelCompleteDialog()
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

        if (_storeHandler) {
            int temp = _storeHandler.GetRewardOfLevel(selectedLevel);
            _storeHandler.SetTotalEarnedCoins(temp + _storeHandler.GetTotalEarnedCoins());
            _totalCoins = _storeHandler.GetTotalEarnedCoins();
            //StartCoroutine(DelayedTotalCoinAdder());
        }
        //AdsManager_ZL.instance.CallInterstitialAd(Adspref.GamePause);
    }

    private IEnumerator DelayedTotalCoinAdder()
    {
        for (int i = 0; i < _totalCoins;) {
            yield return new WaitForSeconds(0.01f);
            totalCoinsEarned.text = i.ToString();
            i += 25;
        }
        //Time.timeScale = 0.01f;
    }
    public void CoinCollect(int coin)
    {
        if (_storeHandler)
        {
            _storeHandler.SetTotalEarnedCoins(coin + _storeHandler.GetTotalEarnedCoins());
            coinText.text = _storeHandler.GetTotalEarnedCoins().ToString();
        }
    }
    private void LevelFailedDialog()
    {
        failedScreen.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void Next()
    {
        if (_soundManager) {
            _soundManager.PlayButtonClickSound();
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
                Home();
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
                Home();
            }
        }
    }

    public void SkipLevel()
    {
        if (_soundManager)
        {
            _soundManager.PlayButtonClickSound();
        }
        Time.timeScale = 1f;
        
        if (PlayerPrefs.GetInt("SelectedMode") == 0)
        {
            if (_storeHandler.GetTotalEarnedCoins() >= 300)
            {
                _storeHandler.SetTotalEarnedCoins(_storeHandler.GetTotalEarnedCoins() - 300);
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
        }
    }

    private IEnumerator LoadScene()
    {
        int temp = Random.Range(0, loadingBackgrounds.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingBackgrounds[temp];
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);

        while (!asyncOperation.isDone)
        {
            loadingText.text = (int)(asyncOperation.progress * 100) + "%";
            if (loadingImage) loadingImage.fillAmount = asyncOperation.progress;

            yield return null;
        }
        
    }

    


}
