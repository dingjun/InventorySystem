using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class ItemObjectHighlight : MonoBehaviour
	{
		public GameObject Highlight;

		private void OnMouseEnter()
		{
			Highlight.SetActive(true);
		}

		private void OnMouseExit()
		{
			Highlight.SetActive(false);
		}
	}
}
