using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class CardCollectionTests
{
    [Test]
    public void LoadFromJSON_GetCardByID()
    {        
        var collection = ScriptableObject.CreateInstance<CardCollection>();
        collection.LoadFromJSON(json);

        CompareCard(collection.GetCardByID(0),
            0,
            "sword",
            1,
            "power",
            50,
            "damage",
            6,
            "enemy");
        CompareCard(collection.GetCardByID(1),
            1,
            "shield",
            2,
            "skill",
            77,
            "shield",
            5,
            "self");
        CompareCard(collection.GetCardByID(2),
            2,
            "fortitude",
            2,
            "power",
            18,
            "strength",
            2,
            "self");

        ScriptableObject.DestroyImmediate(collection, false);
    }    

    void CompareCard(CardOriginal card, int id, string name, int cost, string type, int image_id, string effectType, int effectValue, string effectTarget)
    {
        Assert.AreEqual(id, card.Id);
        Assert.AreEqual(name, card.Name);
        Assert.AreEqual(cost, card.Cost);
        Assert.AreEqual(image_id, card.ImageId);
        Assert.AreEqual(effectType, card.GetEffect(0).Type);
        Assert.AreEqual(effectValue, card.GetEffect(0).Value);
        Assert.AreEqual(effectTarget, card.GetEffect(0).Target);
    }

    static string json = @"
    {
      ""cards"": [
        {
          ""id"": 0,
          ""name"": ""sword"",
          ""cost"": 1,
          ""type"": ""power"",
          ""image_id"": 50,
          ""effects"":[
              {
                ""type"":""damage"",
                ""value"": 6,
                ""target"":""enemy""
              }
            ]
        },
        {
        ""id"": 1,
          ""name"": ""shield"",
          ""cost"": 2,
          ""type"": ""skill"",
          ""image_id"": 77,
          ""effects"":[
              {
            ""type"":""shield"",
                ""value"": 5,
                ""target"":""self""
              }
            ]
        },
        {
        ""id"": 2,
          ""name"": ""fortitude"",
          ""cost"": 2,
          ""type"": ""power"",
          ""image_id"": 18,
          ""effects"":[
              {
            ""type"":""strength"",
                ""value"": 2,
                ""target"":""self""
              }
            ]
        }
      ]
    }";
}
