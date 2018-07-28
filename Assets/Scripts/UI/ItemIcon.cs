using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
	public class ItemIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private Item _item;     // TODO: remove

		public ItemSlot Slot;
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

		public SlotPosition Position
		{
			get
			{
				return Slot.Position;
			}
		}

		public bool IsEquipmentIcon
		{
			get
			{
				return Position.RowIndex == EquipmentSlot.EQUIPMENT_SLOT_ROW_INDEX;
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

		public void OnPointerClick(PointerEventData eventData)
		{
			object[] eventParams = { (object)this };
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				EventManager.TriggerEvent(EventName.LEFT_CLICK_ITEM_ICON, eventParams);
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				EventManager.TriggerEvent(EventName.RIGHT_CLICK_ITEM_ICON, eventParams);
			}
			if (eventData.button == PointerEventData.InputButton.Middle)
			{
				EventManager.TriggerEvent(EventName.MIDDLE_CLICK_ITEM_ICON, eventParams);
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
