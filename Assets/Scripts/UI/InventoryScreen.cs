﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InventoryScreen : MonoBehaviour
	{
		private InventoryController _playerInventory;
		private List<InventoryRowEntry> _entries;

		public GameObject RowEntryPrefab;
		public Transform RowEntryParent;

		void Awake()
		{
			_entries = new List<InventoryRowEntry>();

			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerInventory = player.GetComponent<InventoryController>();
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.UPDATE_INVENTORY, UpdateInventory);
			UpdateInventory();
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.UPDATE_INVENTORY, UpdateInventory);
		}

		private void UpdateInventory()
		{
			// remove rows
			while (_entries.Count > _playerInventory.Rows.Count)
			{
				int index = _entries.Count - 1;
				_entries.RemoveAt(index);
				Destroy(RowEntryParent.GetChild(index).gameObject);
			}
			
			for (int i = 0; i < _playerInventory.Rows.Count; ++i)
			{
				// add rows
				if (i == _entries.Count)
				{
					GameObject entry = Instantiate(RowEntryPrefab, RowEntryParent);
					_entries.Add(entry.GetComponent<InventoryRowEntry>());
					_entries[i].SetInformation(_playerInventory, i);
				}

				// update
				_entries[i].UpdateRow(_playerInventory.Rows[i]);
			}
		}
	}
}
