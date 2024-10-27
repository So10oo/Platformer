namespace Inventory
{
    public class InventorySlotController : BaseSlotController
    {
        public void Initialize(InventorySlot readOnlySlot)
        {
            slot = readOnlySlot;
            var view = GetComponent<InventorySlotView>();  
            readOnlySlot.ItemAmountChanged += (amount) => view.Amount = amount;
            readOnlySlot.ItemChanged += (item) => view.ItemId = item;
        }
    }
}
