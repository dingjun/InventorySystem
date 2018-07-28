using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class PlayerController : MonoBehaviour
	{
		private const float SPEED = 3f;
		private const float PROXIMITY_SQUARE = 9f;

		private Rigidbody2D _rigidBody;
		private Animator _animator;
		private PickupOptionController _pickupOption;
		private InventoryController _inventory;
		private EquipmentController _equipment;
		private AttributeController _stats;
		private AirItemController _airItem;

		// Use this for initialization
		void Start()
		{
			_rigidBody = GetComponent<Rigidbody2D>();
			_animator = GetComponent<Animator>();
			_animator.SetFloat("Angle", 0f);

			_pickupOption = GetComponent<PickupOptionController>();
			_inventory = GetComponent<InventoryController>();
			_equipment = GetComponent<EquipmentController>();
			_stats = GetComponent<AttributeController>();
			_airItem = GetComponent<AirItemController>();
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.CLICK_ITEM_OBJECT, ClickItemObject);
			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_SLOT, LeftClickItemSlot);
			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_ICON, LeftClickItemIcon);
			EventManager.StartListening(EventName.RIGHT_CLICK_ITEM_ICON, RightClickItemIcon);
			EventManager.StartListening(EventName.MIDDLE_CLICK_ITEM_ICON, MiddleClickItemIcon);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.CLICK_ITEM_OBJECT, ClickItemObject);
			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_SLOT, LeftClickItemSlot);
			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_ICON, LeftClickItemIcon);
			EventManager.StopListening(EventName.RIGHT_CLICK_ITEM_ICON, RightClickItemIcon);
			EventManager.StopListening(EventName.MIDDLE_CLICK_ITEM_ICON, MiddleClickItemIcon);
		}

		private void FixedUpdate()
		{
			// velocity
			Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (input.magnitude > 1f)
			{
				input = input.normalized;
			}
			_rigidBody.velocity = input * SPEED;

			// animation
			_animator.speed = input.magnitude;
			if (_animator.speed > 0f)
			{
				float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
				_animator.SetFloat("Angle", angle);
			}
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (_pickupOption.Option != PickupOptionController.PickupOption.Option2)
			{
				return;
			}

			Item item = other.GetComponent<Item>();
			if (item != null)
			{
				InteractWithItemObject(item);
			}
		}

		private void InteractWithItemObject(Item item)
		{
			IUsable usable = item as IUsable;
			IPickupable pickupable = item as IPickupable;
			IEquipable equipable = item as IEquipable;

			if (usable != null)
			{
				usable.OnUse(_stats);
			}
			else if (pickupable != null)
			{
				if (equipable != null && _equipment.EquipmentTable[equipable.EquipmentType].IsEmpty)
				{
					equipable.OnEquip(_equipment, _stats);
				}
				else
				{
					pickupable.OnPutInInventory(_inventory);
				}
				pickupable.OnRemoveFromGround();
			}
		}

		private bool PutAirItemToSlot(SlotPosition slotPosition)
		{
			return false;
		}

		private void ClickItemObject(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is Transform);
			Transform other = (Transform)eventParams[0];

			if (_pickupOption.Option != PickupOptionController.PickupOption.Option1)
			{
				return;
			}

			Vector3 displacement = other.position - transform.position;
			if (displacement.sqrMagnitude > PROXIMITY_SQUARE)
			{
				Debug.Log("Out of Proximity");
				return;
			}

			Item item = other.GetComponent<Item>();
			if (item != null)
			{
				InteractWithItemObject(item);
			}
		}

		private void LeftClickItemSlot(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is SlotPosition);
			SlotPosition slotPosition = (SlotPosition)eventParams[0];

			if (_airItem.IsEmpty)
			{
				return;
			}

			IPickupable pickupable = _airItem.Item as IPickupable;
			if (slotPosition.RowIndex == EquipmentSlot.EQUIPMENT_SLOT_ROW_INDEX)
			{
				IEquipable equipable = _airItem.Item as IEquipable;
				if (equipable == null || (Equipment.EquipmentType)slotPosition.SlotIndex != equipable.EquipmentType)
				{
					return;
				}
				pickupable.OnRemoveFromAir(_airItem);
				equipable.OnEquip(_equipment, _stats);
			}
			else
			{
				pickupable.OnRemoveFromAir(_airItem);
				pickupable.OnPutInInventory(_inventory, slotPosition);
			}
		}

		private void LeftClickItemIcon(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is ItemIcon);
			ItemIcon itemIcon = (ItemIcon)eventParams[0];
			SlotPosition slotPosition = itemIcon.Position;
			IPickupable pickupable = itemIcon.Item as IPickupable;
			IEquipable equipable = itemIcon.Item as IEquipable;

			if (_airItem.IsEmpty)
			{
				if (itemIcon.IsEquipmentIcon)
				{
					equipable.OnUnequip(_equipment, _stats);
				}
				else
				{
					pickupable.OnRemoveFromInventory(_inventory, slotPosition);
				}
				pickupable.OnPutInAir(_airItem, slotPosition);
			}
			else
			{
				IPickupable pickupableAir = _airItem.Item as IPickupable;
				if (slotPosition.RowIndex == EquipmentSlot.EQUIPMENT_SLOT_ROW_INDEX)
				{
					IEquipable equipableAir = _airItem.Item as IEquipable;
					if (equipableAir == null || (Equipment.EquipmentType)(slotPosition.SlotIndex) != equipableAir.EquipmentType)
					{
						return;
					}

					equipable.OnUnequip(_equipment, _stats);
					pickupableAir.OnRemoveFromAir(_airItem);
					equipableAir.OnEquip(_equipment, _stats);
					pickupable.OnPutInAir(_airItem, slotPosition);
				}
				else
				{
					pickupable.OnRemoveFromInventory(_inventory, slotPosition);
					pickupableAir.OnRemoveFromAir(_airItem);
					pickupableAir.OnPutInInventory(_inventory, slotPosition);
					pickupable.OnPutInAir(_airItem, slotPosition);
				}
			}
		}
		
		private void RightClickItemIcon(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is ItemIcon);
			ItemIcon itemIcon = (ItemIcon)eventParams[0];
			IPickupable pickupable = itemIcon.Item as IPickupable;
			IEquipable equipable = itemIcon.Item as IEquipable;

			if (equipable == null)
			{
				return;
			}

			if (itemIcon.IsEquipmentIcon)
			{
				equipable.OnUnequip(_equipment, _stats);
				pickupable.OnPutInInventory(_inventory);
			}
			else
			{
				Item equippedItem = _equipment.EquipmentTable[equipable.EquipmentType].Item;
				pickupable.OnRemoveFromInventory(_inventory, itemIcon.Position);
				if (equippedItem != null)
				{
					((IEquipable)equippedItem).OnUnequip(_equipment, _stats);
					((IPickupable)equippedItem).OnPutInInventory(_inventory, itemIcon.Position);
				}
				equipable.OnEquip(_equipment, _stats);
			}
		}

		private void MiddleClickItemIcon(object[] eventParams)
		{
			// TODO
		}
	}
}
