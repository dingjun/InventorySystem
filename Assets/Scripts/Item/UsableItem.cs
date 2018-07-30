using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class UsableItem : Item, IUsable
	{
		public UsableItem(string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable)
			: base(name, icon, buffTable)
		{

		}

		public override Item Copy()
		{
			return new UsableItem(_name, _icon, _buffTable);
		}

		public override string ToString()
		{
			return "Name: " + Name + "\nType: Usable\nBuff: " + BuffTableToString();
		}

		public void OnUse(AttributeController playerStats, ItemObject itemObject)
		{
			Debug.Log("Use " + Name);
			ApplyBuff(playerStats);
			itemObject.DestroySelf();
		}
	}
}
