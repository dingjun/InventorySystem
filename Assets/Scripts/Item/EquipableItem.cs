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

		public void OnEquip(EquipmentController playerEquipment, AttributeController playerStats)
		{
			Debug.Log("Equip " + Name);
			playerEquipment.AddItem((Item)MemberwiseClone());
			ApplyBuff(playerStats);
		}

		public void OnUnequip(EquipmentController playerEquipment, AttributeController playerStats)
		{
			Debug.Log("Unequip " + Name);
			playerEquipment.RemoveItem(_equipmentType);
			UnapplyBuff(playerStats);
		}

		public static EquipableItem Attach(GameObject itemObject, string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable, Equipment.EquipmentType type)
		{
			EquipableItem item = itemObject.AddComponent<EquipableItem>();
			item._name = name;
			item._icon = icon;
			item._buffTable = buffTable;
			item._equipmentType = type;
			return item;
		}
	}
}
