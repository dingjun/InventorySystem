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
		}

		bool IsNoStackLimit
		{
			get;
		}

		void OnStack();

		void OnSplit();
	}
}
