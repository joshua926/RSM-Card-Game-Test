using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public CardOriginal Original { get; private set; }
    public Card(CardOriginal cardOriginal)
    {
        Original = cardOriginal;
    }
}
