using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public interface IUsable
	{
		void OnUse(AttributeController _playerStats);
	}
}
