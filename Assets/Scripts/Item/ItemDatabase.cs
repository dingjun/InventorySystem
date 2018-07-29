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
			"EquipableItem,		Weapon_1,		0,		2,	0,	1,	10,	0",
			"EquipableItem,		Shield_1,		1,		3,	0,	1,	11,	0",
			"EquipableItem,		Armor_1,		2,		4,	0,	1,	12,	0",
			"EquipableItem,		Ring_1,			3,		5,	0,	1,	13,	0",
			"EquipableItem,		Ring_2,			3,		2,	0,	0,	14,	0",
			"EquipableItem,		Shield_2,		1,		3,	0,	0,	15,	0",
			"EquipableItem,		Shield_3,		1,		4,	0,	0,	16,	0",
			"EquipableItem,		Armor_2,		2,		5,	0,	0,	17,	0",
			"EquipableItem,		Armor_3,		2,		2,	0,	0,	18,	0,	3,	0,	0,	-18,	0",
			"EquipableItem,		Weapon_2,		0,		0,	0,	1,	19,	0",
			"EquipableItem,		Shield_4,		1,		1,	0,	1,	20,	0",
			"EquipableItem,		Weapon_3,		0,		0,	1,	1,	21,	0",
			"UsableItem,		UsableItem_1,	n/a,	0,	0,	0,	22,	0",
			"EquipableItem,		Armor_4,		2,		1,	1,	1,	23,	0",
			"EquipableItem,		Armor_5,		2,		0,	0,	0,	24,	0",
			"EquipableItem,		Weapon_4,		0,		1,	0,	0,	25,	0",
			"EquipableItem,		Weapon_5,		0,		0,	1,	0,	26,	0",
			"EquipableItem,		Armor_6,		2,		1,	1,	0,	27,	0",
			"StackableItem,		Rune_1,			2,		0,	1,	0,	1,	0",
			"StackableItem,		Rune_2,			3,		1,	1,	0,	2,	0",
			"StackableItem,		Rune_3,			4,		2,	0,	0,	3,	0",
			"StackableItem,		Rune_4,			5,		3,	0,	0,	4,	0",
			"StackableItem,		Rune_5,			2,		4,	0,	0,	5,	0",
			"StackableItem,		Rune_6,			3,		5,	0,	0,	6,	0",
			"StackableItem,		Rune_7,			4,		0,	1,	0,	7,	0",
			"StackableItem,		Rune_8,			5,		1,	1,	0,	8,	0",
			"StackableItem,		Rune_9,			2,		2,	0,	0,	9,	0",
			"StackableItem,		Rune_10,		3,		3,	0,	0,	10,	0",
			"StackableItem,		Rune_11,		4,		4,	0,	1,	11,	0",
			"StackableItem,		Rune_12,		5,		5,	0,	1,	12,	0",
			"StackableItem,		Rune_13,		2,		0,	1,	1,	13,	0",
			"StackableItem,		Rune_14,		3,		1,	1,	1,	14,	0",
			"StackableItem,		Rune_15,		4,		2,	0,	1,	15,	0",
			"StackableItem,		Rune_16,		5,		3,	0,	1,	16,	0",
			"StackableItem,		Rune_17,		2,		4,	0,	1,	17,	0",
			"StackableItem,		Rune_18,		3,		5,	0,	1,	18,	0",
			"StackableItem,		Rune_19,		4,		0,	1,	1,	19,	0",
			"StackableItem,		Rune_20,		5,		1,	1,	1,	20,	0",
			"PickupableItem,	Book_1,			n/a,	0,	1,	0,	1,	0",
			"PickupableItem,	Book_2,			n/a,	1,	1,	0,	2,	0",
			"PickupableItem,	Book_3,			n/a,	2,	0,	0,	3,	0",
			"PickupableItem,	Book_4,			n/a,	3,	0,	0,	4,	0",
			"PickupableItem,	Book_5,			n/a,	4,	0,	0,	5,	0",
			"PickupableItem,	Book_6,			n/a,	5,	0,	0,	6,	0",
			"PickupableItem,	Book_7,			n/a,	0,	1,	0,	7,	0",
			"PickupableItem,	Book_8,			n/a,	1,	1,	0,	8,	0",
			"PickupableItem,	Book_9,			n/a,	2,	0,	0,	9,	0",
			"PickupableItem,	Book_10,		n/a,	3,	0,	0,	10,	0",
			"PickupableItem,	Book_11,		n/a,	4,	0,	1,	11,	0",
			"PickupableItem,	Book_12,		n/a,	5,	0,	1,	12,	0",
			"PickupableItem,	Book_13,		n/a,	0,	1,	1,	13,	0",
			"PickupableItem,	Book_14,		n/a,	1,	1,	1,	14,	0",
			"PickupableItem,	Book_15,		n/a,	2,	0,	1,	15,	0",
			"PickupableItem,	Book_16,		n/a,	3,	0,	1,	16,	0",
			"PickupableItem,	Book_17,		n/a,	4,	0,	1,	17,	0",
			"PickupableItem,	Book_18,		n/a,	5,	0,	1,	18,	0",
			"PickupableItem,	Book_19,		n/a,	0,	1,	1,	19,	0",
			"PickupableItem,	Book_20,		n/a,	1,	1,	1,	20,	0",
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
	}
}
