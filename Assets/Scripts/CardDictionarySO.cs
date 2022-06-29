using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[CreateAssetMenu]
public class CardDictionarySO : ScriptableObject
{
    [SerializeField, NonReorderable] CardOriginal[] cards;
    Dictionary<int, CardOriginal> dictionary = new Dictionary<int, CardOriginal>();
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

    public bool TryGetCard(int id, out CardOriginal card)
    {
        Assert.IsTrue(Count != 0, "Card Dictionary count is 0. Are you trying to access it before it has finished being loaded from a web request?");
        return dictionary.TryGetValue(id, out card);
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