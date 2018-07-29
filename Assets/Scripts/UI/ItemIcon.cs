using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventorySystem
{
	public class ItemIcon : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		private Item _item;
		private bool _isMouseHovering;

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

		private void OnEnable()
		{
			EventManager.StartListening(EventName.TRIGGER_HOVER_HOTKEY, TriggerHoverHotkey);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.TRIGGER_HOVER_HOTKEY, TriggerHoverHotkey);
		}
		
		private void UpdateIcon()
		{
			if (_item == null)
			{
				ItemImage.sprite = null;
				ItemCount.text = "";
				gameObject.SetActive(false);
				_isMouseHovering = false;
			}
			else
			{
				ItemImage.sprite = _item.Icon;

				IStackable stackable = _item as IStackable;
				if (stackable == null)
				{
					ItemCount.text = "";
				}
				else
				{
					ItemCount.text = stackable.Count.ToString();
					if (stackable.IsNoStackLimit == false)
					{
						ItemCount.text += "/" + stackable.StackLimit.ToString();
					}
				}

				gameObject.SetActive(true);
			}
		}

		private void TriggerHoverHotkey(object[] eventParams)
		{
			if (_isMouseHovering == false)
			{
				return;
			}
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is KeyCode);
			KeyCode hotkey = (KeyCode)eventParams[0];

			object[] hotkeyEventParams = { (object)this };
			if (hotkey == InputManager.HOVER_SPLIT_HOTKEY)
			{
				EventManager.TriggerEvent(EventName.HOVER_ITEM_ICON_SPLIT_HOTKEY, hotkeyEventParams);
			}
			if (hotkey == InputManager.HOVER_DROP_HOTKEY)
			{
				EventManager.TriggerEvent(EventName.HOVER_ITEM_ICON_DROP_HOTKEY, hotkeyEventParams);
			}
			if (hotkey == InputManager.HOVER_EQUIP_HOTKEY)
			{
				EventManager.TriggerEvent(EventName.HOVER_ITEM_ICON_EQUIP_HOTKEY, hotkeyEventParams);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			object[] eventParams = { (object)this };
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				if (Input.GetKey(InputManager.LEFT_CLICK_DROP_HOTKEY))
				{
					EventManager.TriggerEvent(EventName.LEFT_CLICK_ITEM_ICON_DROP_HOTKEY, eventParams);
				}
				else
				{
					EventManager.TriggerEvent(EventName.LEFT_CLICK_ITEM_ICON, eventParams);
				}
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				EventManager.TriggerEvent(EventName.RIGHT_CLICK_ITEM_ICON, eventParams);
			}
			if (eventData.button == PointerEventData.InputButton.Middle)
			{
				EventManager.TriggerEvent(EventName.MIDDLE_CLICK_ITEM_ICON, eventParams);
			}
			EventManager.TriggerEvent(EventName.CLOSE_TOOLTIP);
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			_isMouseHovering = true;
			object[] eventParams = { (object)_item.ToString() };
			EventManager.TriggerEvent(EventName.OPEN_TOOLTIP, eventParams);
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			_isMouseHovering = false;
			EventManager.TriggerEvent(EventName.CLOSE_TOOLTIP);
		}
	}
}
