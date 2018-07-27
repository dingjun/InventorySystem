using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	public class EquipmentSlot : MonoBehaviour
	{
		public const int EQUIPMENT_SLOT_ROW_INDEX = -1;

		public Text Type;
		public ItemSlot Slot;

		public bool IsEmpty
		{
			get
			{
				return Slot.IsEmpty;
			}
		}

		public void SetInformation(PlayerController playerController, int slotIndex)
		{
			Slot.SetInformation(playerController, new ItemPosition(EQUIPMENT_SLOT_ROW_INDEX, slotIndex));
		}

		public void UpdateSlot(Equipment equipment)
		{
			Type.text = equipment.Type;
			Slot.UpdateSlot(equipment.Item);
		}
	}
}
