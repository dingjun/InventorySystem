using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class Attribute
	{
		public enum AttributeType { Health, Mana, Strength, Dexterity, Agility, Intelligence };

		private const int NOT_SPENDABLE_MAX_VALUE = Int32.MaxValue;

		private readonly AttributeType _type;
		private int _currentValue;
		private int _newCurrentValue;
		private int _maxValue;
		private int _newMaxValue;
		private List<AttributeBuff> _buffs;

		public string Name
		{
			get
			{
				return _type.ToString();
			}
		}

		public int CurrentValue
		{
			get
			{
				return _currentValue;
			}
		}
		
		public int NewCurrentValue
		{
			get
			{
				return _newCurrentValue;
			}
		}

		public int MaxValue
		{
			get
			{
				return _maxValue;
			}
		}

		public int NewMaxValue
		{
			get
			{
				return _newMaxValue;
			}
		}

		public bool IsSpendable
		{
			get
			{
				return _maxValue != NOT_SPENDABLE_MAX_VALUE;
			}
		}

		public int ValueOffset
		{
			get
			{
				if (IsSpendable)
				{
					return _newMaxValue - _maxValue;
				}
				else
				{
					return _newCurrentValue - _currentValue;
				}
			}
		}
		
		public Attribute(AttributeType type, int currentValue, int maxValue = NOT_SPENDABLE_MAX_VALUE)
		{
			_type = type;
			_currentValue = currentValue;
			_newCurrentValue = currentValue;
			_maxValue = maxValue;
			_newMaxValue = maxValue;
			_buffs = new List<AttributeBuff>();
		}

		public void AddBuff(AttributeBuff buff)
		{
			_buffs.Add(buff);
			UpdateNewValue();
		}

		public void RemoveBuff(AttributeBuff buff)
		{
			_buffs.Remove(buff);
			UpdateNewValue();
		}

		private void UpdateNewValue()
		{
			_newCurrentValue = _currentValue;
			_newMaxValue = _maxValue;

			foreach (var buff in _buffs)
			{
				if (buff.Target == AttributeBuff.BuffTarget.Current)
				{
					_newCurrentValue = buff.CalculateNewAttributeValue(_newCurrentValue);
				}
				else    // buff.Target == AttributeBuff.BuffTarget.Maximum
				{
					Debug.Assert(IsSpendable);
					_newMaxValue = buff.CalculateNewAttributeValue(_newMaxValue);
				}
				
			}

			Mathf.Clamp(_newCurrentValue, 0, _newMaxValue);
		}
	}
}