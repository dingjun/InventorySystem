using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class PickupOptionController : MonoBehaviour
	{
		public enum PickupOption { Option1 = 1, Option2, Option3, Option4, Option5, Option6, Option7 }

		private PickupOption _option;

		public CircleCollider2D OverlayCircleCollider;

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
				case PickupOption.Option5:
					{
						_option = value;
						break;
					}
				default:
					{
						break;
					}
				}

				OverlayCircleCollider.enabled = (_option == PickupOption.Option4 || _option == PickupOption.Option5);
			}
		}
		
		private void OnEnable()
		{
			EventManager.StartListening(EventName.SET_PICKUP_OPTION, SetPickupOption);
			Option = PickupOption.Option1;
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.SET_PICKUP_OPTION, SetPickupOption);
		}

		private void SetPickupOption(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is PickupOption);
			Option = (PickupOption)eventParams[0];
		}
	}
}
