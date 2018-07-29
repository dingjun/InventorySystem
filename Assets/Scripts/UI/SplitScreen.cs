using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace InventorySystem
{
	public class SplitScreen : MonoBehaviour
	{
		private ItemIcon _itemIcon;
		private IStackable _stackable;
		private int _splitCount;

		public InputField StackCountInputField;

		public int SplitCount
		{
			get
			{
				return _splitCount;
			}
			set
			{
				int validValue = Mathf.Clamp(value, 1, _stackable.Count);
				_splitCount = validValue;
				StackCountInputField.text = _splitCount.ToString();
			}
		}

		public void Open(ItemIcon itemIcon)
		{
			Debug.Assert(itemIcon.Item is IStackable);
			_itemIcon = itemIcon;
			_stackable = (IStackable)_itemIcon.Item;
			SplitCount = _stackable.Count;
			gameObject.SetActive(true);
		}
		
		public void OnEndEdit()
		{
			SplitCount = int.Parse(StackCountInputField.text);
		}

		public void OnStep(int step)
		{
			SplitCount += step;
		}

		public void OnOK()
		{
			object[] eventParams = { (object)_itemIcon, (object)SplitCount };
			EventManager.TriggerEvent(EventName.SPLIT_STACKABLE_ITEM, eventParams);
			gameObject.SetActive(false);
		}

		public void OnCancel()
		{
			// do nothing
			gameObject.SetActive(false);
		}
	}
}
