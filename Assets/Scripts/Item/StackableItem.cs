using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class StackableItem : Item, IPickupable, IStackable
	{
		private const int NO_STACK_LIMIT = Int32.MaxValue;

		private int _stackLimit;
		private int _count;

		public int StackLimit
		{
			get
			{
				return _stackLimit;
			}
		}

		public int Count
		{
			get
			{
				return _count;
			}
		}

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

		public void OnStack()
		{
			// TODO
		}

		public void OnSplit()
		{
			// TODO
		}

		public static StackableItem Attach(GameObject itemObject, string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable, int stackLimit)
		{
			StackableItem item = itemObject.AddComponent<StackableItem>();
			item._name = name;
			item._icon = icon;
			item._buffTable = buffTable;
			item._stackLimit = stackLimit;
			item._count = 1;
			return item;
		}
	}
}
