using System;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "ItemData", menuName = "ScriptableObjects/ItemData")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public Sprite Icon { get; set; }
        [field: SerializeField] public int CapacitySlot { get; set; }

        public static bool operator ==(ItemData item1, ItemData item2)
        {
            return item1?.Id == item2?.Id;
        }
        public static bool operator !=(ItemData item1, ItemData item2)
        {
            return item1?.Id != item2?.Id;
        }
        public override bool Equals(object obj)
        {
            return obj is ItemData data &&
                   base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode());
        }
    }
}
