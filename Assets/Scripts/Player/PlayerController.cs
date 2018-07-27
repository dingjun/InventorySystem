using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class PlayerController : MonoBehaviour
	{
		public enum ItemAction { Select, Equip, Consume };

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

		void FixedUpdate()
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

		public void Interact(Transform other)
		{
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

		public void InteractWithItemIcon(ItemAction itemAction, ItemIcon itemIcon)
		{
			switch (itemAction)
			{
			case ItemAction.Select:
				{
					// TODO

					// drop for now
					IPickupable pickupable = itemIcon.Item as IPickupable;
					IEquipable equipable = itemIcon.Item as IEquipable;
					if (itemIcon.IsEquipmentIcon)
					{
						equipable.OnUnequip(_equipment, _stats);
					}
					else
					{
						pickupable.OnRemoveFromInventory(_inventory, itemIcon.Position);
					}
					break;
				}
			case ItemAction.Equip:
				{
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
					break;
				}
			case ItemAction.Consume:
				{
					Debug.Log("Consume " + itemIcon.Item.Name);

					// TODO

					break;
				}
			}
		}
	}
}
