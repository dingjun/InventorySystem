using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class AttributeScreen : MonoBehaviour
	{
		private AttributeController _playerStats;
		private List<AttributeEntry> _entries;
		
		public GameObject AttributeEntry;

		// Use this for initialization
		void Start()
		{
			_entries = new List<AttributeEntry>();

			GameObject player = GameObject.FindGameObjectWithTag(Constant.TAG_PLAYER);
			_playerStats = player.GetComponent<AttributeController>();

			float height = AttributeEntry.GetComponent<RectTransform>().rect.height;
			for (int i = 0; i < _playerStats.AttributeTable.Count; ++i)
			{
				GameObject entry = Instantiate(AttributeEntry, transform);
				entry.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, i * height, height);
				_entries.Add(entry.GetComponent<AttributeEntry>());
			}

			UpdateAttribute();
		}

		private void OnEnable()
		{
			EventManager.StartListening(EventName.UPDATE_ATTRIBUTE, UpdateAttribute);
			if (_entries != null)
			{
				UpdateAttribute();
			}
		}

		private void OnDisable()
		{
			EventManager.StopListening(EventName.UPDATE_ATTRIBUTE, UpdateAttribute);
		}

		private void UpdateAttribute()
		{
			for (int i = 0; i < _playerStats.AttributeTable.Count; ++i)
			{
				_entries[i].SetText(_playerStats.AttributeTable[(Attribute.AttributeType)i]);
			}
		}
	}
}
