using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
	public class ItemIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private PlayerController _playerController;
		private int _rowIndex;
		private int _slotIndex;
		private Item _item;		// TODO: remove

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

		public int RowIndex
		{
			get
			{
				return _rowIndex;
			}
		}

		public int SlotIndex
		{
			get
			{
				return _slotIndex;
			}
		}

		public bool IsEquipmentIcon
		{
			get
			{
				return _rowIndex == EquipmentSlot.EQUIPMENT_SLOT_ROW_INDEX;
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

		public void SetInformation(PlayerController playerController, int rowIndex, int slotIndex)
		{
			_playerController = playerController;
			_rowIndex = rowIndex;
			_slotIndex = slotIndex;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				_playerController.InteractWithItemIcon(PlayerController.ItemAction.Select, this);
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				_playerController.InteractWithItemIcon(PlayerController.ItemAction.Equip, this);
			}
			if (eventData.button == PointerEventData.InputButton.Middle)
			{
				_playerController.InteractWithItemIcon(PlayerController.ItemAction.Consume, this);
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			object[] eventParams = { (object)_item.ToString() };
			EventManager.TriggerEvent(EventName.OPEN_TOOLTIP, eventParams);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			EventManager.TriggerEvent(EventName.CLOSE_TOOLTIP);
		}
	}
}
