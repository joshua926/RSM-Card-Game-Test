using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardDictionary : ScriptableObject
{
    [SerializeField, NonReorderable] CardOriginal[] cards;
    Dictionary<int, CardOriginal> dictionary;
    
    public bool TryGetCard(int id, out CardOriginal card)
    {
        if (dictionary == null)
        {
            CreateDictionary();
        }
        return dictionary.TryGetValue(id, out card);
    }

    /// <summary>
    /// If ID does not exist then this will return null.
    /// </summary>
    public CardOriginal GetCard(int id)
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
