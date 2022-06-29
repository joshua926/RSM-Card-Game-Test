using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListenerMB : MonoBehaviour
{
    public EventSO evt;
    public UnityEvent response;

    void OnEnable()
    {
        evt.Register(this);
    }

    void OnDisable()
    {
        evt.Unregister(this);
    }

    public void OnRaiseEvent()
    {
        response.Invoke();
    }
}
