using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class EquipmentScreen : MonoBehaviour
	{
		private EquipmentController _playerEquipment;
		private List<EquipmentSlot> _slots;

		public GameObject EquipmentSlotPrefab;
		public Transform EquipmentSlotParent;

		private void Awake()
		{
			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerEquipment = player.GetComponent<EquipmentController>();

			_slots = new List<EquipmentSlot>();
			for (int i = 0; i < _playerEquipment.EquipmentTable.Count; ++i)
			{
				GameObject slot = Instantiate(EquipmentSlotPrefab, EquipmentSlotParent);
				_slots.Add(slot.GetComponent<EquipmentSlot>());
				_slots[i].SetPosition(i);
			}
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.UPDATE_EQUIPMENT, UpdateEquipment);
			UpdateEquipment(null);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.UPDATE_EQUIPMENT, UpdateEquipment);
		}

		private void UpdateEquipment(object[] eventParams)
		{
			for (int i = 0; i < _playerEquipment.EquipmentTable.Count; ++i)
			{
				_slots[i].UpdateSlot(_playerEquipment.EquipmentTable[(Equipment.EquipmentType)i]);
			}
		}
	}
}
