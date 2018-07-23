using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InputManager : MonoBehaviour
	{
		// Update is called once per frame
		void Update()
		{
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
		}

		public void ToggleInventoryScreen()
		{
			EventManager.TriggerEvent(EventName.TOGGLE_INVENTORY_SCREEN);
		}

		public void ToggleEquipmentScreen()
		{
			EventManager.TriggerEvent(EventName.TOGGLE_EQUIPMENT_SCREEN);
		}

		public void ToggleAttributeScreen()
		{
			EventManager.TriggerEvent(EventName.TOGGLE_ATTRIBUTE_SCREEN);
		}

		public void SpawnNewItems()
		{
			EventManager.TriggerEvent(EventName.SPAWN_NEW_ITEMS);
		}
	}
}