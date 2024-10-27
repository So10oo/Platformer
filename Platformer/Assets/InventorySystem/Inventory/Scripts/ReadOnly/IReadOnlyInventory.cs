using System;

namespace Inventory
{
    public interface IReadOnlyInventory
    {
        event Action<ItemData, int> ItemsAdded;
        event Action<ItemData, int> ItemsRemoved;

        //string OwnerId { get; }

        int GetAmount(ItemData itemId);
        bool Has(ItemData itemId, int amount);
    }
}
