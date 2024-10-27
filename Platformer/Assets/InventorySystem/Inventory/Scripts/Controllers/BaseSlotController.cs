using UnityEngine;

namespace Inventory
{
    public class BaseSlotController : MonoBehaviour
    {
        public InventorySlot slot;

        public void Switch(BaseSlotController slotController)
        {
            if (this.CanSwitch(slotController) && slotController.CanSwitch(this))
            {
                Debug.Log("Switch");
                (slot.Item, slotController.slot.Item) = (slotController.slot.Item, slot.Item);
                (slot.Amount, slotController.slot.Amount) = (slotController.slot.Amount, slot.Amount);
            }
        }

        protected virtual bool CanSwitch(BaseSlotController slotController)
        {
            return true;
        }
    }
}
