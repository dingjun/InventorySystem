using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InventoryRow
	{
		public const int NUMBER_SLOTS = 4;

		private Item[] _items = new Item[NUMBER_SLOTS];

		public Item[] Items
		{
			get
			{
				return _items;
			}
		}

		public bool IsEmpty
		{
			get
			{
				foreach (var item in _items)
				{
					if (item != null)
					{
						return false;
					}
				}
				return true;
			}
		}

		public bool IsFull
		{
			get
			{
				foreach (var item in _items)
				{
					if (item == null)
					{
						return false;
					}
				}
				return true;
			}
		}

		public override string ToString()
		{
			string text = "";
			foreach (var item in _items)
			{
				if (item == null)
				{
					text += "| ____ |";
				}
				else
				{
					text += "| " + item.Name + " |";
				}
			}
			return text;
		}

		public void AddItem(Item item, int? index = null)
		{
			Debug.Assert(item is IPickupable);

			if (index == null)
			{
				Debug.Assert(IsFull == false);
				for (int i = 0; i < NUMBER_SLOTS; ++i)
				{
					if (_items[i] == null)
					{
						_items[i] = item;
						return;
					}
				}
			}
			else
			{
				_items[index.GetValueOrDefault()] = item;
			}
		}

		public void RemoveItem(int index)
		{
			Debug.Assert(_items[index] != null);
			_items[index] = null;
		}
	}
}
