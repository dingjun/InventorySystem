using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class EquipmentScreen : MonoBehaviour
	{
		private EquipmentController _playerEquipment;
		private PlayerController _playerController;
		private List<EquipmentSlot> _slots;

		public GameObject EquipmentSlotPrefab;
		public Transform EquipmentSlotParent;

		private void Awake()
		{
			_slots = new List<EquipmentSlot>();

			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerController = player.GetComponent<PlayerController>();
			_playerEquipment = player.GetComponent<EquipmentController>();

			for (int i = 0; i < _playerEquipment.EquipmentTable.Count; ++i)
			{
				GameObject slot = Instantiate(EquipmentSlotPrefab, EquipmentSlotParent);
				_slots.Add(slot.GetComponent<EquipmentSlot>());
				_slots[i].SetInformation(_playerController, i);
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
