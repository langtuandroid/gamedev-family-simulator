using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TriggerEvents : MonoBehaviour
{
   
    public string tagName;
    public bool isCollision;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    //public UnityEngine.Events.UnityEvent onTriggerStay;
    
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
            if (collision.gameObject.tag == tagName)
            {
                onTriggerEnter.Invoke();
            }
        }
        
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (isCollision)
        {
            if (collision.gameObject.tag == tagName)
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
