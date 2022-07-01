using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// This is responsible for drawing cards from the deck. Not to be confused with something that renders cards.
/// </summary>
public class CardDrawManagerMB : MonoBehaviour
{
    [SerializeField] RuntimeSetOfCardsSO deck;
    [SerializeField] RuntimeSetOfCardsSO hand;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform canvasParent;
    [SerializeField] Transform toCopyOnInstantiate;
    [SerializeField] CircleSlotPositionerMB handPositions;
    [SerializeField] CircleSlotPositionerMB mouseHoverPositions;
    [SerializeField] Transform discardPileTransform;
    [SerializeField] float raiseEventDelay = .3f;
    [SerializeField] EventRaiser onCardDrawn;   

    public void DrawCard()
    {
        AssertDependencies();

        var card = deck.GetAndRemoveAt(deck.Count - 1);
        hand.Add(card);
        int handSlot = hand.Count - 1;
        var cardInitializer = Instantiate(cardPrefab, canvasParent.transform).GetComponent<CardInitializerMB>();
        cardInitializer.transform.position = toCopyOnInstantiate.position;
        cardInitializer.transform.rotation = toCopyOnInstantiate.rotation;
        cardInitializer.transform.localScale = toCopyOnInstantiate.localScale;
        cardInitializer.Init(
            card, 
            handPositions.GetSlot(handSlot),
            mouseHoverPositions.GetSlot(handSlot),
            discardPileTransform);
        StartCoroutine(WaitThenRaiseEvent());
    }

    IEnumerator WaitThenRaiseEvent()
    {
        yield return new WaitForSeconds(raiseEventDelay);
        onCardDrawn.RaiseEvents();
    }

    void AssertDependencies()
    {
        Assert.IsNotNull(deck);
        Assert.IsNotNull(hand);
        Assert.IsNotNull(cardPrefab);
        Assert.IsNotNull(cardPrefab.GetComponent<CardInitializerMB>());
        Assert.IsNotNull(canvasParent);
        Assert.IsNotNull(canvasParent.GetComponent<Canvas>());
    }
}