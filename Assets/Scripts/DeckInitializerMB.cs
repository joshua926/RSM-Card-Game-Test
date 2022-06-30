using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DeckInitializerMB : MonoBehaviour
{
    [SerializeField] CardDictionarySO cardDictionary;
    [SerializeField] CardListSO cardList;
    [SerializeField] RuntimeSetOfCardsSO deck;
    [SerializeField] bool shuffleDeck = true;
    [SerializeField] EventSO OnDeckChanged;
    [SerializeField] EventSO OnTurnStarted;

    public void InitDeck()
    {
        for (int i = 0; i < cardList.Count; i++)
        {
            int cardID = cardList.GetCardID(i);
            deck.Add(cardDictionary.GetCardDuplicate(cardID));
        }
        if (shuffleDeck)
        {
            deck.Shuffle();
        }
        OnDeckChanged.RaiseEvent();
        OnTurnStarted.RaiseEvent();
    }
}
