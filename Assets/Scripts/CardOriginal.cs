using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The original definition of a card. Not to be confused with instantiated card copies.
/// </summary>
[System.Serializable]
public class CardOriginal
{
    [SerializeField] int id;
    [SerializeField] string name;
    [SerializeField] int cost;
    [SerializeField] string type;
    [SerializeField] int image_id;
    [SerializeField, NonReorderable] Effect[] effects;
    public int Id => id;
    public string Name => name;
    public int Cost => cost;
    public string Type => type;
    public int ImageId => image_id;
    public int EffectsCount => effects.Length;

    public Effect GetEffect(int i)
    {
        return effects[i];
    }

    [System.Serializable]
    public struct Effect
    {
        [SerializeField] string type;
        [SerializeField] int value;
        [SerializeField] string target;
        public string Type => type;
        public int Value => value;
        public string Target => target;
        public static string[] KnownTypes = new string[]
        {
            "damage",
            "shield",
            "strength",
        };
        public static string[] KnownTargets = new string[]
        {
            "enemy",
            "self",
        };

        public string GenerateText()
        {
            string target = "";
            string verb = "";
            string value = Value.ToString();
            string resource = "";

            switch (Type)
            {
                case "damage":
                    verb = "take";
                    resource = "damage";
                    break;
                case "shield":
                    verb = "gain";
                    resource = "defense";
                    break;
                case "strength":
                    verb = "gain";
                    resource = "strength";
                    break;
                default:
                    break;
            }

            switch (Target)
            {
                case "enemy":
                    target = "Your enemy";
                    verb += "s";
                    break;
                case "self":
                    target = "You";
                    break;
                default:
                    break;
            }

            return $"{target} {verb} {value} {resource}.";
        }
    }
}
