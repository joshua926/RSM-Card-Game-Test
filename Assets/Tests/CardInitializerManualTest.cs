using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardInitializerMB))]
public class CardInitializerManualTest : MonoBehaviour
{
    [SerializeField] CardDictionarySO cardDictionary;
    [SerializeField] int cardID;
    CardInitializerMB initializer;

    void Start()
    {
        initializer = GetComponent<CardInitializerMB>();
        StartCoroutine(WaitForCardDictionaryToLoadFromWeb());
    }

    IEnumerator WaitForCardDictionaryToLoadFromWeb()
    {
        float startTime = Time.realtimeSinceStartup;
        while(initializer.cardDictionary.Count == 0 && Time.realtimeSinceStartup - startTime < 3)
        {
            yield return null;
        }
        InitCard();
    }

    void InitCard()
    {
        var card = cardDictionary.GetCardDuplicate(cardID);
        initializer.Init(card);
    }
}
