using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(CardDictionaryWebLoaderMB))]
public class CardDictionaryWebLoaderAwakeCallerMB : MonoBehaviour
{
    [Header("Loading cards from url: " + defaultURL)]
    [SerializeField] CardDictionarySO cardDictionary;
    [SerializeField] EventSO OnLoadCardDictionary;
    public const string defaultURL = "https://client.dev.kote.robotseamonster.com/TEST_HARNESS/json_files/cards.json";

    void Awake()
    {
        LoadCardDictionary();
    }

    void LoadCardDictionary()
    {
        Assert.IsTrue(cardDictionary, "Card Dictionary asset missing.");        
        var loader = GetComponent<CardDictionaryWebLoaderMB>();
        loader.StartWebRequest(defaultURL, cardDictionary, RaiseOnLoadCardDictionary);
    }

    void RaiseOnLoadCardDictionary()
    {
        Assert.IsTrue(OnLoadCardDictionary != null, "OnLoadCardDictionary event missing.");
        OnLoadCardDictionary.RaiseEvent();
    }
}
