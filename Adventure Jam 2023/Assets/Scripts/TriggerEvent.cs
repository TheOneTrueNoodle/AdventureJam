using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public bool oneTimeTrigger;
    public UnityEvent triggerEnter;
    public UnityEvent triggerExit;
    public string collisionTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(collisionTag))
        {
            if(triggerEnter == null) { return; }

            triggerEnter.Invoke();
            if(oneTimeTrigger)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(collisionTag))
        {
            if (collision.CompareTag(collisionTag))
            {
                if (triggerExit == null) { return; }

                triggerExit.Invoke();
                if (oneTimeTrigger)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
