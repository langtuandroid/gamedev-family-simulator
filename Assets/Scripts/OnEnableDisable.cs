using UnityEngine;

public class OnEnableDisable : MonoBehaviour
{
    [SerializeField] private GameObject guns;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera selectionCamera;
    private void OnEnable()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = selectionCamera;
        guns.SetActive(true);
    }
    private void OnDisable()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        guns.SetActive(false);
    }
}
