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
			playerInventory.AddItem(this);
			Destroy(gameObject);
		}

		public void OnDrop(InventoryController playerInventory)
		{
			Debug.Log("Drop " + Name);
			playerInventory.RemoveItem(this);
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
