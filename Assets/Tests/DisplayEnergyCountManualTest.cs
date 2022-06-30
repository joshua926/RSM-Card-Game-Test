using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayEnergyCountManualTest : MonoBehaviour
{
    [SerializeField] IntSO energyCountCurrent;
    [SerializeField] EventSO onEnergyCountChanged;
    [SerializeField] float timeInterval = 2f;
    [SerializeField] int changeValue = -1;
    [SerializeField] int loopCount = 4;

    private void Start()
    {
        StartCoroutine(ChangeEnergyCount());
    }

    IEnumerator ChangeEnergyCount()
    {
        for (int i = 0; i < loopCount; i++)
        {
            yield return new WaitForSeconds(timeInterval);
            energyCountCurrent.Value += changeValue;
            onEnergyCountChanged.RaiseEvent();
        }
    }
}
