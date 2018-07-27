using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IPickupable
	{
		void OnPutInInventory(InventoryController playerInventory);

		void OnRemoveFromInventory(InventoryController playerInventory, int rowIndex, int slotIndex);

		void OnPutOnGround();

		void OnRemoveFromGround();
	}
}
