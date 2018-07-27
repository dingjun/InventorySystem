using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	public class Tooltip : MonoBehaviour
	{
		public Text TooltipText;

		public void UpdateTooltip(string text)
		{
			gameObject.SetActive(text != null);
			if (text != null)
			{
				GetComponent<MouseFollower>().Update();
				TooltipText.text = text;
			}
		}
	}
}
