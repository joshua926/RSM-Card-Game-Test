using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListenerMB : MonoBehaviour
{
    public List<EventSO> events = new List<EventSO>();
    public UnityEvent response;

    void OnEnable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i].Register(this);
        }
    }

    void OnDisable()
    {
        for (int i = 0; i < events.Count; i++)
        {
            events[i].Unregister(this);
        }
    }

    public void OnRaiseEvent()
    {
        response.Invoke();
    }
}
