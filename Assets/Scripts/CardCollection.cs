using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardCollection : ScriptableObject
{
    [SerializeField, NonReorderable] CardOriginal[] cards;
    Dictionary<int, CardOriginal> dictionary;

    /// <summary>
    /// Returns card by ID. If ID does not exist then returns null.
    /// </summary>
    public CardOriginal GetCardByID(int id)
    {
        if (dictionary == null)
        {
            CreateDictionary();
        }
        dictionary.TryGetValue(id, out var card);
        return card;
    }

    public void LoadFromJSON(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    void CreateDictionary()
    {
        dictionary = new Dictionary<int, CardOriginal>(cards.Length);
        if (cards != null)
        {
            for (int i = 0; i < cards.Length; i++)
            {
                var card = cards[i];
                if (!dictionary.ContainsKey(card.Id))
                {
                    dictionary.Add(card.Id, card);
                }
            }
        }
    }
}
