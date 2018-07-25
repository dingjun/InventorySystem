using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class ItemDatabase : MonoBehaviour
	{
		public Sprite[] Icons;

		public const int NUMBER_ITEM_TYPE = 2;

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
				AttributeBuff buff = new AttributeBuff(AttributeBuff.BuffTarget.Current, AttributeBuff.BuffType.Amount, 10);
				buttTable.Add(Attribute.AttributeType.Strength, buff);
				return UsableItem.Attach(gameObject, "usableItem", Icons[0], buttTable);
			case 1:
				return PickupableItem.Attach(gameObject, "pickupableItem", Icons[1], new Dictionary<Attribute.AttributeType, AttributeBuff>());
			default:
				return null;
			}
		}
	}
}
