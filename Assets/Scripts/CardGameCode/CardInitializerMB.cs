using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UI;
using GeneralPurpose;

namespace RSMCardGame
{
    public class CardInitializerMB : MonoBehaviour
    {
        public CardDictionarySO cardDictionary;
        public CardArtDictionarySO cardArtDictionary;
        [SerializeField] Image mainArtRenderer;
        [SerializeField] TextMeshProUGUI costUI;
        [SerializeField] TextMeshProUGUI nameUI;
        [SerializeField] TextMeshProUGUI typeUI;
        [SerializeField] TextMeshProUGUI textUI;
        public TransformAnimatorMB drawAnimator;
        public TransformAnimatorMB mouseOverAnimator;
        public TransformAnimatorMB discardAnimator;
        CircleSlotPositionerMB handPositioner;
        CircleSlotPositionerMB mouseOverCardPositioner;
        int handSlotIndex;

        public Card Card { get; private set; }

        public void Init(
            Card card,
            int handSlotIndex,
            CircleSlotPositionerMB handPositioner,
            CircleSlotPositionerMB mouseOverCardPositioner,
            Transform discardPileTransform)
        {
            this.handSlotIndex = handSlotIndex;
            this.handPositioner = handPositioner;
            this.mouseOverCardPositioner = mouseOverCardPositioner;
            Card = card;
            mainArtRenderer.sprite = cardArtDictionary.GetCardArt(Card.ImageId);
            costUI.text = Card.Cost.ToString();
            nameUI.text = Card.Name;
            typeUI.text = Card.Type;
            textUI.text = GenerateEffectText();
            drawAnimator.destination = handPositioner.GetSlot(handSlotIndex);
            mouseOverAnimator.destination = mouseOverCardPositioner.GetSlot(handSlotIndex);
            discardAnimator.destination = discardPileTransform;
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
    }
}