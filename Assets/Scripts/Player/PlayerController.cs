using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class PlayerController : MonoBehaviour
	{
		private const float SPEED = 3f;
		private const float PROXIMITY_SQUARE = 4f;
		private const float PHYSICS_CIRCLE_RADIUS = 1f;

		private Vector2 _playerFootPositionOffset = new Vector2(0f, -0.25f);

		private Rigidbody2D _rigidBody;
		private Animator _animator;
		private PickupOptionController _pickupOption;
		private InventoryController _inventory;
		private EquipmentController _equipment;
		private AttributeController _stats;
		private AirItemController _airItem;
		private Vector3 _lastPosition;

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

			_lastPosition = transform.position;
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.CHECK_PHYSICS_CIRCLE, CheckPhysicsCircle);

			EventManager.StartListening(EventName.CLICK_ITEM_OBJECT, ClickItemObject);
			EventManager.StartListening(EventName.LEFT_CLICK_WORLD, LeftClickWorld);
			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_SLOT, LeftClickItemSlot);
			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_ICON, LeftClickItemIcon);
			EventManager.StartListening(EventName.RIGHT_CLICK_ITEM_ICON, EquipItem);
			EventManager.StartListening(EventName.MIDDLE_CLICK_ITEM_ICON, ConsumeItem);

			EventManager.StartListening(EventName.LEFT_CLICK_ITEM_ICON_DROP_HOTKEY, DropItem);
			EventManager.StartListening(EventName.HOVER_ITEM_ICON_SPLIT_HOTKEY, SplitItem);
			EventManager.StartListening(EventName.HOVER_ITEM_ICON_DROP_HOTKEY, DropItem);
			EventManager.StartListening(EventName.HOVER_ITEM_ICON_EQUIP_HOTKEY, EquipItem);

			EventManager.StartListening(EventName.UNEQUIP_ITEM_NO_DURABILITY, UnequipItemNoDurability);
			EventManager.StartListening(EventName.SPLIT_STACKABLE_ITEM, SplitStackableItem);
			EventManager.StartListening(EventName.RETURN_AIR_ITEM, ReturnAirItem);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.CHECK_PHYSICS_CIRCLE, CheckPhysicsCircle);

			EventManager.StopListening(EventName.CLICK_ITEM_OBJECT, ClickItemObject);
			EventManager.StopListening(EventName.LEFT_CLICK_WORLD, LeftClickWorld);
			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_SLOT, LeftClickItemSlot);
			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_ICON, LeftClickItemIcon);
			EventManager.StopListening(EventName.RIGHT_CLICK_ITEM_ICON, EquipItem);
			EventManager.StopListening(EventName.MIDDLE_CLICK_ITEM_ICON, ConsumeItem);

			EventManager.StopListening(EventName.LEFT_CLICK_ITEM_ICON_DROP_HOTKEY, DropItem);
			EventManager.StopListening(EventName.HOVER_ITEM_ICON_SPLIT_HOTKEY, SplitItem);
			EventManager.StopListening(EventName.HOVER_ITEM_ICON_DROP_HOTKEY, DropItem);
			EventManager.StopListening(EventName.HOVER_ITEM_ICON_EQUIP_HOTKEY, EquipItem);

			EventManager.StopListening(EventName.UNEQUIP_ITEM_NO_DURABILITY, UnequipItemNoDurability);
			EventManager.StopListening(EventName.SPLIT_STACKABLE_ITEM, SplitStackableItem);
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

			// calculate distance
			object[] eventParams = { (object) (transform.position - _lastPosition).magnitude };
			EventManager.TriggerEvent(EventName.PLAYER_WALK, eventParams);
			_lastPosition = transform.position;

			// animation
			_animator.speed = input.magnitude;
			if (_animator.speed > 0f)
			{
				float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
				_animator.SetFloat("Angle", angle);
			}

			// pick up items
			if (_pickupOption.Option == PickupOptionController.PickupOption.Option5
				|| _pickupOption.Option == PickupOptionController.PickupOption.Option7)
			{
				CheckPhysicsCircle();
			}
		}

		private void OnTriggerStay2D(Collider2D other)
		{
			if (_pickupOption.Option == PickupOptionController.PickupOption.Option2
				|| _pickupOption.Option == PickupOptionController.PickupOption.Option3 && Input.GetKey(InputManager.PICK_UP_ITEM_KEY))
			{
				CheckItemObjectCollider(other);
			}
		}

		private void CheckItemObjectCollider(Collider2D other)
		{
			ItemObject itemObject = other.GetComponent<ItemObject>();
			if (itemObject != null)
			{
				InteractWithItemObject(itemObject);
			}
		}

		private void InteractWithItemObject(ItemObject itemObject)
		{
			Debug.Assert(itemObject.Item != null);
			IUsable usable = itemObject.Item as IUsable;
			IPickupable pickupable = itemObject.Item as IPickupable;
			IEquipable equipable = itemObject.Item as IEquipable;
			IStackable stackable = itemObject.Item as IStackable;

			if (usable != null)
			{
				usable.OnUse(_stats, itemObject);
			}
			else if (pickupable != null)
			{
				if (equipable != null && equipable.IsDurable && _equipment.EquipmentTable[equipable.EquipmentType].IsEmpty)
				{
					equipable.OnEquip(_equipment, _stats);
				}
				else if (stackable != null)
				{
					stackable.OnStack(_inventory);
				}
				else
				{
					pickupable.OnPutInInventory(_inventory);
				}
				pickupable.OnRemoveFromGround(itemObject);
			}
		}

		private void CheckPhysicsCircle(object[] eventParams = null)
		{
			if (_pickupOption.Option == PickupOptionController.PickupOption.Option1
				|| _pickupOption.Option == PickupOptionController.PickupOption.Option2
				|| _pickupOption.Option == PickupOptionController.PickupOption.Option3)
			{
				return;
			}

			Vector2 point = (Vector2)transform.position + _playerFootPositionOffset;
			if (_pickupOption.Option == PickupOptionController.PickupOption.Option6
				|| _pickupOption.Option == PickupOptionController.PickupOption.Option7)
			{
				Vector2 direction = _rigidBody.velocity.normalized;
				if (direction == Vector2.zero)
				{
					return;
				}
				point += direction;
			}

			Collider2D[] colliders = Physics2D.OverlapCircleAll(point, PHYSICS_CIRCLE_RADIUS, LayerMask.GetMask(Constant.LAYER_ITEM));
			foreach (var collider in colliders)
			{
				CheckItemObjectCollider(collider);
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
				if ((Equipment.EquipmentType)slotPosition.SlotIndex != equipable.EquipmentType)
				{
					Debug.Log("Not the same equipment type");
					return;
				}
				if (equipable.IsDurable == false)
				{
					Debug.Log("Not durable");
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
				else if (itemIcon.Item is IStackable)
				{
					EventManager.TriggerEvent(EventName.OPEN_SPLIT_SCREEN, eventParams);
					return;
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
					if ((Equipment.EquipmentType)slotPosition.SlotIndex != equipableAir.EquipmentType)
					{
						Debug.Log("Not the same equipment type");
						return;
					}
					if (equipable.IsDurable == false)
					{
						Debug.Log("Not durable");
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

		private void EquipItem(object[] eventParams)
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
			if (equipable.IsDurable == false)
			{
				Debug.Log("Not durable");
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

		private void UnequipItemNoDurability(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is Item);
			IPickupable pickupable = eventParams[0] as IPickupable;
			IEquipable equipable = eventParams[0] as IEquipable;
			equipable.OnUnequip(_equipment, _stats);
			pickupable.OnPutInInventory(_inventory);
		}

		private void DropItem(object[] eventParams)
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

		private void ConsumeItem(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is ItemIcon);
			ItemIcon itemIcon = (ItemIcon)eventParams[0];
			IConsumable consumable = itemIcon.Item as IConsumable;
			if (consumable == null)
			{
				Debug.Log("Not consumable");
				return;
			}
			consumable.OnConsume(_inventory, _stats, itemIcon.Position);
		}

		private void SplitItem(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 1 && eventParams[0] is ItemIcon);
			ItemIcon itemIcon = (ItemIcon)eventParams[0];
			IStackable stackable = itemIcon.Item as IStackable;
			if (stackable == null)
			{
				Debug.Log("Not splitable");
				return;
			}
			EventManager.TriggerEvent(EventName.OPEN_SPLIT_SCREEN, eventParams);
		}

		private void SplitStackableItem(object[] eventParams)
		{
			Debug.Assert(eventParams.Length == 2 && eventParams[0] is ItemIcon && eventParams[1] is int);
			ItemIcon itemIcon = (ItemIcon)eventParams[0];
			int splitCount = (int)eventParams[1];
			Item item = itemIcon.Item;
			SlotPosition slotPosition = itemIcon.Position;
			IStackable stackable = (IStackable)item;
			
			if (splitCount == stackable.Count)
			{
				IPickupable pickupable = (IPickupable)item;
				pickupable.OnRemoveFromInventory(_inventory, slotPosition);
				pickupable.OnPutInAir(_airItem, slotPosition);
			}
			else
			{
				Item splitItem = item.Copy();
				((IStackable)splitItem).Count = splitCount;
				((IPickupable)splitItem).OnPutInAir(_airItem, slotPosition);
				stackable.OnSplit(_inventory, slotPosition, splitCount);
			}
		}

		private void ReturnAirItem(object[] eventParams)
		{
			if (_airItem.IsEmpty)
			{
				return;
			}

			SlotPosition originalPosition = _airItem.OriginalPosition;
			Item itemAir = _airItem.Item;
			IPickupable pickupableAir = itemAir as IPickupable;
			IEquipable equipableAir = itemAir as IEquipable;
			IStackable stackableAir = itemAir as IStackable;

			pickupableAir.OnRemoveFromAir(_airItem);

			if (originalPosition.RowIndex == EquipmentSlot.EQUIPMENT_SLOT_ROW_INDEX)
			{
				Equipment.EquipmentType equipmentType = (Equipment.EquipmentType)originalPosition.SlotIndex;

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
				if (stackableAir != null)
				{
					stackableAir.OnStack(_inventory, originalPosition);
				}
				else if (_inventory.IsItemEmpty(originalPosition))
				{
					pickupableAir.OnPutInInventory(_inventory, originalPosition);
				}
				else
				{
					pickupableAir.OnPutInInventory(_inventory);
				}
			}
		}
	}
}
