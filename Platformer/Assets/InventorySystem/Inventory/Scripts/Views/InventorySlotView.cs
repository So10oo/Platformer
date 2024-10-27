using Inventory;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textTitle;
        [SerializeField] private TextMeshProUGUI _textAmount;
        [SerializeField] private Image _iconItem;

        public static Action<ItemData, int> OnInventoryButtonClicked;

        private ItemData _itemId;

        public string Title
        {
            get => _textTitle.text;
            set => _textTitle.text = value;
        }

        public int Amount
        {
            get => Convert.ToInt32(Amount);
            set => _textAmount.text = value == 0 ? "" : value.ToString();
        }

        public ItemData ItemId
        {
            get => _itemId;
            set
            {
                _itemId = value;
                Title = value?.Id;
                if (value?.Icon == null) 
                    _iconItem.color = Color.clear;
                else
                    _iconItem.color = Color.white;
                _iconItem.sprite = value?.Icon;

            }
        }

        //public void OnInventoryButtonClick()
        //{
        //    if (_itemId != null) OnInventoryButtonClicked?.Invoke(_itemId, Convert.ToInt32(_textAmount.text));
        //}
    }
}
 
