using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnableDisable : MonoBehaviour
{
    public GameObject Guns;
    public Canvas canvas;
    public Camera SelectionCamera;
    private void OnEnable()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = SelectionCamera;
        Guns.SetActive(true);
    }
    private void OnDisable()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Guns.SetActive(false);
    }
}
