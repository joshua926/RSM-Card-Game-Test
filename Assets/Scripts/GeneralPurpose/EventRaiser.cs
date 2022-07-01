using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralPurpose
{
    [System.Serializable]
    public class EventRaiser
    {
        public List<EventSO> events = new List<EventSO>();

        public void RaiseEvents()
        {
            for (int i = 0; i < events.Count; i++)
            {
                events[i]?.RaiseEvent();
            }
        }
    }
}