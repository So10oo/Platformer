using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryGrid /*: IReadOnlyInventoryGrid*/
    {
        public event Action<ItemData, int> ItemsAdded;
        public event Action<ItemData, int> ItemsRemoved;

        private readonly InventoryGridData _data;
        private readonly Dictionary<Vector2Int, InventorySlot> _slotsMap = new();

        public InventoryGrid(InventoryGridData data)
        {
            _data = data;
            var size = _data.Size;
            for (var i = 0; i < size.x; i++)
            {
                for (var j = 0; j < size.y; j++)
                {
                    var index = i * size.y + j;
                    var slotData = data.Slots[index];
                    var slot = new InventorySlot(slotData);
                    var position = new Vector2Int(i, j);

                    _slotsMap[position] = slot;
                }
            }
        }

        public Vector2Int Size
        {
            get => _data.Size;
            set
            {
                if (_data.Size != value)
                    _data.Size = value;
            }
        }

        public int GetAmount(ItemData itemId)
        {
            var amount = 0;
            var slots = _data.Slots;

            foreach (var slot in slots)
            {
                if (slot.Item == itemId)
                {
                    amount += slot.Amount;
                }
            }
             
            return amount;
        }

        public bool Has(ItemData itemId, int amount)
        {
            var amountExist = GetAmount(itemId);
            return amountExist >= amount;
        }

        //public void SwitchSlots(Vector2Int slotCoordsA, Vector2Int slotCoordsB)
        //{
        //    var slotA = _slotsMap[slotCoordsA];
        //    var slotB = _slotsMap[slotCoordsB];

        //    (slotA.Item, slotB.Item) = (slotB.Item, slotA.Item);
        //    (slotA.Amount, slotB.Amount) = (slotB.Amount, slotA.Amount);
        //}

        public InventorySlot[,] GetSlots()
        {
            var array = new InventorySlot[Size.x, Size.y];

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var position = new Vector2Int(i, j);
                    array[i, j] = _slotsMap[position];
                }
            }
            return array;
        }

        public AddItemsToInventoryGridResult AddItems(ItemData itemId, int amount = 1)
        {
            var remainingAmount = amount;
            var itemsAddedToSlotWithSameItemsAmount = AddToSlotWithSameItems(itemId, remainingAmount, out remainingAmount);

            if (remainingAmount <= 0)
            {
                ItemsAdded?.Invoke(itemId, amount);
                return new AddItemsToInventoryGridResult(/*OwnerId,*/ amount, itemsAddedToSlotWithSameItemsAmount);
            }

            var itemsAddedToAvailableSlotAmount = AddItemsAddedToAvailableSlot(itemId, remainingAmount, out remainingAmount);
            var totalAddedAmount = itemsAddedToSlotWithSameItemsAmount + itemsAddedToAvailableSlotAmount;

            ItemsAdded?.Invoke(itemId, amount);
            return new AddItemsToInventoryGridResult(/*OwnerId,*/ amount, totalAddedAmount);

        }

        public AddItemsToInventoryGridResult AddItems(Vector2Int slotCoords, ItemData itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];
            var newValue = slot.Amount + amount;
            var itemsAddedAmount = 0;

            if (slot.IsEmpty)
            {
                slot.Item = itemId;
            }

            var itemSlotCapacity = GetItemSlotCapacity(itemId);

            if (newValue > itemSlotCapacity)
            {
                var remainingItems = newValue - itemSlotCapacity;
                var itemsToAddAmount = itemSlotCapacity - slot.Amount;
                itemsAddedAmount += itemsToAddAmount;
                slot.Amount = itemSlotCapacity;

                var result = AddItems(itemId, remainingItems);
                itemsAddedAmount += result.ItemsAddedAmount;
            }
            else
            {
                itemsAddedAmount = amount;
                slot.Amount = newValue;
            }

            ItemsAdded?.Invoke(itemId, amount);
            return new AddItemsToInventoryGridResult(/*OwnerId, */amount, itemsAddedAmount);
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(ItemData itemId, int amount = 1)
        {
            if (!Has(itemId, amount)) {
                return new RemoveItemsFromInventoryGridResult(/*OwnerId,*/ amount, false);
            }

            var amountToRemove = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var slotCoords = new Vector2Int(i, j);
                    var slot = _slotsMap[slotCoords];

                    if (slot.Item != itemId)
                    {
                        continue;
                    }

                    if (amountToRemove > slot.Amount)
                    {
                        amountToRemove -= slot.Amount;

                        RemoveItems(slotCoords, itemId, slot.Amount);
                    }
                    else
                    {
                        RemoveItems(slotCoords, itemId, amountToRemove);

                        
                        return new RemoveItemsFromInventoryGridResult(/*OwnerId,*/ amount, true);                        
                    }
                }
            }

            throw new Exception("Something went wrong, couldn't remove some items");
        }

        public RemoveItemsFromInventoryGridResult RemoveItems(Vector2Int slotCoords, ItemData itemId, int amount = 1)
        {
            var slot = _slotsMap[slotCoords];

            if (slot.IsEmpty || slot.Item != itemId || slot.Amount < amount)
            {
                return new RemoveItemsFromInventoryGridResult(/*OwnerId,*/ amount, false);
            }

            slot.Amount -= amount;

            if (slot.Amount == 0)
            {
                slot.Item = null;
            }

            ItemsRemoved?.Invoke(itemId, amount);
            return new RemoveItemsFromInventoryGridResult(/*OwnerId,*/ amount, true);
        }
        
        private int AddToSlotWithSameItems(ItemData itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (slot.IsEmpty)
                    {
                        continue;
                    }

                    var slotItemCapacity = GetItemSlotCapacity(itemId);
                    if (slot.Amount >= slotItemCapacity)
                    {
                        continue;
                    }

                    if (slot.Item != itemId)
                    {
                        continue;
                    }

                    var newValue = slot.Amount + remainingAmount;
                    if (newValue > slotItemCapacity)
                    {
                        remainingAmount = newValue - slotItemCapacity;
                        var itemsToAddAmount = slotItemCapacity - slot.Amount;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapacity;

                        if (remainingAmount == 0)
                        {
                            return itemsAddedAmount;
                        }
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }
            return itemsAddedAmount;
        }

        private int AddItemsAddedToAvailableSlot(ItemData itemId, int amount, out int remainingAmount)
        {
            var itemsAddedAmount = 0;
            remainingAmount = amount;
            //Item item = AllServices.Container.Single<IItemService>().GetItemInfo(itemId);

            for (var i = 0; i < Size.x; i++)
            {
                for (var j = 0; j < Size.y; j++)
                {
                    var coords = new Vector2Int(i, j);
                    var slot = _slotsMap[coords];

                    if (!slot.IsEmpty)
                    {
                        continue;
                    }

                    slot.Item = itemId;
                    //slot.ItemSprite = item.Icon;
                    var newValue = remainingAmount;
                    var slotItemCapaclty = GetItemSlotCapacity(slot.Item);

                    if (newValue > slotItemCapaclty)
                    {
                        remainingAmount = newValue - slotItemCapaclty;
                        var itemsToAddAmount = slotItemCapaclty;
                        itemsAddedAmount += itemsToAddAmount;
                        slot.Amount = slotItemCapaclty;
                    }
                    else
                    {
                        itemsAddedAmount += remainingAmount;
                        slot.Amount = newValue;
                        remainingAmount = 0;

                        return itemsAddedAmount;
                    }
                }
            }
            return itemsAddedAmount;
        }

        private int GetItemSlotCapacity(ItemData itemId)
        {
            return itemId.CapacitySlot;
        }
    }
}
