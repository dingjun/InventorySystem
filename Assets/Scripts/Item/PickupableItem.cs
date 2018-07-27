using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class PickupableItem : Item, IPickupable
	{
		public void OnPutInInventory(InventoryController playerInventory)
		{
			Debug.Log("PutInInventory " + Name);
			playerInventory.AddItem((Item)MemberwiseClone());
		}

		public void OnRemoveFromInventory(InventoryController playerInventory, int rowIndex, int slotIndex)
		{
			Debug.Log("RemoveFromInventory " + Name);
			playerInventory.RemoveItem(rowIndex, slotIndex);
		}

		public void OnPutInAir(PlayerController playerController)
		{
			Debug.Log("PutInAir " + Name);
			//playerController.AddItem((Item)MemberwiseClone());
		}

		public void OnRemoveFromAir(PlayerController playerController)
		{
			Debug.Log("RemoveFromAir " + Name);
			//playerController.RemoveItem();
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
