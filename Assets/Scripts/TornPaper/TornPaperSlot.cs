using System;
using UnityEngine;
using UnityEngine.EventSystems;


namespace LemApperson.TornPaper
{
    public class TornPaperSlot : MonoBehaviour,  IDropHandler
    {
        //
        //  No longer used. This feature snapped paper pieces
        //  to specific locations on the screen.
        // 
        
        // Use USPS TornPaper Abbreviations
        [SerializeField] public string _TornPaperName;
        private Vector3 _initialPosition;
        
        
        public void OnDrop(PointerEventData eventData)
        {
            String TornPaperName = eventData.pointerDrag.GetComponent<TornPaperPiece>()._TornPaperName;
            if(TornPaperName == _TornPaperName) {
                eventData.pointerDrag.transform.position = transform.position;
                eventData.pointerDrag.transform.rotation = transform.rotation;
                eventData.pointerDrag.transform.localScale = transform.localScale;
                eventData.pointerDrag.GetComponent<TornPaperPiece>()._reachedTornPaperSlot = true;
                //AudioManager.Instance.PlayClick();
            }
        }

    }
}