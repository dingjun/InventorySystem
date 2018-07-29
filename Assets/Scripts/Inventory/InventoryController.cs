using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class InventoryController : MonoBehaviour
	{
		private const int MINIMUM_NUMBER_ROWS = 4;

		private List<InventoryRow> _rows;

		public int RowCount
		{
			get
			{
				return _rows.Count;
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
			AddRows(MINIMUM_NUMBER_ROWS);
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
		
		private int GetAvailableRowIndex()
		{
			for (int i = 0; i < RowCount; ++i)
			{
				if (_rows[i].IsFull == false)
				{
					return i;
				}
			}
			Debug.Log(false);
			return - 1;
		}

		private void AddRows(int num = 1)
		{
			for (int i = 0; i < num; ++i)
			{
				_rows.Add(new InventoryRow());
			}
		}

		private void UpdateCapacity()
		{
			while (RowCount > MINIMUM_NUMBER_ROWS)
			{
				int lastIndex = RowCount - 1;
				if (_rows[lastIndex].IsEmpty == false)
				{
					break;
				}
				_rows.RemoveAt(lastIndex);
			}
		}

		public bool IsItemEmpty(SlotPosition slotPosition)
		{
			return slotPosition.RowIndex >= RowCount || _rows[slotPosition.RowIndex].IsItemEmpty(slotPosition.SlotIndex);
		}

		public InventoryRow GetRow(int rowIndex)
		{
			Debug.Assert(rowIndex < RowCount);
			return _rows[rowIndex];
		}

		public void AddItem(Item item, SlotPosition? slotPosition = null)
		{
			Debug.Assert(item is IPickupable);

			if (slotPosition == null)
			{
				if (IsFull)
				{
					AddRows();
				}
				_rows[GetAvailableRowIndex()].AddItem(item);
			}
			else
			{
				SlotPosition position = slotPosition.GetValueOrDefault();
				AddRows(position.RowIndex - RowCount + 1);
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
