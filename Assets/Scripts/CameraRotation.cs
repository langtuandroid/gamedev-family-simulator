using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotete : MonoBehaviour
{
    [SerializeField] GameObject MainCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = MainCamera.transform.position;

        while (time < duration)
        {
            MainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        MainCamera.transform.position = targetPosition;
    }
    IEnumerator LerpRotation(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = MainCamera.transform.rotation;

        while (time < duration)
        {
            MainCamera.transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        MainCamera.transform.rotation = endValue;
    }
}
