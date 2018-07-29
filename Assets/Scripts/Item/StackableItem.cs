using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class StackableItem : Item, IPickupable, IStackable, IConsumable
	{
		private const int NO_STACK_LIMIT = int.MaxValue;

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
			set
			{
				_count = value;
			}
		}

		public bool IsNoStackLimit
		{
			get
			{
				return _stackLimit == NO_STACK_LIMIT;
			}
		}

		public StackableItem(string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable, int stackLimit = NO_STACK_LIMIT, int count = 1)
			: base(name, icon, buffTable)
		{
			_stackLimit = stackLimit;
			_count = count;
		}

		public override Item Copy()
		{
			return new StackableItem(_name, _icon, _buffTable, _stackLimit, _count);
		}

		public override string ToString()
		{
			return "Name: " + Name + "\nType: Stackable\nBuff: " + BuffTableToString();
		}

		public void OnPutInInventory(InventoryController playerInventory, SlotPosition? slotPosition = null)
		{
			Debug.Log("PutInInventory " + Name);
			playerInventory.AddItem(Copy(), slotPosition);
		}

		public void OnRemoveFromInventory(InventoryController playerInventory, SlotPosition slotPosition)
		{
			Debug.Log("RemoveFromInventory " + Name);
			playerInventory.RemoveItem(slotPosition);
		}

		public void OnPutInAir(AirItemController playerAirItem, SlotPosition slotPosition)
		{
			Debug.Log("PutInAir " + Name);
			playerAirItem.AddItem(Copy(), slotPosition);
		}

		public void OnRemoveFromAir(AirItemController playerAirItem)
		{
			Debug.Log("RemoveFromAir " + Name);
			playerAirItem.RemoveItem();
		}

		public void OnPutOnGround(Vector3 playerPosition)
		{
			Debug.Log("OnPutOnGround " + Name);
			object[] eventParams = { (object)playerPosition, (object)this };
			EventManager.TriggerEvent(EventName.SPAWN_ITEM_NEAR_PLAYER, eventParams);
		}

		public void OnRemoveFromGround(ItemObject itemObject)
		{
			Debug.Log("RemoveFromGround " + Name);
			itemObject.DestroySelf();
		}

		public void OnStack(InventoryController playerInventory, SlotPosition? slotPosition = null)
		{
			Debug.Log("Stack " + Name);
			playerInventory.StackItem(Copy(), slotPosition);
		}

		public void OnSplit(InventoryController playerInventory, SlotPosition slotPosition, int splitCount)
		{
			Debug.Log("Split " + Name);
			playerInventory.SplitItem(slotPosition, splitCount);
		}

		public void OnConsume(InventoryController playerInventory, AttributeController playerStats, SlotPosition slotPosition)
		{
			Debug.Log("Consume " + Name);
			playerInventory.ConsumeItem(slotPosition);
			ApplyBuff(playerStats);
		}
	}
}
