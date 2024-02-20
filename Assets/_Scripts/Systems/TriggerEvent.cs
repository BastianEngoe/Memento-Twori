using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public List<string> tags;
    public UnityEvent enterTrigger, exitTrigger;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < tags.Count; i++)
        {
            if (other.CompareTag(tags[i]))
            {
                enterTrigger.Invoke();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < tags.Count; i++)
        {
            if (other.CompareTag(tags[i]))
            {
                exitTrigger.Invoke();
            }
        }
    }
}
