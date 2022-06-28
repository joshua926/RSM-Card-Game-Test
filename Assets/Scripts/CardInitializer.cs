using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardInitializer : MonoBehaviour
{
    [SerializeField] CardDictionary cardCollection;    
    [SerializeField] CardArtDictionary cardArtCollection;
    [SerializeField] SpriteRenderer mainArtRenderer;
    [SerializeField] TextMeshProUGUI costUI;
    [SerializeField] TextMeshProUGUI nameUI;
    [SerializeField] TextMeshProUGUI typeUI;
    [SerializeField] TextMeshProUGUI textUI;    

    public CardOriginal Card { get; private set; }

    public void Init(int cardID)
    {
        Card = cardCollection.GetCard(cardID);
        mainArtRenderer.sprite = cardArtCollection.GetArt(Card.ImageId);
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
