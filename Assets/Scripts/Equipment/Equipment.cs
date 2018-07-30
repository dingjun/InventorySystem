using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class Equipment
	{
		public enum EquipmentType { Weapon, Shield, Armor, Ring };

		private readonly EquipmentType _type;
		private Item _item;

		public string Type
		{
			get
			{
				return _type.ToString();
			}
		}

		public Item Item
		{
			get
			{
				return _item;
			}
		}

		public bool IsEmpty
		{
			get
			{
				return _item == null;
			}
		}

		public override string ToString()
		{
			return Type + ": " + ((_item == null) ? "____" : _item.Name);
		}

		public Equipment(EquipmentType type)
		{
			_type = type;
			_item = null;
		}

		public void DecreaseDurability(float durabilityDiff)
		{
			if (IsEmpty == false)
			{
				((IEquipable)_item).DecreaseDurability(durabilityDiff);
			}
		}

		public void AddItem(Item item)
		{
			Debug.Assert(item is IEquipable);
			_item = item;
		}

		public void RemoveItem()
		{
			_item = null;
		}
	}
}
