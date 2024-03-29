using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GeneralPurpose;

namespace RSMCardGame
{
    public class DisplayEnergyCountMB : MonoBehaviour
    {
        [SerializeField] IntSO energyCountCurrent;
        [SerializeField] IntSO energyCountMax;
        [SerializeField] TextMeshProUGUI textUI;

        void Start()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            textUI.text = $"{energyCountCurrent.Value}/{energyCountMax.Value}";
        }
    }
}