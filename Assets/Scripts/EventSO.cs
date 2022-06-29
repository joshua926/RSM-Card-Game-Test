using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class EventSO : ScriptableObject
{
    [SerializeField] UnityEvent evt;

    public void RaiseEvent()
    {
        evt?.Invoke();
    }

    public void Subscribe(UnityAction action)
    {
        evt.AddListener(action);
    }

    public void Unsubscribe(UnityAction action)
    {
        evt.RemoveListener(action);
    }
}
