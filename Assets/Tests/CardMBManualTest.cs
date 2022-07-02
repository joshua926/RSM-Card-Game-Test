using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RSMCardGame;
using GeneralPurpose;

namespace Tests
{
    [RequireComponent(typeof(CardMB))]
    public class CardInitializerManualTest : MonoBehaviour
    {
        [SerializeField] CardDictionarySO cardDictionary;
        [SerializeField] float waitTimeForDictionaryLoadingBeforeFailure = 2;
        [SerializeField] int cardID;
        CardMB initializer;

        void Start()
        {
            initializer = GetComponent<CardMB>();
            StartCoroutine(WaitForCardDictionaryToLoadFromWeb());
        }

        IEnumerator WaitForCardDictionaryToLoadFromWeb()
        {
            float startTime = Time.realtimeSinceStartup;
            while (cardDictionary.Count == 0 && Time.realtimeSinceStartup - startTime < waitTimeForDictionaryLoadingBeforeFailure)
            {
                yield return null;
            }
            InitCard();
        }

        void InitCard()
        {
            var hand = ScriptableObject.CreateInstance<RuntimeSetOfCardsSO>();
            hand.Add(cardDictionary.CreateCard(cardID));
            initializer.Init();
            Destroy(hand);
        }
    }
}