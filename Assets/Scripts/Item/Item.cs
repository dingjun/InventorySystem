using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public abstract class Item : MonoBehaviour
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

		protected void ApplyBuff(AttributeController _playerStats)
		{
			Debug.Log("ApplyBuff " + Name);
			_playerStats.ApplyBuff(_buffTable);
		}

		protected void UnapplyBuff(AttributeController _playerStats)
		{
			Debug.Log("UnapplyBuff " + Name);
			_playerStats.UnapplyBuff(_buffTable);
		}
	}
}
