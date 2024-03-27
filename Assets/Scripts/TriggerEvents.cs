using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    [SerializeField] private string tagName;
    [SerializeField] private bool isCollision;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerExit;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollision)
        {
            if (other.gameObject.CompareTag(tagName))
            {
                onTriggerEnter.Invoke();

            }
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isCollision)
        {
            if (other.gameObject.CompareTag(tagName))
            {
                onTriggerExit.Invoke();

            }
        }

    }
    //private void OnTriggerStay(Collider other)
    //{
    //    if (!isCollision)
    //    {
    //        if (other.gameObject.CompareTag(tagName))
    //        {
    //            onTriggerStay.Invoke();

    //        }
    //    }

    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (isCollision)
        {
            if (collision.gameObject.CompareTag(tagName))
            {
                onTriggerEnter.Invoke();
            }
        }
        
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (isCollision)
        {
            if (collision.gameObject.CompareTag(tagName))
            {
                onTriggerExit.Invoke();
            }
        }
        
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (isCollision)
    //    {
    //        if (collision.gameObject.tag == tagName)
    //        {
    //            onTriggerStay.Invoke();
    //        }
    //    }
        
    //}
}
