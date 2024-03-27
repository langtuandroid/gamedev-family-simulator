using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnableAndDisable : MonoBehaviour
{
    [FormerlySerializedAs("OnEnableEvent")] public UnityEvent onEnableEvent;
    [FormerlySerializedAs("OnDisableEvent")] public UnityEvent onDisableEvent;
    
    void OnEnable()
    {
        if (onEnableEvent != null)
        {
            onEnableEvent.Invoke();
            Debug.Log("GameObject Enabled");
        }
    }

    private void OnDisable()
    {
        if (onDisableEvent != null)
        {
            onDisableEvent.Invoke();
            Debug.Log("Game Object Disabled");
        }

    }
}
