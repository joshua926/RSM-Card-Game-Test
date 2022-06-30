using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
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

    /// <summary>
    /// Returns a deep copy of this card.
    /// </summary>
    public Card Duplicate()
    {
        var effectsCopy = new Effect[effects.Length];
        for (int i = 0; i < effects.Length; i++)
        {
            effectsCopy[i] = effects[i].Duplicate();
        }
        var copy = new Card
        {
            id = id,
            name = name,
            cost = cost,
            image_id = image_id,
            effects = effectsCopy,
        };
        return copy;
    }

    [System.Serializable]
    public class Effect
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

        /// <summary>
        /// Returns a deep copy of effect.
        /// </summary>
        public Effect Duplicate()
        {
            var copy = new Effect()
            {
                type = type,
                value = value,
                target = target,
            };
            return copy;
        }

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
