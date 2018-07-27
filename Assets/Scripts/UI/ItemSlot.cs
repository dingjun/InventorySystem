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

		public void SetPosition(ItemPosition itemPosition)
		{
			Icon.SetPosition(itemPosition);
		}

		public void UpdateSlot(Item item)
		{
			Icon.Item = item;
		}
	}
}
