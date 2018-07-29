using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public abstract class Item
	{
		protected string _name;
		protected Sprite _icon;
		protected Dictionary<Attribute.AttributeType, AttributeBuff> _buffTable;

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public Sprite Icon
		{
			get
			{
				return _icon;
			}
		}

		public Dictionary<Attribute.AttributeType, AttributeBuff> BuffTable
		{
			get
			{
				return _buffTable;
			}
		}
		
		public Item(string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable)
		{
			_name = name;
			_icon = icon;
			_buffTable = buffTable;
		}

		public abstract Item Copy();

		public abstract override string ToString();

		protected string BuffTableToString()
		{
			string text = "";
			foreach (var buffPair in _buffTable)
			{
				text += "\n  " + buffPair.Key.ToString() + " " + buffPair.Value.ToString();
			}
			return text;
		}

		protected void ApplyBuff(AttributeController playerStats)
		{
			Debug.Log("ApplyBuff " + Name);
			playerStats.ApplyBuff(_buffTable);
		}

		protected void UnapplyBuff(AttributeController playerStats)
		{
			Debug.Log("UnapplyBuff " + Name);
			playerStats.UnapplyBuff(_buffTable);
		}
	}
}
