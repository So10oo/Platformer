using UnityEngine;

namespace Inventory
{
    public class PutOnSlotController : BaseSlotController
    {
        [field: SerializeField] public TypePutOnItem Type { get; private set; }
        protected override bool CanSwitch(BaseSlotController slotController)
        {
            if (slotController.slot.IsEmpty)
                return true;
            return slotController.slot.Item is PutOnItemData putOnItemData && putOnItemData.Type == Type;
        }
    }

}
