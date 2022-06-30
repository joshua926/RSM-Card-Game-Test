using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayDeckCardCountManualTest : MonoBehaviour
{
    [SerializeField] RuntimeSetOfCardsSO deck;
    [SerializeField] EventSO onCardDrawn;
    [SerializeField] float timeInterval = 1f;
    [SerializeField] int loopCount = 5;

    public void StartDecrementingDeck()
    {
        StartCoroutine(ChangeDeckCount());
    }

    IEnumerator ChangeDeckCount()
    {
        for (int i = 0; i < loopCount && deck.Count > 0; i++)
        {
            yield return new WaitForSeconds(timeInterval);
            deck.RemoveAt(deck.Count - 1);
            onCardDrawn.RaiseEvent();
        }
    }
}
