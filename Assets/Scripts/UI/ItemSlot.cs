using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventorySystem
{
	public struct SlotPosition
	{
		public readonly int RowIndex;
		public readonly int SlotIndex;

		public SlotPosition(int rowIndex, int slotIndex)
		{
			RowIndex = rowIndex;
			SlotIndex = slotIndex;
		}
	}

	public class ItemSlot : MonoBehaviour, IPointerClickHandler
	{
		public ItemIcon Icon;

		private SlotPosition _position;

		public SlotPosition Position
		{
			get
			{
				return _position;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return Icon.Item == null;
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			object[] eventParams = { (object)_position };
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				EventManager.TriggerEvent(EventName.LEFT_CLICK_ITEM_SLOT, eventParams);
			}
		}

		public void SetPosition(SlotPosition slotPosition)
		{
			_position = slotPosition;
		}

		public void UpdateSlot(Item item)
		{
			Icon.Item = item;
		}
	}
}
