using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOEvent : ScriptableObject
{
    List<SOEventListener> listeners = new List<SOEventListener>();

    public void RegisterListener(SOEventListener listener)
    {
        listeners.Add(listener);
    }

    public void DeregisterListener(SOEventListener listener)
    {
        listeners.Remove(listener);
    }

    public void TriggerEvent()
    {
        foreach(SOEventListener listener in listeners)
        {
            listener.onEventTrigger.Invoke();
        }
    }
}
