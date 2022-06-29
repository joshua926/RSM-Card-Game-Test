using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckInitializerMB : MonoBehaviour
{
    [SerializeField] CardListSO cardList;
    [SerializeField] RuntimeSetOfCardsSO deck;
    [SerializeField] bool shuffleDeck = true;

    public void InitDeck()
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            deck.Add(new Card(cardList.GetCardOriginal(i)));
        }
        if (shuffleDeck)
        {
            deck.Shuffle();
        }
        Debug.Log("Deck loaded");
        for (int i = 0; i < deck.Count; i++)
        {
            Debug.Log(deck[i].Original.Name);
        }
    }
}
