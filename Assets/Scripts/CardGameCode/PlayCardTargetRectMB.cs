using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralPurpose;
using UnityEngine.EventSystems;

namespace RSMCardGame
{
    public class PlayCardTargetRectMB : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] BoolSO isCursorOverPlayCardTargetRect;

        private void Start()
        {
            transform.SetAsLastSibling(); // pointer enter will not be called if this is covered by other ui
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isCursorOverPlayCardTargetRect.Value = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isCursorOverPlayCardTargetRect.Value = false;
        }
    }
}
