using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnableAndDisable : MonoBehaviour
{

    public UnityEvent OnEnableEvent;
    public UnityEvent OnDisableEvent;



    void OnEnable()
    {
        if (OnEnableEvent != null)
        {
            OnEnableEvent.Invoke();
            Debug.Log("GameObject Enabled");
        }
    }

    private void OnDisable()
    {
        if (OnDisableEvent != null)
        {
            OnDisableEvent.Invoke();
            Debug.Log("Game Object Disabled");
        }

    }
}
