using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InventoryRowEntry : MonoBehaviour
	{
		public ItemSlot[] Slots;

		public void SetPosition(int rowIndex)
		{
			for (int i = 0; i < Slots.Length; ++i)
			{
				Slots[i].SetPosition(new SlotPosition(rowIndex, i));
			}
		}

		public void UpdateRow(InventoryRow row)
		{
			Debug.Assert(Slots.Length == InventoryRow.NUMBER_SLOTS);
			for (int i = 0; i < Slots.Length; ++i)
			{
				Slots[i].UpdateSlot(row.Items[i]);
			}
		}
	}
}
