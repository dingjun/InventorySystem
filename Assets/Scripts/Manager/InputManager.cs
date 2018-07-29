using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InputManager : MonoBehaviour
	{
		public const KeyCode LEFT_CLICK_DROP_HOTKEY = KeyCode.LeftShift;

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SetPickupOption(PickupOptionController.PickupOption.Option1);
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				SetPickupOption(PickupOptionController.PickupOption.Option2);
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				SetPickupOption(PickupOptionController.PickupOption.Option3);
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				SetPickupOption(PickupOptionController.PickupOption.Option4);
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				SetPickupOption(PickupOptionController.PickupOption.Option5);
			}
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				SetPickupOption(PickupOptionController.PickupOption.Option6);
			}
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				SetPickupOption(PickupOptionController.PickupOption.Option7);
			}

			if (Input.GetKeyDown(KeyCode.I))
			{
				ToggleInventoryScreen();
			}
			if (Input.GetKeyDown(KeyCode.E))
			{
				ToggleEquipmentScreen();
			}
			if (Input.GetKeyDown(KeyCode.C))
			{
				ToggleAttributeScreen();
			}

			if (Input.GetKeyDown(KeyCode.Space))
			{
				SpawnNewItems();
			}

			if (Input.GetMouseButtonDown(0))
			{
				Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Collider2D hitCollider = Physics2D.OverlapPoint(mouseWorldPosition, LayerMask.GetMask(Constant.LAYER_ITEM));
				if (hitCollider)
				{
					object[] eventParams = { (object)(hitCollider.transform) };
					EventManager.TriggerEvent(EventName.CLICK_ITEM_OBJECT, eventParams);
				}
			}
		}

		private void SetPickupOption(PickupOptionController.PickupOption option)
		{
			object[] eventParams = { (object)option };
			EventManager.TriggerEvent(EventName.SET_PICKUP_OPTION, eventParams);
		}

		private void ToggleInventoryScreen()
		{
			EventManager.TriggerEvent(EventName.TOGGLE_INVENTORY_SCREEN);
		}

		private void ToggleEquipmentScreen()
		{
			EventManager.TriggerEvent(EventName.TOGGLE_EQUIPMENT_SCREEN);
		}

		private void ToggleAttributeScreen()
		{
			EventManager.TriggerEvent(EventName.TOGGLE_ATTRIBUTE_SCREEN);
		}

		private void SpawnNewItems()
		{
			EventManager.TriggerEvent(EventName.SPAWN_NEW_ITEMS);
		}
	}
}
