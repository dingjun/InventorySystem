using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class UIManager : MonoBehaviour
	{
		public GameObject InventoryScreen;
		public GameObject EquipmentScreen;
		public GameObject AttributeScreen;
		public GameObject InventoryButton;
		public GameObject EquipmentButton;
		public GameObject AttributeButton;

		private void OnEnable()
		{
			EventManager.StartListening(EventName.TOGGLE_INVENTORY_SCREEN, ToggleInventoryScreen);
			EventManager.StartListening(EventName.TOGGLE_EQUIPMENT_SCREEN, ToggleEquipmentScreen);
			EventManager.StartListening(EventName.TOGGLE_ATTRIBUTE_SCREEN, ToggleAttributeScreen);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.TOGGLE_INVENTORY_SCREEN, ToggleInventoryScreen);
			EventManager.StopListening(EventName.TOGGLE_EQUIPMENT_SCREEN, ToggleEquipmentScreen);
			EventManager.StopListening(EventName.TOGGLE_ATTRIBUTE_SCREEN, ToggleAttributeScreen);
		}

		private void ToggleInventoryScreen()
		{
			InventoryScreen.SetActive(!InventoryScreen.activeSelf);
			InventoryButton.SetActive(!InventoryButton.activeSelf);
		}

		private void ToggleEquipmentScreen()
		{
			EquipmentScreen.SetActive(!EquipmentScreen.activeSelf);
			EquipmentButton.SetActive(!EquipmentButton.activeSelf);
		}

		private void ToggleAttributeScreen()
		{
			AttributeScreen.SetActive(!AttributeScreen.activeSelf);
			AttributeButton.SetActive(!AttributeButton.activeSelf);
		}
	}
}
