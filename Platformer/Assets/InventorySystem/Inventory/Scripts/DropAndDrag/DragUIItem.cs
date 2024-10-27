using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class DragUIItem : MonoBehaviour, IDragHandler ,IEndDragHandler ,IBeginDragHandler
    {
        RectTransform _rectTransform;
        CanvasGroup _canvasGroup;
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log("OnDrag");
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");
            _canvasGroup.blocksRaycasts = true;
            transform.localPosition = Vector3.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            var parent = _rectTransform.parent;
            parent.SetAsLastSibling();
        }
    }
}
