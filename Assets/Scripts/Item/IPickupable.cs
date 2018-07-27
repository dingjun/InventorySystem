using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IPickupable
	{
		void OnPutInInventory(InventoryController playerInventory);

		void OnRemoveFromInventory(InventoryController playerInventory, ItemPosition itemPosition);

		void OnPutInAir(PlayerController playerController);

		void OnRemoveFromAir(PlayerController playerController);

		void OnPutOnGround();

		void OnRemoveFromGround();
	}
}
