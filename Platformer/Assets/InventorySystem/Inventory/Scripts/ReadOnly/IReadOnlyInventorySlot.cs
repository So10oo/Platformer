using System;

namespace Inventory
{
    public interface IReadOnlyInventorySlot
    {
        event Action<ItemData> ItemChanged;
        event Action<int> ItemAmountChanged;

        ItemData Item { get; }
        int Amount { get; }
        bool IsEmpty { get; }
    }
}
