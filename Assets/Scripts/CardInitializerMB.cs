using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CardInitializerMB : MonoBehaviour
{
    public CardDictionarySO cardDictionary;
    public CardArtDictionarySO cardArtDictionary;
    [SerializeField] Image mainArtRenderer;
    [SerializeField] TextMeshProUGUI costUI;
    [SerializeField] TextMeshProUGUI nameUI;
    [SerializeField] TextMeshProUGUI typeUI;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] TransformAnimatorMB drawAnimator;
    [SerializeField] TransformAnimatorMB mouseOverAnimator;
    [SerializeField] TransformAnimatorMB discardAnimator;

    public Card Card { get; private set; }

    public void Init(Card card, Transform handTransform, Transform mouseOverTransform, Transform discardPileTransform)
    {
        Card = card;
        mainArtRenderer.sprite = cardArtDictionary.GetCardArt(Card.ImageId);
        costUI.text = Card.Cost.ToString();
        nameUI.text = Card.Name;
        typeUI.text = Card.Type;
        textUI.text = GenerateEffectText();
        drawAnimator.destination = handTransform;
        mouseOverAnimator.destination = mouseOverTransform;
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
