using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	[System.Serializable]
	public class UsableItem : Item, IUsable
	{
		public void OnUse(AttributeController _playerStats)
		{
			Debug.Log("Use " + Name);
			ApplyBuff(_playerStats);
		}

		public static UsableItem Attach(GameObject itemObject, string name, Sprite icon, Dictionary<Attribute.AttributeType, AttributeBuff> buffTable)
		{
			UsableItem item = itemObject.AddComponent<UsableItem>();
			item._name = name;
			item._icon = icon;
			item._buffTable = buffTable;
			return item;
		}
	}
}
