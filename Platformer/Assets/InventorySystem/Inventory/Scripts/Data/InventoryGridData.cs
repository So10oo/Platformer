using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventoryGridData
    {
        public List<InventorySlotData> Slots;
        public Vector2Int Size;
    }
}
