﻿using System.Collections;
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
		public SplitScreen SplitScreen;
		public Tooltip ItemTooltip;

		private void OnEnable()
		{
			EventManager.StartListening(EventName.TOGGLE_INVENTORY_SCREEN, ToggleInventoryScreen);
			EventManager.StartListening(EventName.TOGGLE_EQUIPMENT_SCREEN, ToggleEquipmentScreen);
			EventManager.StartListening(EventName.TOGGLE_ATTRIBUTE_SCREEN, ToggleAttributeScreen);
			EventManager.StartListening(EventName.OPEN_SPLIT_SCREEN, OpenSplitScreen);
			EventManager.StartListening(EventName.OPEN_TOOLTIP, OpenTooltip);
			EventManager.StartListening(EventName.CLOSE_TOOLTIP, CloseTooltip);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.TOGGLE_INVENTORY_SCREEN, ToggleInventoryScreen);
			EventManager.StopListening(EventName.TOGGLE_EQUIPMENT_SCREEN, ToggleEquipmentScreen);
			EventManager.StopListening(EventName.TOGGLE_ATTRIBUTE_SCREEN, ToggleAttributeScreen);
			EventManager.StopListening(EventName.OPEN_SPLIT_SCREEN, OpenSplitScreen);
			EventManager.StopListening(EventName.OPEN_TOOLTIP, OpenTooltip);
			EventManager.StopListening(EventName.CLOSE_TOOLTIP, CloseTooltip);
		}

		private void ToggleInventoryScreen(object[] eventParams)
		{
			InventoryScreen.SetActive(!InventoryScreen.activeSelf);
			InventoryButton.SetActive(!InventoryButton.activeSelf);
			CheckInventoryEquipmentScreens();
		}

		private void ToggleEquipmentScreen(object[] eventParams)
		{
			EquipmentScreen.SetActive(!EquipmentScreen.activeSelf);
			EquipmentButton.SetActive(!EquipmentButton.activeSelf);
			CheckInventoryEquipmentScreens();
		}

		private void ToggleAttributeScreen(object[] eventParams)
		{
			AttributeScreen.SetActive(!AttributeScreen.activeSelf);
			AttributeButton.SetActive(!AttributeButton.activeSelf);
		}

		private void OpenSplitScreen(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is ItemIcon);
			SplitScreen.Open((ItemIcon)eventParams[0]);
		}

		private void OpenTooltip(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is string);
			ItemTooltip.UpdateTooltip((string)eventParams[0]);
		}

		private void CloseTooltip(object[] eventParams)
		{
			ItemTooltip.UpdateTooltip(null);
		}

		private void CheckInventoryEquipmentScreens()
		{
			if (InventoryScreen.activeSelf == false && EquipmentScreen.activeSelf == false)
			{
				EventManager.TriggerEvent(EventName.RETURN_AIR_ITEM);
			}
		}
	}
}
