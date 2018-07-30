using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class EquipableItem : Item, IPickupable, IEquipable
	{
		private Equipment.EquipmentType _equipmentType;
		private float _curDurability;
		private float _maxDurability;

		public Equipment.EquipmentType EquipmentType
		{
			get
			{
				return _equipmentType;
			}
		}

		public float CurDurability
		{
			get
			{
				return _curDurability;
			}
			set
			{
				_curDurability = value;
			}
		}

		public float MaxDurability
		{
			get
			{
				return _maxDurability;
			}
			set
			{
				_maxDurability = value;
			}
		}

		public bool IsDurable
		{
			get
			{
				return _curDurability > 0f;
			}
		}

		public override Item Copy()
		{
			return new EquipableItem(_name, _icon, _buffTable, _equipmentType, _curDurability, _maxDurability);
		}

		public override string ToString()
		{
			return "Name: " + Name + "\nType: Equipable-" + _equipmentType.ToString() + "(" + _curDurability.ToString("0.00") + "/" + _maxDurability.ToString() + ")" + "\nBuff: " + BuffTableToString();
		}

		public EquipableItem(string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable, Equipment.EquipmentType equipmentType, float curDurability, float maxDurability)
			: base(name, icon, buffTable)
		{
			_equipmentType = equipmentType;
			_curDurability = curDurability;
			_maxDurability = maxDurability;
		}

		public void DecreaseDurability(float durabilityDiff)
		{
			_curDurability -= durabilityDiff;
			if (_curDurability <= 0f)
			{
				_curDurability = 0f;
				object[] eventParams = { (object)this };
				EventManager.TriggerEvent(EventName.UNEQUIP_ITEM_NO_DURABILITY, eventParams);
			}
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
