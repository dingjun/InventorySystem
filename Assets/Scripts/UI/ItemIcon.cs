using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
	public class ItemIcon : MonoBehaviour, IPointerClickHandler
	{
		private InventoryController _playerInventory;
		private int _rowIndex;
		private int _slotIndex;
		private Item _item;

		public Image ItemImage;
		public Text ItemCount;

		public Item Item
		{
			get
			{
				return _item;
			}
			set
			{
				_item = value;
				UpdateIcon();
			}
		}

		private void UpdateIcon()
		{
			if (_item == null)
			{
				ItemImage.sprite = null;
				ItemCount.text = "";
				gameObject.SetActive(false);
			}
			else
			{
				ItemImage.sprite = _item.Icon;
				// TODO: set up count
				gameObject.SetActive(true);
			}
		}

		public void SetInformation(InventoryController playerInventory, int rowIndex, int slotIndex)
		{
			_playerInventory = playerInventory;
			_rowIndex = rowIndex;
			_slotIndex = slotIndex;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				// TODO: toggle move
				((PickupableItem)_item).OnDrop(_playerInventory, _rowIndex, _slotIndex);
			}
			else if (eventData.button == PointerEventData.InputButton.Right)
			{
				// TODO: toggle equip
			}
			else    // eventData.button == PointerEventData.InputButton.Middle
			{
				// TODO: consume
			}
		}
	}
}
