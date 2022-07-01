using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralPurpose;

namespace RSMCardGame
{
    [CreateAssetMenu]
    public class PlayerTurnMediatorSO : ScriptableObject
    {
        //Player Turn
        //  Draw phase
        //    while(hand< 4)
        //      if(deck == 0)
        //        shuffle discard
        //      draw card
        //  Play Phase
        //    Can play card if enough energy
        //    Can click 'End Turn' to end turn
        //  End Turn Phase
        //    Discard hand
        //    Go to next turn

        [SerializeField] EventSO OnTurnStarted;
        [SerializeField] EventSO OnDeckShuffled;
        [SerializeField] EventSO OnCardDrawn;
        [SerializeField] EventSO OnCardPlayed;
        [SerializeField] EventSO OnTurnEnded;
        [SerializeField] EventSO OnChangedEnergyCount;
        [SerializeField] EventSO OnChangedDeckCount;
        [SerializeField] EventSO OnChangedDiscardPileCount;
        [SerializeField] RuntimeSetOfCardsSO deck;
        [SerializeField] RuntimeSetOfCardsSO hand;
        [SerializeField] RuntimeSetOfCardsSO playedCard;
        [SerializeField] RuntimeSetOfCardsSO discardPile;
        [SerializeField] DeckDescriptionSO deckList;
        [SerializeField] CardDictionarySO cardDictionary;
        [SerializeField] IntSO currentEnergyCount;
        [SerializeField, Min(0)] float x;

        public void StartGame()
        {
            InitDeck();
            StartTurn();
        }

        public bool TryPlayCard(int handSlotIndex)
        {
            var card = hand[handSlotIndex];
            if (currentEnergyCount.Value < card.Cost)
            {
                return false;
            }
            else
            {
                hand.RemoveAt(handSlotIndex);
                discardPile.Add(card);
                return true;
            }
        }

        public void EndTurn()
        {
            for (int i = hand.Count - 1; i >= 0; i--)
            {
                var card = hand.GetAndRemoveAt(i);
                discardPile.Add(card);
            }
            OnTurnEnded.RaiseEvent();
        }

        void InitDeck()
        {
            for (int i = 0; i < deckList.Count; i++)
            {
                int cardID = deckList.GetCardID(i);
                var card = cardDictionary.CreateCard(cardID);
                deck.Add(card);
            }
            ShuffleDeck();
        }

        void StartTurn()
        {

            OnTurnStarted.RaiseEvent();
        }

        void MoveDiscardPileToDeck()
        {
            for (int i = discardPile.Count - 1; i >= 0; i--)
            {
                var card = discardPile.GetAndRemoveAt(i);
                deck.Add(card);
            }
        }

        void ShuffleDeck()
        {
            deck.Shuffle();
            OnDeckShuffled.RaiseEvent();
        }

        void DrawCard()
        {

            OnCardDrawn.RaiseEvent();
            OnChangedDeckCount.RaiseEvent();
        }
    }
}