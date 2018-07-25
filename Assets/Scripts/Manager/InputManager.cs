using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InputManager : MonoBehaviour
	{
		private PlayerController _playerController;
		private PickupOptionController _pickupOptionController;

		// Use this for initialization
		void Start()
		{
			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerController = player.GetComponent<PlayerController>();
			_pickupOptionController = player.GetComponent<PickupOptionController>();
		}
		
		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				_pickupOptionController.Option = PickupOptionController.PickupOption.Option1;
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				_pickupOptionController.Option = PickupOptionController.PickupOption.Option2;
			}
			if (Input.GetKeyDown(KeyCode.Alpha3))
			{
				_pickupOptionController.Option = PickupOptionController.PickupOption.Option3;
			}
			if (Input.GetKeyDown(KeyCode.Alpha4))
			{
				_pickupOptionController.Option = PickupOptionController.PickupOption.Option4;
			}
			if (Input.GetKeyDown(KeyCode.Alpha5))
			{
				_pickupOptionController.Option = PickupOptionController.PickupOption.Option5;
			}
			if (Input.GetKeyDown(KeyCode.Alpha6))
			{
				_pickupOptionController.Option = PickupOptionController.PickupOption.Option6;
			}
			if (Input.GetKeyDown(KeyCode.Alpha7))
			{
				_pickupOptionController.Option = PickupOptionController.PickupOption.Option7;
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
					_playerController.Interact(hitCollider.transform);
				}
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
