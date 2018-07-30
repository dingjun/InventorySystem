using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class EquipmentController : MonoBehaviour
	{
		private Dictionary<Equipment.EquipmentType, Equipment> _equipmentTable;

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
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.PLAYER_WALK, DecreaseDurability);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.PLAYER_WALK, DecreaseDurability);
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

		private void DecreaseDurability(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is float);
			float durabilityDiff = (float)eventParams[0];
			foreach (var equipment in _equipmentTable.Values)
			{
				equipment.DecreaseDurability(durabilityDiff);
			}
		}

		public void AddItem(Item item)
		{
			Debug.Assert(item is IEquipable);

			Equipment.EquipmentType type = ((IEquipable)item).EquipmentType;
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
