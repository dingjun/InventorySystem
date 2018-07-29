using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class PickupableItem : Item, IPickupable, IConsumable
	{
		public PickupableItem(string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable)
			: base(name, icon, buffTable)
		{

		}

		public override Item Copy()
		{
			return new PickupableItem(_name, _icon, _buffTable);
		}

		public override string ToString()
		{
			return "Name: " + Name + "\nType: Pickupable\nBuff: " + BuffTableToString();
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

		public void OnPutInAir(AirItemController _playerAirItem, SlotPosition slotPosition)
		{
			Debug.Log("PutInAir " + Name);
			_playerAirItem.AddItem(Copy(), slotPosition);
		}

		public void OnRemoveFromAir(AirItemController _playerAirItem)
		{
			Debug.Log("RemoveFromAir " + Name);
			_playerAirItem.RemoveItem();
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

		public void OnConsume(InventoryController playerInventory, AttributeController playerStats, SlotPosition slotPosition)
		{
			Debug.Log("Consume " + Name);
			playerInventory.ConsumeItem(slotPosition);
			ApplyBuff(playerStats);
		}
	}
}
