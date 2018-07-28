using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class AirItemUI : MonoBehaviour
	{
		private AirItemController _playerAirItem;
		private ItemSlot _itemSlot;

		private void Awake()
		{
			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerAirItem = player.GetComponent<AirItemController>();
			_itemSlot = GetComponent<ItemSlot>();
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.UPDATE_AIR_ITEM, UpdateAirItem);
			UpdateAirItem(null);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.UPDATE_AIR_ITEM, UpdateAirItem);
		}

		private void UpdateAirItem(object[] eventParams)
		{
			_itemSlot.UpdateSlot(_playerAirItem.Item);
		}
	}
}
