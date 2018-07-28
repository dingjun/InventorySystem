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
		
		private void Awake()
		{
			_rows = new List<InventoryRow>();
			for (int i = 0; i < MINIMUM_NUMBER_ROWS; ++i)
			{
				AddRow();
			}
		}

		public override string ToString()
		{
			string text = "==== INVENTORY ====\n";
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

		public void AddItem(Item item, SlotPosition? slotPosition = null)
		{
			Debug.Assert(item is IPickupable);

			if (slotPosition == null)
			{
				_rows[GetAvailableRowIndex()].AddItem(item);
			}
			else
			{
				SlotPosition position = slotPosition.GetValueOrDefault();
				_rows[position.RowIndex].AddItem(item, position.SlotIndex);
			}

			EventManager.TriggerEvent(EventName.UPDATE_INVENTORY);

			Debug.Log(this.ToString());
		}

		public void RemoveItem(SlotPosition slotPosition)
		{
			_rows[slotPosition.RowIndex].RemoveItem(slotPosition.SlotIndex);
			UpdateCapacity();

			EventManager.TriggerEvent(EventName.UPDATE_INVENTORY);

			Debug.Log(this.ToString());
		}
	}
}
