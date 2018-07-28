using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class AirItemController : MonoBehaviour
	{
		private Item _item;
		private SlotPosition _originalPosition;

		public Item Item
		{
			get
			{
				return _item;
			}
		}
		
		public SlotPosition OriginalPosition
		{
			get
			{
				Debug.Assert(IsEmpty == false);
				return _originalPosition;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return _item == null;
			}
		}

		public override string ToString()
		{
			return "==== Air Item ====\n" + ((_item == null) ? "____" : _item.Name);
		}

		public void AddItem(Item item, SlotPosition slotPosition)
		{
			Debug.Assert(item is IPickupable);

			_item = item;
			_originalPosition = slotPosition;

			EventManager.TriggerEvent(EventName.UPDATE_AIR_ITEM);

			Debug.Log(this.ToString());
		}

		public void RemoveItem()
		{
			_item = null;

			EventManager.TriggerEvent(EventName.UPDATE_AIR_ITEM);

			Debug.Log(this.ToString());
		}
	}
}
