using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InventoryScreen : MonoBehaviour
	{
		private InventoryController _playerInventory;
		private List<InventoryRowEntry> _entries;

		public GameObject InventoryRowPrefab;
		public Transform InventoryRowParent;

		private void Awake()
		{
			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerInventory = player.GetComponent<InventoryController>();
			_entries = new List<InventoryRowEntry>();
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.UPDATE_INVENTORY, UpdateInventory);
			UpdateInventory(null);
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.UPDATE_INVENTORY, UpdateInventory);
		}

		private void UpdateInventory(object[] eventParams)
		{
			// remove rows
			while (_entries.Count > _playerInventory.RowCount)
			{
				int index = _entries.Count - 1;
				_entries.RemoveAt(index);
				Destroy(InventoryRowParent.GetChild(index).gameObject);
			}
			
			for (int i = 0; i < _playerInventory.RowCount; ++i)
			{
				// add rows
				if (i == _entries.Count)
				{
					GameObject entry = Instantiate(InventoryRowPrefab, InventoryRowParent);
					_entries.Add(entry.GetComponent<InventoryRowEntry>());
					_entries[i].SetPosition(i);
				}

				// update
				_entries[i].UpdateRow(_playerInventory.GetRow(i));
			}
		}
	}
}
