using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Splash : MonoBehaviour
{
    public Image SplashImage;
   
   

    // Start is called before the first frame update
    void Start()
    {
       
       
    }

    public void Loadscene()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
    }
}
