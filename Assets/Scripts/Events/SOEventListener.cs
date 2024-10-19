using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Events;
using UnityEngine.Events;

public class SOEventListener : MonoBehaviour
{
    [SerializeField] SOEvent SOEventToRegister;

    public UnityEvent onEventTrigger = new();

    private void OnEnable()
    {
        SOEventToRegister.RegisterListener(this);
    }

    private void OnDisable()
    {
        SOEventToRegister.DeregisterListener(this);
    }
}
