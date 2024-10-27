using System;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventorySlotData
    {
        public ItemData Item;
        public int Amount;
    }
}