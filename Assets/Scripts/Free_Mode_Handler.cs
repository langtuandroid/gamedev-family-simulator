using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Free_Mode_Handler : MonoBehaviour
{
    [Header("Math Questions...")]
    [SerializeField] GameObject mathPanel;
    [SerializeField] GameObject[] mathQuestions;
    
    [Header("Color Match Questions...")]
    [SerializeField] GameObject colorPanel;
    [SerializeField] GameObject[] colorQuestions;
    
    [Header("Match Shape Questions...")]
    [SerializeField] GameObject shapePanel;
    [SerializeField] GameObject[] shapeQuestions;
    
    [Header("Identify Animal Questions...")]
    [SerializeField] GameObject animalPanel;
    [SerializeField] GameObject[] animalQuestions;
    
    [Header("Music Setting...")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip Rightclip,Wrongclip;
    
    [Header("On Scene Load Setting...")]
    [SerializeField] GameObject[] Players;
    [SerializeField] GameObject[] activeModes;
    [SerializeField] GameObject girlHairBand;
    [SerializeField] GameObject girlRibbon;
    [SerializeField] GameObject girlHair01;
    [SerializeField] GameObject girlHair02;

    [Header("Game Setting...")]
    [SerializeField] GameObject pausePanel;
    [SerializeField] Sprite[] loadingScreenImages;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Text loadingText;
    [SerializeField] Image loadingImage;
    [SerializeField] GameObject env;
    [SerializeField] GameObject resultPanel;
    [SerializeField] Text correct_Text;
    [SerializeField] Text wrong_Text;
    [SerializeField] Text total_Text;
    [SerializeField] Text total_M_Que;
    [SerializeField] Text total_S_Que;
    [SerializeField] Text total_C_Que;
    [SerializeField] Text total_A_Que;
    [SerializeField] GameObject[] remarks;


    int tempMath = 0, tempShape = 0, tempColor = 0, tempAnimal = 0,
               countMath, countShape, countColor, countAnimal;
    // Start is called before the first frame update
    void Start()
    {
        countMath = 1;
        countShape = 1;
        countColor = 1;
        countAnimal = 1;
        env.SetActive(true);
        OnStart();
        //AdsManager_ZL.instance.CallInterstitialAd(Adspref.GameEnd);
    }

    public void NextMathQuestion()
    {
        StartCoroutine("WaitMath");
    }
    public void NextShapeQuestion()
    {
        StartCoroutine("WaitShape");
    }
    public void NextColorQuestion()
    {
        StartCoroutine("WaitColor");
    }
    public void NextAnimalQuestion()
    {
        StartCoroutine("WaitAnimal");
    }
    
    public void SetPref()
    {
        PlayerPrefs.SetInt("ShowLearningMode", 1);
    }
    
    public void SetCounter()
    {
        if (mathPanel.activeSelf)
        {
            PlayerPrefs.SetInt("MathRightAnswers", countMath);
            //Debug.Log("Value of Math Counter: " + countMath);
            countMath++;
        }
        else if (shapePanel.activeSelf)
        {
            PlayerPrefs.SetInt("ShapeRightAnswers", countShape);
            //Debug.Log("Value of Shape Counter: " + countShape);
            countShape++;
        }
        else if (colorPanel.activeSelf)
        {
            PlayerPrefs.SetInt("ColorRightAnswers", countColor);
            //Debug.Log("Value of Color Counter: " + countColor);
            countColor++;
        }
        else if (animalPanel.activeSelf)
        {
            PlayerPrefs.SetInt("AnimalRightAnswers", countAnimal);
            //Debug.Log("Value of Animal Counter: " + countAnimal);
            countAnimal++;
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
            correct_Text.text = result.ToString();
            wrong_Text.text = (10 - result).ToString();
            total_Text.text = result.ToString() + "/10";
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
            correct_Text.text = result.ToString();
            wrong_Text.text = (10 - result).ToString();
            total_Text.text = result.ToString() + "/10";
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
            correct_Text.text = result.ToString();
            wrong_Text.text = (10 - result).ToString();
            total_Text.text = result.ToString() + "/10";
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
            correct_Text.text = result.ToString();
            wrong_Text.text = (11 - result).ToString();
            total_Text.text = result.ToString() + "/11";
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
    IEnumerator WaitMath()
    {
        //Debug.Log("Value of Math Temper: " + tempMath);
        if (tempMath >= 0 && tempMath <= 9)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < mathQuestions.Length; i++)
            {
                int index = tempMath;
                if (i == index)
                {
                    mathQuestions[index].SetActive(true);
                    total_M_Que.text = (i + 1) + "/10";
                }
                else
                {
                    mathQuestions[i].SetActive(false);
                }
            }
            tempMath++;
        }
    }
    
    IEnumerator WaitShape()
    {
        //Debug.Log("Value of Shapes Temper: " + tempShape);
        if (tempShape >= 0 && tempShape <= 9)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < shapeQuestions.Length; i++)
            {
                int index = tempShape;
                if (i == index)
                {
                    shapeQuestions[index].SetActive(true);
                    total_S_Que.text = (i + 1) + "/10";
                }
                else
                {
                    shapeQuestions[i].SetActive(false);
                }
            }
            tempShape++;
        }
    }
    
    IEnumerator WaitColor()
    {
        //Debug.Log("Value of Color Temper: " + tempColor);
        if (tempColor >= 0 && tempColor <= 9)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < colorQuestions.Length; i++)
            {
                int index = tempColor;
                if (i == index)
                {
                    colorQuestions[index].SetActive(true);
                    total_C_Que.text = (i + 1) + "/10";
                }
                else
                {
                    colorQuestions[i].SetActive(false);
                }
            }
            tempColor++;
        }
    }
    
    IEnumerator WaitAnimal()
    {
        //Debug.Log("Value of Animal Temper: " + tempAnimal);
        if (tempAnimal >= 0 && tempAnimal <= 10)
        {
            yield return new WaitForSeconds(2f);
            //Debug.Log("Count is Greater than 0 & Less than or Equel to 9");
            for (int i = 0; i < animalQuestions.Length; i++)
            {
                int index = tempAnimal;
                if (i == index)
                {
                    animalQuestions[index].SetActive(true);
                    total_A_Que.text = (i + 1) + "/11";
                }
                else
                {
                    animalQuestions[i].SetActive(false);
                }
            }
            tempAnimal++;
        }
    }

    public void RightAnsSound()
    {
        audioSource.clip = Rightclip;
        audioSource.Play();
    }
    public void WrongAnsSound()
    {
        audioSource.clip = Wrongclip;
        audioSource.Play();
    }
    public void PlayButtonSound()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
    }
    void OnStart()
    {
        int index = Random.Range(0, Players.Length);
        Players[index].SetActive(true);
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
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        //AdsManager_ZL.instance.CallInterstitialAd(Adspref.GameEnd);

    }
    public void GameResume()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

    }
    public void HomeLearningMode()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayButtonClickSound();
        }
        int temp = Random.Range(0, loadingScreenImages.Length);
        loadingScreen.SetActive(true);
        loadingScreen.GetComponent<Image>().sprite = loadingScreenImages[temp];
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void GameRestart()
    {
        if (SoundManager.Instance)
        {
            SoundManager.Instance.PlayButtonClickSound();
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
