using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LemApperson.TornPaper
{
    public class TornPaperPiece : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        private Camera _mainCamera;
        // Use USPS TornPaper Abbreviations
        [SerializeField] public string _TornPaperName;
        private Vector3 _initialPosition;
        private Image _image;
        public bool _reachedTornPaperSlot ;

        void Start()
        {
            _mainCamera = Camera.main;
            _image = GetComponent<Image>();
        }

        /// <summary>
        /// Moves the paper across the screen based on the pointer's movement.
        /// That position is translated from screen space to world scape because
        /// the paper piece is attached to a World Space canvas
        /// </summary>
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 screenPoint = new Vector3(eventData.position.x, eventData.position.y, _mainCamera.WorldToScreenPoint(transform.position).z);
            Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(screenPoint);
            transform.position = worldPosition;
        }
        
        /// <summary>
        /// When the mouse goes up, the paper piece is reset to opaque.
        /// raycastTarget allows the pointer to react to other interactables.
        /// </summary>
        public void OnEndDrag(PointerEventData eventData)
        {
            var temp = _image.color;
            temp.a = 1.0f;
            _image.color = temp;
            _image.raycastTarget = true;
        }

        /// <summary>
        /// When the mouse goes down, the paper piece goes semi-transparent.
        /// raycastTarget blocks the pointer from react to other interactables.
        /// </summary>
        public void OnBeginDrag(PointerEventData eventData)
        {
            var temp = _image.color;
            temp.a = 0.5f;
            _image.color = temp;
            _image.raycastTarget = false;
        }

    }
}
