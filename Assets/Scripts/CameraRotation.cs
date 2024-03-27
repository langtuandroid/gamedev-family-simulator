using System.Collections;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    
    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = mainCamera.transform.position;

        while (time < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.position = targetPosition;
    }
    private IEnumerator LerpRotation(Quaternion endValue, float duration)
    {
        float time = 0;
        Quaternion startValue = mainCamera.transform.rotation;

        while (time < duration)
        {
            mainCamera.transform.rotation = Quaternion.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.rotation = endValue;
    }
}
