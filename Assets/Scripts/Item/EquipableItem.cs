using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class EquipableItem : Item, IPickupable, IEquipable
	{
		private Equipment.EquipmentType _equipmentType;

		public Equipment.EquipmentType EquipmentType
		{
			get
			{
				return _equipmentType;
			}
		}

		public override Item Copy()
		{
			return new EquipableItem(_name, _icon, _buffTable, _equipmentType);
		}

		public override string ToString()
		{
			return "Name: " + Name + "\nType: Equipable-" + _equipmentType.ToString() + "\nBuff: " + BuffTableToString();
		}

		public EquipableItem(string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable, Equipment.EquipmentType equipmentType)
			: base(name, icon, buffTable)
		{
			_equipmentType = equipmentType;
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

		public void OnEquip(EquipmentController playerEquipment, AttributeController playerStats)
		{
			Debug.Log("Equip " + Name);
			playerEquipment.AddItem(Copy());
			ApplyBuff(playerStats);
		}

		public void OnUnequip(EquipmentController playerEquipment, AttributeController playerStats)
		{
			Debug.Log("Unequip " + Name);
			playerEquipment.RemoveItem(_equipmentType);
			UnapplyBuff(playerStats);
		}
	}
}
