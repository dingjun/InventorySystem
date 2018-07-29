using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IStackable
	{
		int StackLimit
		{
			get;
		}

		int Count
		{
			get;
			set;
		}

		bool IsNoStackLimit
		{
			get;
		}

		void OnStack(InventoryController playerInventory, SlotPosition? slotPosition = null);

		void OnSplit(InventoryController playerInventory, SlotPosition slotPosition, int splitCount);
	}
}
