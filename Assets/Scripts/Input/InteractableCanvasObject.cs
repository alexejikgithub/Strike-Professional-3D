using UnityEngine;
using UnityEngine.EventSystems;

namespace Scripts.Input
{
    /// <summary>
    /// Used for Implementing Canvas based controls
    /// </summary>
    public class InteractableCanvasObject : GameplayInput, IBeginDragHandler, IEndDragHandler, IDragHandler,
        IPointerDownHandler
    {
        public delegate void OnMouseDownDelegate(Vector3 position);
        public event OnMouseDownDelegate OnMouseDownEvent;

        public delegate void OnMouseDragDelegate(Vector3 position);
        public event OnMouseDragDelegate OnMouseDragEvent;

        public delegate void OnMouseUpDelegate(Vector3 position);
        public event OnMouseUpDelegate OnMouseUpEvent;

        public delegate void OnPointerDownDelegate(Vector3 position);
        public event OnPointerDownDelegate OnPointerDownEvent;

        private bool _isGameplayInputOn = true;


        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isGameplayInputOn) return;

            OnMouseDownEvent?.Invoke(eventData.pointerPressRaycast.worldPosition);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isGameplayInputOn) return;
            OnMouseDragEvent?.Invoke(eventData.pointerPressRaycast.worldPosition);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isGameplayInputOn) return;
            OnMouseUpEvent?.Invoke(eventData.pointerPressRaycast.worldPosition);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isGameplayInputOn) return;


            OnPointerDownEvent?.Invoke(eventData.pointerPressRaycast.worldPosition);
        }

        public override void GameplayInputOn()
        {
            _isGameplayInputOn = true;
        }

        public override void GameplayInputOff()
        {
            _isGameplayInputOn = false;
        }
    }
}