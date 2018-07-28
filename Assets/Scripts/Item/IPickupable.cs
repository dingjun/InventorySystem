using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IPickupable
	{
		void OnPutInInventory(InventoryController playerInventory, SlotPosition? slotPosition = null);

		void OnRemoveFromInventory(InventoryController playerInventory, SlotPosition slotPosition);

		void OnPutInAir(AirItemController _playerAirItem, SlotPosition slotPosition);

		void OnRemoveFromAir(AirItemController _playerAirItem);

		void OnPutOnGround();

		void OnRemoveFromGround();
	}
}
