using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IEquipable
	{
		Equipment.EquipmentType EquipmentType
		{
			get;
		}

		void OnEquip(EquipmentController playerEquipment, AttributeController playerStats);

		void OnUnequip(EquipmentController playerEquipment, AttributeController playerStats);
	}
}
