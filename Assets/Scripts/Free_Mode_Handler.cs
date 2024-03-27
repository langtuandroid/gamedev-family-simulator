using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

public class FreeModeHandler : MonoBehaviour
{
    [Header("Math Questions...")] [SerializeField]
    GameObject mathPanel;

    [SerializeField] GameObject[] mathQuestions;

    [Header("Color Match Questions...")] [SerializeField]
    GameObject colorPanel;

    [SerializeField] GameObject[] colorQuestions;

    [Header("Match Shape Questions...")] [SerializeField]
    GameObject shapePanel;

    [SerializeField] GameObject[] shapeQuestions;

    [Header("Identify Animal Questions...")] [SerializeField]
    GameObject animalPanel;

    [SerializeField] GameObject[] animalQuestions;

    [Header("Music Setting...")] [SerializeField]
    private AudioSource audioSource;

    [SerializeField] private AudioClip rightClip, wrongClip;

    [Header("On Scene Load Setting...")] [SerializeField]
    private GameObject[] players;

    [SerializeField] private GameObject[] activeModes;
    [SerializeField] private GameObject girlHairBand;
    [SerializeField] private GameObject girlRibbon;
    [SerializeField] private GameObject girlHair01;
    [SerializeField] private GameObject girlHair02;

    [Header("Game Setting...")] 
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private Sprite[] loadingScreenImages;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Text loadingText;
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject env;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private Text correctText;
    [SerializeField] private Text wrongText;
    [SerializeField] private Text totalText;
    [SerializeField] private Text totalMQue;
    [SerializeField] private Text totalSQue;
    [SerializeField] private Text totalCQue;
    [SerializeField] private Text totalAQue;
    [SerializeField] private GameObject[] remarks;

    [Inject] private SoundManager _soundManager;

    private int _tempMath = 0,
        _tempShape = 0,
        _tempColor = 0,
        _tempAnimal = 0,
        _countMath,
        _countShape,
        _countColor,
        _countAnimal;
    
    private void Start()
    {
        _countMath = 1;
        _countShape = 1;
        _countColor = 1;
        _countAnimal = 1;
        env.SetActive(true);
        OnStart();
        //AdsManager_ZL.instance.CallInterstitialAd(Adspref.GameEnd);
    }

    public void NextMathQuestion()
    {
        StartCoroutine(nameof(WaitMath));
    }

    public void NextShapeQuestion()
    {
        StartCoroutine(nameof(WaitShape));
    }

    public void NextColorQuestion()
    {
        StartCoroutine(nameof(WaitColor));
    }

    public void NextAnimalQuestion()
    {
        StartCoroutine(nameof(WaitAnimal));
    }

    public void SetPref()
    {
        PlayerPrefs.SetInt("ShowLearningMode", 1);
    }

    public void SetCounter()
    {
        if (mathPanel.activeSelf)
        {
            PlayerPrefs.SetInt("MathRightAnswers", _countMath);
            //Debug.Log("Value of Math Counter: " + countMath);
            _countMath++;
        }
        else if (shapePanel.activeSelf)
        {
            PlayerPrefs.SetInt("ShapeRightAnswers", _countShape);
            //Debug.Log("Value of Shape Counter: " + countShape);
            _countShape++;
        }
        else if (colorPanel.activeSelf)
        {
            PlayerPrefs.SetInt("ColorRightAnswers", _countColor);
            //Debug.Log("Value of Color Counter: " + countColor);
            _countColor++;
        }
        else if (animalPanel.activeSelf)
        {
            PlayerPrefs.SetInt("AnimalRightAnswers", _countAnimal);
            //Debug.Log("Value of Animal Counter: " + countAnimal);
            _countAnimal++;
        }
    }

    public void ShowResult()
    {
        StartCoroutine("WaitInResult");
    }

    IEnumerator WaitInResult()
    {
        yield return new WaitForSeconds(2f);
        if (mathPanel.activeSelf)
        {
            resultPanel.SetActive(true);
            int result = PlayerPrefs.GetInt("MathRightAnswers");
            correctText.text = result.ToString();
            wrongText.text = (10 - result).ToString();
            totalText.text = result.ToString() + "/10";
            if (result > 7)
            {
                remarks[0].SetActive(true);
            }
            else if (result > 5 && result <= 7)
            {
                remarks[1].SetActive(true);
            }
            else
            {
                remarks[2].SetActive(true);
            }
        }
        else if (shapePanel.activeSelf)
        {
            resultPanel.SetActive(true);
            int result = PlayerPrefs.GetInt("ShapeRightAnswers");
            correctText.text = result.ToString();
            wrongText.text = (10 - result).ToString();
            totalText.text = result.ToString() + "/10";
            if (result > 7)
            {
                remarks[0].SetActive(true);
            }
            else if (result > 5 && result <= 7)
            {
                remarks[1].SetActive(true);
            }
            else
            {
                remarks[2].SetActive(true);
            }
        }
        else if (colorPanel.activeSelf)
        {
            resultPanel.SetActive(true);
            int result = PlayerPrefs.GetInt("ColorRightAnswers");
            correctText.text = result.ToString();
            wrongText.text = (10 - result).ToString();
            totalText.text = result.ToString() + "/10";
            if (result > 7)
            {
                remarks[0].SetActive(true);
            }
            else if (result > 5 && result <= 7)
            {
                remarks[1].SetActive(true);
            }
            else
            {
                remarks[2].SetActive(true);
            }
        }
        else if (animalPanel.activeSelf)
        {
            resultPanel.SetActive(true);
            int result = PlayerPrefs.GetInt("AnimalRightAnswers");
            correctText.text = result.ToString();
            wrongText.text = (11 - result).ToString();
            totalText.text = result.ToString() + "/11";
            if (result > 7)
            {
                remarks[0].SetActive(true);
            }
            else if (result > 5 && result <= 7)
            {
                remarks[1].SetActive(true);
            }
            else
            {
                remarks[2].SetActive(true);
            }
        }
    }

