using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class CardListSO : ScriptableObject
{
    [SerializeField] CardDictionarySO cardDictionary;
    [SerializeField] List<int> cardIDList = new List<int>();
    public int Count => cardIDList.Count;

    private void OnValidate()
    {
        Assert.IsTrue(cardDictionary != null, "Card Dictionary asset missing.");
    }

    public int GetCardID(int cardListIndex)
    {
        return cardIDList[cardListIndex];
    }

    public CardOriginal GetCardOriginal(int cardListIndex)
    {
        bool cardExists = cardDictionary.TryGetCard(cardIDList[cardListIndex], out var cardOriginal);
        Assert.IsTrue(cardExists, $"Card with ID {cardIDList[cardListIndex]} does not exist!");
        return cardOriginal;
    }
}
