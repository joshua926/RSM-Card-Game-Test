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
        [SerializeField] GameObject cardPrefab;
        [SerializeField] Transform parentCanvas;
        [SerializeField] Transform transformToCopy;
        [SerializeField] CircleSlotPositionerMB handPositions;
        [SerializeField] CircleSlotPositionerMB mouseHoverPositions;
        [SerializeField] Transform discardPileTransform;
        [SerializeField] PoolMB cardPool;

        public void InstantiateCard(Card card, int handSlot)
        {
            AssertDependencies();

            var cardGO = cardPool.Get(parentCanvas);
            var cardInitializer = cardGO.GetComponent<CardInitializerMB>();
            cardInitializer.transform.position = transformToCopy.position;
            cardInitializer.transform.rotation = transformToCopy.rotation;
            cardInitializer.transform.localScale = transformToCopy.localScale;
            cardInitializer.Init(
                card,
                handSlot,
                handPositions,
                mouseHoverPositions,
                discardPileTransform);
            cardInitializer.drawAnimator.StartAnimation();
        }

        void AssertDependencies()
        {
            Assert.IsNotNull(cardPrefab);
            Assert.IsNotNull(cardPrefab.GetComponent<CardInitializerMB>());
            Assert.IsNotNull(parentCanvas);
            Assert.IsNotNull(parentCanvas.GetComponent<Canvas>());
        }
    }
}