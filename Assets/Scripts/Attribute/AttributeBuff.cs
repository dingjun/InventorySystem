using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class AttributeBuff
	{
		public enum BuffTarget { Current, Maximum };
		public enum BuffType { Amount, Percentage };

		private const float PERMANENT_SECONDS = 0f;

		private readonly BuffTarget _target;
		private readonly BuffType _type;
		private readonly int _value;
		private readonly float _seconds;

		public BuffTarget Target
		{
			get
			{
				return _target;
			}
		}

		public BuffType Type
		{
			get
			{
				return _type;
			}
		}

		public int Value
		{
			get
			{
				return _value;
			}
		}

		public float Seconds
		{
			get
			{
				return _seconds;
			}
		}

		public bool IsPermanent
		{
			get
			{
				return _seconds == PERMANENT_SECONDS;
			}
		}

		public AttributeBuff(BuffTarget target, BuffType type, int value, float seconds = PERMANENT_SECONDS)
		{
			_target = target;
			_type = type;
			_value = value;
			_seconds = seconds;
		}

		public override string ToString()
		{
			string text = _value.ToString("+#;-#");
			if (_type == BuffType.Percentage)
			{
				text += "%";
			}
			if (_target == BuffTarget.Maximum)
			{
				text += "(Max)";
			}
			if (IsPermanent == false)
			{
				text += " for " + _seconds.ToString("0.00") + "s";
			}
			return text;
		}

		public int CalculateNewAttributeValue(int attributeValue)
		{
			if (Type == BuffType.Amount)
			{
				return attributeValue + _value;
			}
			else if (Type == BuffType.Percentage)
			{
				return (int)(attributeValue * (1 + _value / 100f));
			}
			Debug.Assert(false);
			return 0;
		}
	}
}
