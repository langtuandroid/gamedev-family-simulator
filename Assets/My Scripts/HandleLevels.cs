using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleLevels : MonoBehaviour
{
    int counter = 0;
    [Header("Level 01 Things")]
    [SerializeField] GameObject milkPack;

    [Header("Level 05 Things")]
    [SerializeField] GameObject[] bucketToys;
    [SerializeField] GameObject[] handToys;
    [SerializeField] GameObject pickButton;
    [SerializeField] GameObject path;
    [SerializeField] GameObject infoText;
    
    [Header("Level 06 Things")]
    [SerializeField] GameObject fadeImage;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject boyPlayer;
    [SerializeField] GameObject girlPlayer;
    [SerializeField] GameObject mousePlayer;
    [SerializeField] GameObject motherPlayer;

    [Header("Level 08 Things")]
    [SerializeField] GameObject playerPanel;
    [SerializeField] GameObject boyCutScene;
    [SerializeField] GameObject girlCutScene;

    [Header("Level 14 Things")]
    [SerializeField] GameObject level14Cutscene;

    void Start()
    {
        if (PlayerPrefs.GetInt("SelectedLevel") == 7)
        {
            playerPanel.SetActive(true);
        }
        else
        {
            playerPanel.SetActive(false);
        }
    }

    public void HideMilkPack()
    {
        milkPack.SetActive(false);
    }
    public void SetCounter()
    {
        counter++;
    }
    public void ResetCounter()
    {
        counter = 0;
    }
    public void ActionButtonSetScale(GameObject gameObject)
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }
    public void ActionButtonResetScale(GameObject gameObject)
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
    }
    
    public void CheckCounter()
    {
        if (counter > 0)
        {
            infoText.SetActive(true);
            ActionButtonResetScale(pickButton);
        }
        else
        {
            ActionButtonSetScale(pickButton);
        }
    }
    public void CheckAllToys()
    {
        if (bucketToys[0].activeSelf && bucketToys[1].activeSelf && bucketToys[2].activeSelf && bucketToys[3].activeSelf && bucketToys[4].activeSelf)
        {
            this.GetComponent<LevelModel_Handler>().ShowSecondaryObjective(3);
            path.SetActive(true);
        }
    }

    public void ThrowToys()
    {
        if (handToys[0].activeSelf)
        {
            bucketToys[0].SetActive(true);
            handToys[0].SetActive(false);
        }
        else if (handToys[1].activeSelf)
        {
            bucketToys[1].SetActive(true);
            handToys[1].SetActive(false);
        }
        else if (handToys[2].activeSelf)
        {
            bucketToys[2].SetActive(true);
            handToys[2].SetActive(false);
        }
        else if (handToys[3].activeSelf)
        {
            bucketToys[3].SetActive(true);
            handToys[3].SetActive(false);
        }
        else if (handToys[4].activeSelf)
        {
            bucketToys[4].SetActive(true);
            handToys[4].SetActive(false);
        }
    }

    public void ActivePlayerCar()
    {
        StartCoroutine(DelayInCar());
    }
    public void ActivePlayerBoy()
    {
        StartCoroutine(DelayInBoy());
    }
    public void ActivePlayerMouse()
    {
        StartCoroutine(DelayInMouse());
    }
    public void ActivePlayerMother()
    {
        StartCoroutine(DelayInMother());
    }

    public void ActiveLevel14Cutscene()
    {
        StartCoroutine(DelayInCutscene());
    }
    public void OnOffBGSound(int temp)
    {
        if (temp == 0)
        {
            if (SoundManager._SoundManager)
            {
                SoundManager._SoundManager.playGameplaySounds(0f);
            }
        }
        else if (temp == 1)
        {
            if (SoundManager._SoundManager)
            {
                SoundManager._SoundManager.playGameplaySounds(0.65f);
            }
        }
    }

    public void ChoosePlayer(int temp)
    {
        if (PlayerPrefs.GetInt("SelectedLevel") == 7)
        {
            if (temp == 0)
            {
                boyPlayer.SetActive(true);
            }
            else
            {
                girlPlayer.SetActive(true);
            }
        }
    }
    public void ActiveLevel08CutScene()
    {
        if (PlayerPrefs.GetInt("SelectedLevel") == 7 && girlPlayer.activeSelf)
        {
            boyCutScene.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("SelectedLevel") == 7 && boyPlayer.activeSelf)
        {
            girlCutScene.SetActive(true);
        }
    }

    IEnumerator DelayInCar()
    {
        fadeImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        arrow.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeImage.SetActive(false);
    }
    IEnumerator DelayInBoy()
    {
        fadeImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        boyPlayer.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeImage.SetActive(false);
    }
    IEnumerator DelayInMouse()
    {
        fadeImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        boyPlayer.SetActive(false);
        mousePlayer.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeImage.SetActive(false);
    }
    IEnumerator DelayInMother()
    {
        fadeImage.SetActive(true);
        yield return new WaitForSeconds(1f);
        motherPlayer.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        fadeImage.SetActive(false);
    }
    
    IEnumerator DelayInCutscene()
    {
        yield return new WaitForSeconds(1f);
        level14Cutscene.SetActive(true);
    }
    //IEnumerator DelayInPlayerSwitch()
    //{
    //    fadeImage.SetActive(true);
    //    yield return new WaitForSeconds(1f);
    //    boyPlayer.SetActive(true);
    //    girlPlayer.SetActive(false);
    //    this.gameObject.GetComponent<LevelModel_Handler>().ShowSecondaryObjective(2);
    //    yield return new WaitForSeconds(1.5f);
    //    fadeImage.SetActive(false);
    //}
}
