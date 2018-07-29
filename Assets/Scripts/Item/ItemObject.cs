using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class ItemObject : MonoBehaviour
	{
		private Item _item;

		public Item Item
		{
			get
			{
				return _item;
			}
			set
			{
				_item = value;
				GetComponent<SpriteRenderer>().sprite = _item.Icon;
			}
		}

		public void DestroySelf()
		{
			Destroy(gameObject);
		}
	}
}
