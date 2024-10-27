using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory
{
    public class DropUISlot : MonoBehaviour, IDropHandler
    {
        public virtual void OnDrop(PointerEventData eventData)
        {
            Debug.Log("OnDrop");
            var slotDrag = eventData.pointerDrag.transform.parent.GetComponent<BaseSlotController>();
            var slotDrop = transform.GetComponent<BaseSlotController>();
            slotDrop.Switch(slotDrag);
        }
    }
}
 

