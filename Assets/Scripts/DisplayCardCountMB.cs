using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCardCountMB : MonoBehaviour
{
    [SerializeField] RuntimeSetOfCardsSO cards;
    [SerializeField] TextMeshProUGUI textUI;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        textUI.text = cards.Count.ToString();
    }
}
