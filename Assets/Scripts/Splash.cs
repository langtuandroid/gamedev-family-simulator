using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour
{
    [SerializeField] private Image splashImage;

    void Start()
    {
        LoadScene();

    }

    private void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
