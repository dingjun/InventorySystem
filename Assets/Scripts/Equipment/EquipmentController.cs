using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class EquipmentController : MonoBehaviour
	{
		private Dictionary<Equipment.EquipmentType, Equipment> _equipmentTable;
		private InventoryController _playerInventory;

		public Dictionary<Equipment.EquipmentType, Equipment> EquipmentTable
		{
			get
			{
				return _equipmentTable;
			}
		}

		private void Awake()
		{
			_equipmentTable = new Dictionary<Equipment.EquipmentType, Equipment>();
			foreach (Equipment.EquipmentType type in Enum.GetValues(typeof(Equipment.EquipmentType)))
			{
				_equipmentTable.Add(type, new Equipment(type));
			}
			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerInventory = player.GetComponent<InventoryController>();
		}

		public override string ToString()
		{
			string text = "==== EQUIPMENT ====\n";
			foreach (var equipment in _equipmentTable.Values)
			{
				text += equipment.ToString() + "\n";
			}
			return text;
		}

		public void AddItem(Item item)
		{
			Debug.Assert(item is IEquipable);

			Equipment.EquipmentType type = ((IEquipable)item).EquipmentType;

			// put original equipment back to inventory
			if (_equipmentTable[type].IsEmpty == false)
			{
				_playerInventory.AddItem(_equipmentTable[type].Item);
			}

			// equip new item
			_equipmentTable[type].AddItem(item);

			EventManager.TriggerEvent(EventName.UPDATE_EQUIPMENT);

			Debug.Log(this.ToString());
		}

		public void RemoveItem(Equipment.EquipmentType type)
		{
			Debug.Assert(_equipmentTable[type].IsEmpty == false);

			_equipmentTable[type].RemoveItem();

			EventManager.TriggerEvent(EventName.UPDATE_EQUIPMENT);

			Debug.Log(this.ToString());
		}
	}
}
