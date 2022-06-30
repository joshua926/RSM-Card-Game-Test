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

    public CardOriginal Card { get; private set; }

    public void Init(int cardID)
    {
        bool foundCard = cardDictionary.TryGetCard(cardID, out var card);
        Assert.IsTrue(foundCard, $"Failed to find card in Card Dictionary with ID {cardID}.");
        Card = card;
        bool foundCardArt = cardArtDictionary.TryGetArt(Card.ImageId, out var artSprite);
        Assert.IsTrue(foundCardArt, $"Failed to find card art in Card Art Dictionary with ImageID {Card.ImageId}.");
        mainArtRenderer.sprite = artSprite;
        costUI.text = Card.Cost.ToString();
        nameUI.text = Card.Name;
        typeUI.text = Card.Type;
        textUI.text = GenerateEffectText();
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
