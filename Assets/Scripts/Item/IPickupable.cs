using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IPickupable
	{
		void OnPickup(InventoryController playerInventory);

		void OnDrop(InventoryController playerInventor, int rowIndex, int slotIndex);
	}
}
