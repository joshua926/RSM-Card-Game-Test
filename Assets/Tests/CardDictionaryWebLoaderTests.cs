using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using RSMCardGame;
using GeneralPurpose;

namespace Tests
{
    public class CardDictionaryWebLoaderTests
    {
        [UnityTest]
        public IEnumerator CardDictionaryWebLoaderTestsWithEnumeratorPasses()
        {
            var cards = ScriptableObject.CreateInstance<CardDictionarySO>();
            var card = cards.CreateCard(0);
            Assert.That(cards.Count == 0);

            var loader = new GameObject().AddComponent<CardDictionaryWebLoaderMB>();
            string url = CardDictionaryWebLoaderAwakeCallerMB.defaultURL;
            loader.StartWebRequest(url, cards);

            float startTime = Time.realtimeSinceStartup;
            float maxWaitTime = 2f;

            while (!loader.IsRequestCompleted && Time.realtimeSinceStartup - startTime < maxWaitTime)
            {
                yield return null;
            }
            Assert.That(cards.Count > 0);

            GameObject.DestroyImmediate(loader, false);
            ScriptableObject.DestroyImmediate(cards, false);
        }
    }
}