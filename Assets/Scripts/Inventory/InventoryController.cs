using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InventoryController : MonoBehaviour
	{
		private List<Item> _items;

		public List<Item> Items
		{
			get
			{
				return _items;
			}
		}

		// Use this for initialization
		void Start()
		{
			_items = new List<Item>();
		}

		private void PrintAll()
		{
			string message = "";
			for (int i = 0; i < _items.Count; ++i)
			{
				message += i.ToString() + ". " + _items[i].Name + "\n";
			}
			Debug.Log(message);
		}

		public void AddItem(Item item)
		{
			Debug.Assert(item is IPickupable);
			_items.Add(item);
			PrintAll();
		}

		public void RemoveItem(Item item)
		{
			_items.Remove(item);
			PrintAll();
		}
	}
}
