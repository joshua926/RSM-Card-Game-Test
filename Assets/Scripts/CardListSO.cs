using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class CardListSO : ScriptableObject
{
    [SerializeField] List<int> cardIDList = new List<int>();
    public int Count => cardIDList.Count;

    public int GetCardID(int cardListIndex)
    {
        return cardIDList[cardListIndex];
    }
}
