using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	public class AttributeEntry : MonoBehaviour
	{
		public Text Type;
		public Text Value;
		public Text Buff;

		public void SetText(Attribute attribute)
		{
			Type.text = attribute.Type;

			if (attribute.IsSpendable)
			{
				Value.text = attribute.NewCurrentValue.ToString() + "/" + attribute.NewMaxValue.ToString();
			}
			else
			{
				Value.text = attribute.NewCurrentValue.ToString();
			}

			Buff.text = attribute.ValueOffset.ToString("+#;-#;");
		}
	}
}
