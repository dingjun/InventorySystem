using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class PlayerController : MonoBehaviour
	{
		private const float SPEED = 3f;
		private const float PROXIMITY_SQUARE = 16f;

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
			EventManager.StartListening(EventName.LEFT_CLICK_WORLD, LeftClickWorld);
			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_SLOT, LeftClickItemSlot);
			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_ICON, LeftClickItemIcon);
			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_ICON_DROP_HOTKEY, LeftClickItemIconDropHotkey);
			EventManager.StartListening(EventName.RIGHT_CLICK_ITEM_ICON, RightClickItemIcon);
			EventManager.StartListening(EventName.MIDDLE_CLICK_ITEM_ICON, MiddleClickItemIcon);
			EventManager.StartListening(EventName.RETURN_AIR_ITEM, ReturnAirItem);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.CLICK_ITEM_OBJECT, ClickItemObject);
			EventManager.StopListening(EventName.LEFT_CLICK_WORLD, LeftClickWorld);
			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_SLOT, LeftClickItemSlot);
			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_ICON, LeftClickItemIcon);
			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_ICON_DROP_HOTKEY, LeftClickItemIconDropHotkey);
			EventManager.StopListening(EventName.RIGHT_CLICK_ITEM_ICON, RightClickItemIcon);
			EventManager.StopListening(EventName.MIDDLE_CLICK_ITEM_ICON, MiddleClickItemIcon);
			EventManager.StopListening(EventName.RETURN_AIR_ITEM, ReturnAirItem);
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

			ItemObject itemObject = other.GetComponent<ItemObject>();
			if (itemObject != null)
			{
				InteractWithItemObject(itemObject);
			}
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

			ItemObject itemObject = other.GetComponent<ItemObject>();
			if (itemObject != null)
			{
				InteractWithItemObject(itemObject);
			}
		}

		private void LeftClickWorld(object[] eventParams)
		{
			if (_airItem.IsEmpty)
			{
				return;
			}

			IPickupable pickupableAir = _airItem.Item as IPickupable;
			pickupableAir.OnRemoveFromAir(_airItem);
			pickupableAir.OnPutOnGround(transform.position);
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
				if (equipable == null)
				{
					Debug.Log("Not equipable");
					return;
				}
				if ((Equipment.EquipmentType)(slotPosition.SlotIndex) != equipable.EquipmentType)
				{
					Debug.Log("Not the same equipment type");
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
					if (equipableAir == null)
					{
						Debug.Log("Not equipable");
						return;
					}
					if ((Equipment.EquipmentType)(slotPosition.SlotIndex) != equipableAir.EquipmentType)
					{
						Debug.Log("Not the same equipment type");
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

		private void LeftClickItemIconDropHotkey(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is ItemIcon);
			ItemIcon itemIcon = (ItemIcon)eventParams[0];
			SlotPosition slotPosition = itemIcon.Position;
			IPickupable pickupable = itemIcon.Item as IPickupable;
			IEquipable equipable = itemIcon.Item as IEquipable;

			if (slotPosition.RowIndex == EquipmentSlot.EQUIPMENT_SLOT_ROW_INDEX)
			{
				equipable.OnUnequip(_equipment, _stats);
			}
			else
			{
				pickupable.OnRemoveFromInventory(_inventory, itemIcon.Position);
			}
			pickupable.OnPutOnGround(transform.position);
		}

		private void RightClickItemIcon(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is ItemIcon);

			if (_airItem.IsEmpty == false)
			{
				Debug.Log("Air Item is not null");
				return;
			}

			ItemIcon itemIcon = (ItemIcon)eventParams[0];
			IPickupable pickupable = itemIcon.Item as IPickupable;
			IEquipable equipable = itemIcon.Item as IEquipable;

			if (equipable == null)
			{
				Debug.Log("Not equipable");
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

		private void ReturnAirItem(object[] eventParams)
		{
			if (_airItem.IsEmpty)
			{
				return;
			}

			SlotPosition originalPosition = _airItem.OriginalPosition;
			IPickupable pickupableAir = _airItem.Item as IPickupable;
			IEquipable equipableAir = _airItem.Item as IEquipable;

			pickupableAir.OnRemoveFromAir(_airItem);

			if (originalPosition.RowIndex == EquipmentSlot.EQUIPMENT_SLOT_ROW_INDEX)
			{
				Equipment.EquipmentType equipmentType = (Equipment.EquipmentType)(originalPosition.SlotIndex);

				if (_equipment.EquipmentTable[equipmentType].IsEmpty)
				{
					equipableAir.OnEquip(_equipment, _stats);
				}
				else
				{
					pickupableAir.OnPutInInventory(_inventory);
				}
			}
			else
			{
				if (_inventory.IsItemEmpty(originalPosition))
				{
					Debug.Log("test");
					pickupableAir.OnPutInInventory(_inventory, originalPosition);
				}
				else
				{
					pickupableAir.OnPutInInventory(_inventory);
				}
			}
		}

		private void InteractWithItemObject(ItemObject itemObject)
		{
			Debug.Assert(itemObject.Item != null);
			IUsable usable = itemObject.Item as IUsable;
			IPickupable pickupable = itemObject.Item as IPickupable;
			IEquipable equipable = itemObject.Item as IEquipable;

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
				pickupable.OnRemoveFromGround(itemObject);
			}
		}
	}
}
