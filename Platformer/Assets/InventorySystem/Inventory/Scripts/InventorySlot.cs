using System;

namespace Inventory
{
    public class InventorySlot : IReadOnlyInventorySlot
    {
        private readonly InventorySlotData _slotData;

        public event Action<ItemData> ItemChanged;
        public event Action<int> ItemAmountChanged;

        public ItemData Item 
        { 
            get => _slotData.Item; 
            set
            {
                if (_slotData.Item != value)
                {
                    _slotData.Item = value;
                    ItemChanged?.Invoke(value);
                }
            }
        }
        public int Amount 
        {
            get => _slotData.Amount;
            set
            {
                if (_slotData.Amount != value)
                {
                    _slotData.Amount = value;
                    ItemAmountChanged?.Invoke(value);
                }
            }
        }
        public bool IsEmpty => Amount == 0 && Item == null;

        public InventorySlot(InventorySlotData data)
        {
            _slotData = data;
        }
    }
}
