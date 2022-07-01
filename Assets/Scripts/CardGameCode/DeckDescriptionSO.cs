using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using GeneralPurpose;

namespace RSMCardGame
{
    [CreateAssetMenu]
    public class DeckDescriptionSO : ScriptableObject
    {
        [SerializeField] List<int> cardIDList = new List<int>();
        public int Count => cardIDList.Count;

        public int GetCardID(int cardListIndex)
        {
            return cardIDList[cardListIndex];
        }
    }
}