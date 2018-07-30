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

		float CurDurability
		{
			get;
			set;
		}

		float MaxDurability
		{
			get;
			set;
		}

		bool IsDurable
		{
			get;
		}

		void DecreaseDurability(float durabilityDiff);

		void OnEquip(EquipmentController playerEquipment, AttributeController playerStats);

		void OnUnequip(EquipmentController playerEquipment, AttributeController playerStats);
	}
}
