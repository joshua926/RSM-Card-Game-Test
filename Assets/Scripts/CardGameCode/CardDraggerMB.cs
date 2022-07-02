using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using GeneralPurpose;

namespace RSMCardGame
{
    public class CardDraggerMB : 
        MonoBehaviour, 
        IDragHandler, 
        IEndDragHandler, 
        IPointerEnterHandler, 
        IPointerExitHandler
    {
        [SerializeField] Canvas canvas;
        [SerializeField] CardMB cardMB;
        [SerializeField] BoolSO isCursoroverTarget;
        [SerializeField] TurnMediatorMB turnMediator;
        [SerializeField] UnityEvent onReturnToHand;
        [SerializeField] UnityEvent onMouseHover;

        public void OnDrag(PointerEventData eventData)
        {
            var rect = transform as RectTransform;
            rect.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isCursoroverTarget.Value)
            {
                turnMediator.PlayCard(cardMB.HandSlotIndex);
            }
            else
            {
                onReturnToHand?.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
            onMouseHover?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.SetSiblingIndex(cardMB.HandSlotIndex);
            onReturnToHand?.Invoke();
        }
    }
}
