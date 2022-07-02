using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UI;
using GeneralPurpose;

namespace RSMCardGame
{
    public class CardMB : MonoBehaviour
    {
        [SerializeField] CardArtDictionarySO cardArtDictionary;
        [SerializeField] Image mainArtRenderer;
        [SerializeField] TextMeshProUGUI costUI;
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] TextMeshProUGUI typeUI;
        [SerializeField] TextMeshProUGUI textUI;
        [SerializeField] TransformAnimatorMB drawAnimator;
        [SerializeField] TransformAnimatorMB mouseEnterAnimator;
        [SerializeField] TransformAnimatorMB returnToHandAnimator;
        [SerializeField] TransformAnimatorMB discardAnimator;
        [SerializeField] CircleSlotPositionerMB handPositioner;
        [SerializeField] CircleSlotPositionerMB mouseOverCardPositioner;
        [SerializeField] RuntimeSetOfCardsSO setOfHandCards;
        [SerializeField] PoolMB cardPool;
        public int HandSlotIndex { get; private set; }
        public Card Card { get; private set; }

        public void Init()
        {
            AssertDependencies();

            HandSlotIndex = setOfHandCards.Count - 1;
            Card = setOfHandCards[HandSlotIndex];
            mainArtRenderer.sprite = cardArtDictionary.GetCardArt(Card.ImageId);
            costUI.text = Card.Cost.ToString();
            nameUI.text = Card.Name;
            typeUI.text = Card.Type;
            textUI.text = GenerateEffectText();
            handPositioner.IncreaseSlotsToCount(HandSlotIndex + 1);
            mouseOverCardPositioner.IncreaseSlotsToCount(HandSlotIndex + 1);
            drawAnimator.destination = handPositioner.GetSlot(HandSlotIndex);
            mouseEnterAnimator.destination = mouseOverCardPositioner.GetSlot(HandSlotIndex);
            returnToHandAnimator.destination = handPositioner.GetSlot(HandSlotIndex);
            discardAnimator.onAnimationCompleted.AddListener(ReturnToPool);
        }

        public void StartDrawAnimation()
        {
            drawAnimator.StartAnimation();
        }

        public void StartMouseEnterAnimation()
        {
            mouseEnterAnimator.StartAnimation();
        }

        public void StartReturnToHandAnimation()
        {
            returnToHandAnimator.StartAnimation();
        }

        public void StartDiscardAnimation()
        {
            discardAnimator.StartAnimation();
        }

        void ReturnToPool()
        {
            cardPool.Return(gameObject);
            discardAnimator.onAnimationCompleted.RemoveListener(ReturnToPool);
        }

        string GenerateEffectText()
        {
            string effectText = "";
            for (int i = 0; i < Card.EffectsCount; i++)
            {
                effectText += $"{Card.GetEffect(i).GenerateText()} ";
            }
            return effectText;
        }

        void AssertDependencies()
        {
            Assert.IsNotNull(cardArtDictionary);
            Assert.IsNotNull(mainArtRenderer);
            Assert.IsNotNull(costUI);
            Assert.IsNotNull(nameUI);
            Assert.IsNotNull(typeUI);
            Assert.IsNotNull(textUI);
            Assert.IsNotNull(drawAnimator);
            Assert.IsNotNull(mouseEnterAnimator);
            Assert.IsNotNull(discardAnimator);
            Assert.IsNotNull(handPositioner);            
            Assert.IsNotNull(mouseOverCardPositioner);
            Assert.IsNotNull(setOfHandCards);
            Assert.IsTrue(setOfHandCards.Count > 0, "There are no cards in hand, but you are trying to initialize one.");
        }
    }
}