using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class ItemDatabase : MonoBehaviour
	{
		public const int NUMBER_ITEM_TYPE = 18;

		public Sprite[] Icons;

		/*
		 * Usable, name, icon, bufftable
		 * Pickupable, name, icon, bufftable
		 * Equipable, name, icon, bufftable, slottype
		 * Stackble, name, icon, bufftable, stackLimit
		 */
		public Item AttachItemComponent(GameObject gameObject, int itemTypeIndex)
		{
			// TODO: read from text/json file
			Dictionary<Attribute.AttributeType, AttributeBuff> buttTable = new Dictionary<Attribute.AttributeType, AttributeBuff>();
			switch (itemTypeIndex)
			{
			case 0:
				{
					buttTable.Add(Attribute.AttributeType.Strength, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 10));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 5));
					return EquipableItem.Attach(gameObject, "Weapon_1", Icons[0], buttTable, Equipment.EquipmentType.Weapon);
				}
			case 1:
				{
					buttTable.Add(Attribute.AttributeType.Dexterity, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, 10));
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, 5));
					return EquipableItem.Attach(gameObject, "Shield_1", Icons[1], buttTable, Equipment.EquipmentType.Shield);
				}
			case 2:
				{
					buttTable.Add(Attribute.AttributeType.Health, new AttributeBuff(AttributeBuff.BuffTarget.Maximum, AttributeBuff.BuffType.Amount, 100));
					return EquipableItem.Attach(gameObject, "Armor_1", Icons[2], buttTable, Equipment.EquipmentType.Armor);
				}
			case 3:
				{
					buttTable.Add(Attribute.AttributeType.Mana, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 100));
					return EquipableItem.Attach(gameObject, "Ring_1", Icons[3], buttTable, Equipment.EquipmentType.Ring);
				}
			case 4:
				{
					buttTable.Add(Attribute.AttributeType.Mana, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 300));
					return EquipableItem.Attach(gameObject, "Ring_2", Icons[4], buttTable, Equipment.EquipmentType.Ring);
				}
			case 5:
				{
					buttTable.Add(Attribute.AttributeType.Strength, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Dexterity, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, -10));
					return EquipableItem.Attach(gameObject, "Shield_2", Icons[5], buttTable, Equipment.EquipmentType.Shield);
				}
			case 6:
				{
					buttTable.Add(Attribute.AttributeType.Strength, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Shield_3", Icons[6], buttTable, Equipment.EquipmentType.Shield);
				}
			case 7:
				{
					buttTable.Add(Attribute.AttributeType.Strength, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 15));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 15));
					return EquipableItem.Attach(gameObject, "Armor_2", Icons[7], buttTable, Equipment.EquipmentType.Armor);
				}
			case 8:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Armor_3", Icons[8], buttTable, Equipment.EquipmentType.Armor);
				}
			case 9:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Weapon_2", Icons[9], buttTable, Equipment.EquipmentType.Weapon);
				}
			case 10:
				{
					buttTable.Add(Attribute.AttributeType.Strength, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Shield_4", Icons[10], buttTable, Equipment.EquipmentType.Shield);
				}
			case 11:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Weapon_3", Icons[11], buttTable, Equipment.EquipmentType.Weapon);
				}
			case 12:
				{
					AttributeBuff buff = new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 10);
					buttTable.Add(Attribute.AttributeType.Strength, buff);
					return UsableItem.Attach(gameObject, "UsableItem_1", Icons[12], buttTable);
				}
			case 13:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Armor_4", Icons[13], buttTable, Equipment.EquipmentType.Armor);
				}
			case 14:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Armor_5", Icons[14], buttTable, Equipment.EquipmentType.Armor);
				}
			case 15:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Weapon_4", Icons[15], buttTable, Equipment.EquipmentType.Weapon);
				}
			case 16:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Weapon_5", Icons[16], buttTable, Equipment.EquipmentType.Weapon);
				}
			case 17:
				{
					buttTable.Add(Attribute.AttributeType.Intelligence, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 30));
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Armor_6", Icons[17], buttTable, Equipment.EquipmentType.Armor);
				}
			default:
				{
					return PickupableItem.Attach(gameObject, "pickupableItem", Icons[17], new Dictionary<Attribute.AttributeType, AttributeBuff>());
				}
			}
		}
	}
}
