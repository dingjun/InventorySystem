using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class ItemDatabase : MonoBehaviour
	{
		// TODO: read from text/json file
		/*
		 * UsableItem		name	n/a						[bufftable(each 5 int)]
		 * PickupableItem	name	n/a						[bufftable(each 5 int)]
		 * EquipableItem	name	equipmentType(1 int)	[bufftable(each 5 int)]
		 * StackableItem	name	stackLimit(1 int)		[bufftable(each 5 int)]
		 */
		private string[] _itemInfos =
		{
			"EquipableItem,	Weapon_1,		0,		2,	0,	1,	10,	0",
			"EquipableItem,	Shield_1,		1,		3,	0,	1,	11,	0",
			"EquipableItem,	Armor_1,		2,		4,	0,	1,	12,	0",
			"EquipableItem,	Ring_1,			3,		5,	0,	1,	13,	0",
			"EquipableItem,	Ring_2,			3,		2,	0,	0,	14,	0",
			"EquipableItem,	Shield_2,		1,		3,	0,	0,	15,	0",
			"EquipableItem,	Shield_3,		1,		4,	0,	0,	16,	0",
			"EquipableItem,	Armor_2,		2,		5,	0,	0,	17,	0",
			"EquipableItem,	Armor_3,		2,		2,	0,	0,	18,	0,	3,	0,	0,	-18,	0",
			"EquipableItem,	Weapon_2,		0,		0,	0,	1,	19,	0",
			"EquipableItem,	Shield_4,		1,		1,	0,	1,	20,	0",
			"EquipableItem,	Weapon_3,		0,		0,	1,	1,	21,	0",
			"UsableItem,	UsableItem_1,	n/a,	0,	0,	0,	22,	0",
			"EquipableItem,	Armor_4,		2,		1,	1,	1,	23,	0",
			"EquipableItem,	Armor_5,		2,		0,	0,	0,	24,	0",
			"EquipableItem,	Weapon_4,		0,		1,	0,	0,	25,	0",
			"EquipableItem,	Weapon_5,		0,		0,	1,	0,	26,	0",
			"EquipableItem,	Armor_6,		2,		1,	1,	0,	27,	0",
		};

		private Item[] _itemDatabase;

		public Sprite[] Icons;

		public int Count
		{
			get
			{
				return _itemDatabase.Length;
			}
		}

		private void Awake()
		{
			_itemDatabase = new Item[_itemInfos.Length];
			for (int i = 0; i < Count; ++i)
			{
				_itemDatabase[i] = ConstructItem(i);
			}
		}

		private Dictionary<Attribute.AttributeType, AttributeBuff> ConstructBuff(string[] infos)
		{
			var buffTable = new Dictionary<Attribute.AttributeType, AttributeBuff>();
			for (int i = 3; i < infos.Length; i += 5)
			{
				Attribute.AttributeType attributeType = (Attribute.AttributeType)int.Parse(infos[i]);
				AttributeBuff.BuffTarget buffTarget = (AttributeBuff.BuffTarget)int.Parse(infos[i + 1]);
				AttributeBuff.BuffType buffType = (AttributeBuff.BuffType)int.Parse(infos[i + 2]);
				int value = int.Parse(infos[i + 3]);
				float seconds = float.Parse(infos[i + 4]);
				buffTable.Add(attributeType, new AttributeBuff(buffTarget, buffType, value, seconds));
			}
			return buffTable;
		}

		private Item ConstructItem(int itemIndex)
		{
			string[] infos = _itemInfos[itemIndex].Replace("\t", "").Split(',');
			switch (infos[0])
			{
			case "UsableItem":
				{
					return new UsableItem(infos[1], Icons[itemIndex], ConstructBuff(infos));
				}
			case "PickupableItem":
				{
					return new PickupableItem(infos[1], Icons[itemIndex], ConstructBuff(infos));
				}
			case "EquipableItem":
				{
					return new EquipableItem(infos[1], Icons[itemIndex], ConstructBuff(infos), (Equipment.EquipmentType)int.Parse(infos[2]));
				}
			case "StackableItem":
				{
					int stackLimit = int.Parse(infos[2]);
					if (stackLimit < 0)
					{
						return new StackableItem(infos[1], Icons[itemIndex], ConstructBuff(infos));
					}
					else
					{
						return new StackableItem(infos[1], Icons[itemIndex], ConstructBuff(infos), stackLimit);
					}
				}
			default:
				Debug.Assert(false, _itemInfos[itemIndex]);
				return null;
			}
		}

		private int GetItemTypeIndex(string itemName)
		{
			for (int i = 0; i < Count; ++i)
			{
				if (itemName == _itemDatabase[i].Name)
				{
					return i;
				}
			}
			Debug.Assert(false);
			return -1;
		}

		public Item GetItem(int itemTypeIndex)
		{
			return _itemDatabase[itemTypeIndex].Copy();
		}

		public Item GetItem(string itemName)
		{
			return GetItem(GetItemTypeIndex(itemName));
		}

		/*public Item AttachItemComponent(GameObject gameObject, string itemName)
		{
			return AttachItemComponent(gameObject, GetItemTypeIndex(itemName));
		}

		public Item AttachItemComponent(GameObject gameObject, int itemTypeIndex)
		{
			/*Item item = EquipableItem.Attach(gameObject, (EquipableItem)_itemDatabase[itemTypeIndex]);
			return item;
			//gameObject.
		}*/



		/*public Item AttachItemComponent(GameObject gameObject, int itemTypeIndex)
		{
			
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
					
					buttTable.Add(Attribute.AttributeType.Agility, new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Percentage, -10));
					return EquipableItem.Attach(gameObject, "Armor_6", Icons[17], buttTable, Equipment.EquipmentType.Armor);
				}
			default:
				{
					return PickupableItem.Attach(gameObject, "pickupableItem", Icons[17], new Dictionary<Attribute.AttributeType, AttributeBuff>());
				}
			}
		}*/
	}
}
