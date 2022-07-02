using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralPurpose;

namespace RSMCardGame
{
    public class TurnMediatorMB : MonoBehaviour
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

        [SerializeField] CardDictionarySO cardDictionary;
        [SerializeField] DeckDescriptionSO deckDescription;
        [SerializeField] EventSO OnTurnStarted;
        [SerializeField] EventSO OnDeckShuffled;
        [SerializeField] EventSO OnMoveCardFromDiscardPileToDeck;
        [SerializeField] EventSO OnCardDrawn;
        [SerializeField] EventSO OnCardPlayed;
        [SerializeField] EventSO OnCardDiscardedPre;
        [SerializeField] EventSO OnCardDiscarded;
        [SerializeField] EventSO OnTurnEnded;
        [SerializeField] EventSO OnChangedEnergyCount;
        [SerializeField] EventSO OnChangedDeckCount;
        [SerializeField] EventSO OnChangedDiscardPileCount;
        [SerializeField] RuntimeSetOfCardsSO setOfDeckCards;
        [SerializeField] RuntimeSetOfCardsSO setOfHandCards;
        [SerializeField] RuntimeSetOfCardsSO setOfPlayedCards;
        [SerializeField] RuntimeSetOfCardsSO setOfDiscardedCards;
        [SerializeField] IntSO energyCountCurrent;
        [SerializeField] IntSO energyCountMax;
        [SerializeField] IntSO baseHandCount;
        [SerializeField] float durationOfDrawingCard = .5f;
        [SerializeField] float durationOfPlayingCard = 2f;
        [SerializeField] float durationOfDiscardingCard = 1f;
        [SerializeField] float durationOfMovingCardFromDiscardPileToDeck = .15f;
        public bool IsLegalTimeToPlayCard { get; private set; } = false;
        public float DurationOfDrawingCard => durationOfDrawingCard;
        public float DurationOfPlayingCard => durationOfPlayingCard;
        public float DurationOfDiscardingCard => durationOfDiscardingCard;
        public float DurationOfMovingCardFromDiscardPileToDeck => durationOfMovingCardFromDiscardPileToDeck;

        public void StartGame()
        {
            InitDeck();
            StartTurn();

            void InitDeck()
            {
                for (int i = 0; i < deckDescription.Count; i++)
                {
                    int cardID = deckDescription.GetCardID(i);
                    var card = cardDictionary.CreateCard(cardID);
                    setOfDeckCards.Add(card);
                }
                ShuffleDeck();
                OnChangedDeckCount.RaiseEvent();
            }
        }

        public void PlayCard(int handSlotIndex)
        {
            var card = setOfHandCards.GetAndRemoveAt(handSlotIndex);
            setOfPlayedCards.Add(card);
            energyCountCurrent.Value -= card.Cost;
            IsLegalTimeToPlayCard = false;
            OnChangedEnergyCount.RaiseEvent();
            OnCardPlayed.RaiseEvent();
            StartCoroutine(PlayCardWaitRoutine());

            IEnumerator PlayCardWaitRoutine()
            {
                yield return new WaitForSeconds(durationOfPlayingCard);
                IsLegalTimeToPlayCard = true;
                var card = setOfPlayedCards.GetAndRemoveAt(0);
                yield return AddCardToDiscardPileRoutine(card);
            }
        }

        public void EndTurn()
        {
            IsLegalTimeToPlayCard = false;
            StartCoroutine(DiscardHandRoutine(actionOnCompletion: () =>
            {
                OnTurnEnded.RaiseEvent();
                StartTurn();
            }));
        }

        void StartTurn()
        {
            energyCountCurrent.Value = energyCountMax.Value;
            OnChangedEnergyCount.RaiseEvent();
            OnTurnStarted.RaiseEvent();
            DrawHand(actionOnCompletion: () =>
            {
                IsLegalTimeToPlayCard = true;
            });
        }

        void DrawHand(Action actionOnCompletion = null)
        {
            StartCoroutine(DrawHandRoutine());

            IEnumerator DrawHandRoutine()
            {
                while (setOfHandCards.Count < baseHandCount.Value)
                {
                    if (setOfDeckCards.Count <= 0)
                    {
                        yield return MoveDiscardPileToDeckRoutine();
                        ShuffleDeck();
                    }
                    yield return DrawCard();
                }
                if (actionOnCompletion != null)
                {
                    actionOnCompletion();
                }
            }

            IEnumerator MoveDiscardPileToDeckRoutine()
            {
                for (int i = setOfDiscardedCards.Count - 1; i >= 0; i--)
                {
                    var card = setOfDiscardedCards.GetAndRemoveAt(i);
                    setOfDeckCards.Add(card);
                    OnMoveCardFromDiscardPileToDeck.RaiseEvent();
                    OnChangedDiscardPileCount.RaiseEvent();
                    yield return new WaitForSeconds(durationOfMovingCardFromDiscardPileToDeck);
                    OnChangedDeckCount.RaiseEvent();
                }
            }

            IEnumerator DrawCard()
            {
                var card = setOfDeckCards.GetAndRemoveAt(setOfDeckCards.Count - 1);
                setOfHandCards.Add(card);
                OnCardDrawn.RaiseEvent();
                OnChangedDeckCount.RaiseEvent();
                yield return new WaitForSeconds(durationOfDrawingCard);
            }
        }

        void ShuffleDeck()
        {
            setOfDeckCards.Shuffle();
            OnDeckShuffled.RaiseEvent();
        }

        IEnumerator DiscardHandRoutine(Action actionOnCompletion = null)
        {
            for (int i = setOfHandCards.Count - 1; i >= 0; i--)
            {
                Card card = setOfHandCards.GetAndRemoveAt(i);
                yield return AddCardToDiscardPileRoutine(card);
            }
            if (actionOnCompletion != null)
            {
                actionOnCompletion();
            }
        }

        IEnumerator AddCardToDiscardPileRoutine(Card card)
        {
            setOfDiscardedCards.Add(card);
            OnCardDiscardedPre.RaiseEvent();
            OnCardDiscarded.RaiseEvent();
            yield return new WaitForSeconds(durationOfDiscardingCard);
            OnChangedDiscardPileCount.RaiseEvent();
        }
    }
}