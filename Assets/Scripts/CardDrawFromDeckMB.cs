using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// This is responsible for drawing cards from the deck. Not to be confused with something that renders cards.
/// </summary>
public class CardDrawFromDeckMB : MonoBehaviour
{
    [SerializeField] RuntimeSetOfCardsSO deck;
    [SerializeField] RuntimeSetOfCardsSO hand;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject canvas;
    [SerializeField] EventSO onCardDrawn;    

    public void DrawCard()
    {
        AssertDependencies();

        var card = deck.GetAndRemoveAt(deck.Count - 1);
        hand.Add(card);
        var cardInitializer = Instantiate(cardPrefab, canvas.transform).GetComponent<CardInitializerMB>();
        cardInitializer.Init(card);
        onCardDrawn.RaiseEvent();
    }

    void AssertDependencies()
    {
        AssertionHelper.AssertUnityObject(deck);
        AssertionHelper.AssertUnityObject(hand);
        AssertionHelper.AssertUnityObject(cardPrefab);
        AssertionHelper.AssertUnityObject(cardPrefab.GetComponent<CardInitializerMB>());
        AssertionHelper.AssertUnityObject(canvas);
        AssertionHelper.AssertUnityObject(canvas.GetComponent<Canvas>());
        AssertionHelper.AssertUnityObject(onCardDrawn);
    }
}