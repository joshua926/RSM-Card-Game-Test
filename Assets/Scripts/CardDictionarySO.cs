using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class CardDictionarySO : ScriptableObject
{
    [SerializeField, NonReorderable] Card[] cards;
    Dictionary<int, Card> dictionary = new Dictionary<int, Card>();
    public int Count => dictionary.Count;

    public void Init()
    {
        dictionary.Clear();
        int length = cards == null ? 0 : cards.Length;        
        for (int i = 0; i < length; i++)
        {
            var card = cards[i];
            if (!dictionary.ContainsKey(card.Id))
            {
                dictionary.Add(card.Id, card);
            }
        }
    }

    public bool ContainsID(int cardID)
    {
        return dictionary.ContainsKey(cardID);
    }

    public Card GetCardDuplicate(int cardID)
    {
        Assert.IsTrue(Count != 0, "Card Dictionary count is 0. Are you trying to access it before it has finished being loaded from a web request?");
        bool cardExists = ContainsID(cardID);
        Assert.IsTrue(cardExists, $"Card ID {cardID} does not exist.");
        var card = dictionary[cardID];
        return card.Duplicate();
    }

    /// <summary>
    /// Parses a card collection from a json file then calls InitDictionary.
    /// </summary>
    public void LoadFromJSON(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
        Init();
    }
}
