using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	public class Tooltip : MonoBehaviour
	{
		public Text TooltipText;

		void Update()
		{
			transform.position = Input.mousePosition;
		}

		public void UpdateTooltip(Item item)
		{
			gameObject.SetActive(item != null);
			if (item != null)
			{
				TooltipText.text = item.ToString();
			}
		}
	}
}
