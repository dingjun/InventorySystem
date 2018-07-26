using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InventoryController : MonoBehaviour
	{
		private const int MINIMUM_NUMBER_ROWS = 4;

		private List<InventoryRow> _rows;

		public List<InventoryRow> Rows
		{
			get
			{
				return _rows;
			}
		}

		public bool IsFull
		{
			get
			{
				foreach (var row in _rows)
				{
					if (row.IsFull == false)
					{
						return false;
					}
				}
				return true;
			}
		}
		
		// Use this for initialization
		void Start()
		{
			_rows = new List<InventoryRow>();
			for (int i = 0; i < MINIMUM_NUMBER_ROWS; ++i)
			{
				AddRow();
			}
		}

		public override string ToString()
		{
			string text = "";
			foreach (var row in _rows)
			{
				text += row.ToString() + "\n";
			}
			return text;
		}

		private void AddRow()
		{
			_rows.Add(new InventoryRow());
		}

		private int GetAvailableRowIndex()
		{
			for (int i = 0; i < _rows.Count; ++i)
			{
				if (_rows[i].IsFull == false)
				{
					return i;
				}
			}

			AddRow();
			return _rows.Count - 1;
		}

		private void UpdateCapacity()
		{
			while (_rows.Count > MINIMUM_NUMBER_ROWS)
			{
				int lastIndex = _rows.Count - 1;
				if (_rows[lastIndex].IsEmpty == false)
				{
					break;
				}
				_rows.RemoveAt(lastIndex);
			}
		}

		public void AddItem(Item item)
		{
			Debug.Assert(item is IPickupable);
			_rows[GetAvailableRowIndex()].AddItem(item);

			// TODO: with specified spot

			EventManager.TriggerEvent(EventName.UPDATE_INVENTORY);

			Debug.Log(this.ToString());
		}

		public void RemoveItem(int rowIndex, int itemIndex)
		{
			_rows[rowIndex].RemoveItem(itemIndex);
			UpdateCapacity();

			EventManager.TriggerEvent(EventName.UPDATE_INVENTORY);

			Debug.Log(this.ToString());
		}
	}
}
