using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class PickupOptionController : MonoBehaviour
	{
		public enum PickupOption { Option1 = 1, Option2, Option3, Option4, Option5, Option6, Option7 }

		private PickupOption _option;

		public PickupOption Option
		{
			get
			{
				return _option;
			}
			set
			{
				switch (value)
				{
				case PickupOption.Option1:
				case PickupOption.Option2:
					_option = value;
					return;
				default:
					return;
				}
			}
		}

		// Use this for initialization
		void Start()
		{
			Option = PickupOption.Option1;
		}
	}
}
