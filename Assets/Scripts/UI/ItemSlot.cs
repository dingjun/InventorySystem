using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class ItemSlot : MonoBehaviour
	{
		public ItemIcon Icon;

		public bool IsEmpty
		{
			get
			{
				return Icon.Item == null;
			}
		}

		public void SetInformation(InventoryController playerInventory, int rowIndex, int slotIndex)
		{
			Icon.SetInformation(playerInventory, rowIndex, slotIndex);
		}

		public void UpdateSlot(Item item)
		{
			Icon.Item = item;
		}
	}
}
