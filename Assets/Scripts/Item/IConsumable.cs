using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IConsumable
	{
		void OnConsume(InventoryController playerInventory, AttributeController playerStats, SlotPosition slotPosition);
	}
}
