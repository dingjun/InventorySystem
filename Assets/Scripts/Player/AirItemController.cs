using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class AirItemController : MonoBehaviour
	{
		private Item _item;

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

		public void AddItem(Item item)
		{
			Debug.Assert(item is IPickupable);
			_item = item;

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