    private IEnumerator WaitMath()
    {
        //Debug.Log("Value of Math Temper: " + tempMath);
        if (_tempMath >= 0 && _tempMath <= 9)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < mathQuestions.Length; i++)
            {
                int index = _tempMath;
                if (i == index)
                {
                    mathQuestions[index].SetActive(true);
                    totalMQue.text = (i + 1) + "/10";
                }
                else
                {
                    mathQuestions[i].SetActive(false);
                }
            }

            _tempMath++;
        }
    }

    private IEnumerator WaitShape()
    {
        //Debug.Log("Value of Shapes Temper: " + tempShape);
        if (_tempShape >= 0 && _tempShape <= 9)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < shapeQuestions.Length; i++)
            {
                int index = _tempShape;
                if (i == index)
                {
                    shapeQuestions[index].SetActive(true);
                    totalSQue.text = (i + 1) + "/10";
                }
                else
                {
                    shapeQuestions[i].SetActive(false);
                }
            }

            _tempShape++;
        }
    }

    private IEnumerator WaitColor()
    {
        //Debug.Log("Value of Color Temper: " + tempColor);
        if (_tempColor >= 0 && _tempColor <= 9)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < colorQuestions.Length; i++)
            {
                int index = _tempColor;
                if (i == index)
                {
                    colorQuestions[index].SetActive(true);
                    totalCQue.text = (i + 1) + "/10";
                }
                else
                {
                    colorQuestions[i].SetActive(false);
                }
            }

            _tempColor++;
        }
    }

    private IEnumerator WaitAnimal()
    {
        //Debug.Log("Value of Animal Temper: " + tempAnimal);
        if (_tempAnimal >= 0 && _tempAnimal <= 10)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < animalQuestions.Length; i++)
            {
                int index = _tempAnimal;
                if (i == index)
                {
                    animalQuestions[index].SetActive(true);
                    totalAQue.text = (i + 1) + "/11";
                }
                else
                {
                    animalQuestions[i].SetActive(false);
                }
            }

            _tempAnimal++;
        }
    }

    public void RightAnsSound()
    {
        audioSource.clip = rightClip;
        audioSource.Play();
    }

    public void WrongAnsSound()
    {
        audioSource.clip = wrongClip;
        audioSource.Play();
    }

    public void PlayButtonSound()
    {
        if (_soundManager)
        {
            _soundManager.PlayButtonClickSound();
        }
    }

    void OnStart()
    {
        int index = Random.Range(0, players.Length);
        players[index].SetActive(true);
        for (int i = 0; i < activeModes.Length; i++)
        {
            if (PlayerPrefs.GetInt("SelectedLearningMode") == i)
            {
                activeModes[i].SetActive(true);
            }
        }

        //Active Fashion Meshes based on Selection
        if (PlayerPrefs.GetInt("HairBand") == 1)
        {
            girlHairBand.SetActive(true);
            girlRibbon.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Ribbon") == 1)
        {
            girlHairBand.SetActive(false);
            girlRibbon.SetActive(true);
        }

        //Active Hair Meshes based on Selection
        if (PlayerPrefs.GetInt("Hair01") == 1)
        {
            girlHair01.SetActive(true);
            girlHair02.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("Hair02") == 1)
        {
            girlHair01.SetActive(false);
            girlHair02.SetActive(true);
        }
    }

    public void GamePause()
    {
        if (_soundManager)
        {
            _soundManager.PlayButtonClickSound();
        }

        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        //AdsManager_ZL.instance.CallInterstitialAd(Adspref.GameEnd);
    }

    public void GameResume()
    {
        if (_soundManager)
        {
            _soundManager.PlayButtonClickSound();
        }

        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void HomeLearningMode()
    {
        if (_soundManager)
        {
            _soundManager.PlayButtonClickSound();
        }

        int temp = Random.Range(0, loadingScreenImages.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingScreenImages[temp];
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void GameRestart()
    {
        if (_soundManager)
        {
            _soundManager.PlayButtonClickSound();
        }

        loadingScreen.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(LoadLearningScene());
    }

    IEnumerator LoadLearningScene()
    {
        int temp = Random.Range(0, loadingScreenImages.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingScreenImages[temp];
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        while (!asyncOperation.isDone)
        {
            loadingText.text = (int)(asyncOperation.progress * 100) + "%";
            if (loadingImage) loadingImage.fillAmount = asyncOperation.progress;
            yield return null;
        }
    }
}