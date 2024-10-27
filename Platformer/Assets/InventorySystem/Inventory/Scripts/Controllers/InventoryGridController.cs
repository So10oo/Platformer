using UnityEngine;

namespace Inventory
{
    public class InventoryGridController : MonoBehaviour
    {
        [SerializeField] private InventorySlotController[] _slotControllers;

        public void Initialize(InventoryGrid inventory)
        {
            var size = inventory.Size;
            var slots = inventory.GetSlots();
            var lineLength = size.y;

            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var index = i * lineLength + j;
                    var slotControllers = GetInventorySlotController(index);
                    //var view = slotControllers.gameObject.GetComponent<InventoryView>();
                    slotControllers.Initialize(slots[i, j]);
                }
            }
        }

        public InventorySlotController GetInventorySlotController(int index)
        {
            return _slotControllers[index];
        }
    }
}
