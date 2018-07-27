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

		public void OnPutInInventory(InventoryController playerInventory)
		{
			Debug.Log("PutInInventory " + Name);
			playerInventory.AddItem((Item)MemberwiseClone());
		}

		public void OnRemoveFromInventory(InventoryController playerInventory, ItemPosition itemPosition)
		{
			Debug.Log("RemoveFromInventory " + Name);
			playerInventory.RemoveItem(itemPosition);
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
