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

		void OnStack(InventoryController _playerInventory);

		void OnSplit(InventoryController _playerInventory);
	}
}
