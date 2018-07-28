using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class PickupableItem : Item, IPickupable
	{
		public void OnPutInInventory(InventoryController playerInventory, SlotPosition? slotPosition = null)
		{
			Debug.Log("PutInInventory " + Name);
			playerInventory.AddItem((Item)MemberwiseClone(), slotPosition);
		}

		public void OnRemoveFromInventory(InventoryController playerInventory, SlotPosition slotPosition)
		{
			Debug.Log("RemoveFromInventory " + Name);
			playerInventory.RemoveItem(slotPosition);
		}

		public void OnPutInAir(AirItemController _playerAirItem, SlotPosition slotPosition)
		{
			Debug.Log("PutInAir " + Name);
			_playerAirItem.AddItem((Item)MemberwiseClone(), slotPosition);
		}

		public void OnRemoveFromAir(AirItemController _playerAirItem)
		{
			Debug.Log("RemoveFromAir " + Name);
			_playerAirItem.RemoveItem();
		}

		public void OnPutOnGround()
		{
			// TODO
		}

		public void OnRemoveFromGround()
		{
			Destroy(gameObject);
		}

		public static PickupableItem Attach(GameObject itemObject, string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable)
		{
			PickupableItem item = itemObject.AddComponent<PickupableItem>();
			item._name = name;
			item._icon = icon;
			item._buffTable = buffTable;
			return item;
		}
	}
}
