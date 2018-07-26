using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class PickupableItem : Item, IPickupable
	{
		public void OnPickup(InventoryController playerInventory)
		{
			Debug.Log("Pickup " + Name);
			playerInventory.AddItem((Item)MemberwiseClone());
			Destroy(gameObject);
		}

		public void OnDrop(InventoryController playerInventory, int rowIndex, int slotIndex)
		{
			Debug.Log("Drop " + Name);
			playerInventory.RemoveItem(rowIndex, slotIndex);
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
