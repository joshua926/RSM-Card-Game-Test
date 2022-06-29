using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class EventSO : ScriptableObject
{
    List<EventListenerMB> listeners = new List<EventListenerMB>();

    public void RaiseEvent()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnRaiseEvent();
        }
    }

    public void Register(EventListenerMB listener)
    {
        listeners.Add(listener);
    }

    public void Unregister(EventListenerMB listener)
    {
        listeners.Remove(listener);
    }
}
