using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class EntryPoint: MonoBehaviour
    {
        [SerializeField] InventoryGridController _inventoryGridController;
        [SerializeField] ItemData[] _itemIds;

        InventoryGrid inventoryGrid;
        private void Start()
        {
            var inventoryDataPlayer = CreateTestInventory();
            inventoryGrid = new InventoryGrid(inventoryDataPlayer);
            //var inventory = new InventoryGridController(inventoryGrid, InventoryView);
            _inventoryGridController.Initialize(inventoryGrid);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                var rIndex = Random.Range(0, _itemIds.Length);
                var rItemId = _itemIds[rIndex];
                var rAmount = Random.Range(1, 2);
                //Debug.Log($"Try to find item {rItemId}, Amount{rAmount}");
                var result = inventoryGrid.AddItems(rItemId, rAmount);
                //Debug.Log($"Item added: ${rItemId}. Amount added: {result.ItemsAddedAmount}");
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                var rIndex = Random.Range(0, _itemIds.Length);
                var rItemId = _itemIds[rIndex];
                var rAmount = Random.Range(0, 50);
                var result = inventoryGrid.RemoveItems(rItemId, rAmount);
                //Debug.Log($"Item removed: ${rItemId}. Truying to remove: {result.ItemsToRemoveAmount}. Success: {result.Success}");
            }
        }

        private InventoryGridData CreateTestInventory()
        {
            var size = new Vector2Int(4, 4);
            var createdInventorySlots = new List<InventorySlotData>();
            var length = size.x * size.y;
            for (var i = 0;i < length;i++)
            {
                createdInventorySlots.Add(new InventorySlotData());
            }

            var createdInventoryData = new InventoryGridData 
            { 
                //OwnerId = ownerId,
                Size = size,
                Slots = createdInventorySlots
            };

            return createdInventoryData;
        }
    }
}
