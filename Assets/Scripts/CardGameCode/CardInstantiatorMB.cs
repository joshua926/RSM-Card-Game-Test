using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using GeneralPurpose;

namespace RSMCardGame
{
    /// <summary>
    /// This is responsible for drawing cards from the deck. Not to be confused with something that renders cards.
    /// </summary>
    public class CardInstantiatorMB : MonoBehaviour
    {
        [SerializeField] GameObject card;
        [SerializeField] Transform cardParent;
        [SerializeField] PoolMB cardPool;

        public void InstantiateCard()
        {
            AssertDependencies();

            var cardGO = cardPool.Get(cardParent);
            var cardInitializer = cardGO.GetComponent<CardMB>();
            cardInitializer.Init();
            cardInitializer.StartDrawAnimation();
        }

        void AssertDependencies()
        {
            Assert.IsNotNull(cardParent);
            Assert.IsNotNull(cardPool);
        }
    }
}