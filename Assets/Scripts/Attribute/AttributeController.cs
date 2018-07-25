using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
	public class AttributeController : MonoBehaviour
	{
		private Dictionary<Attribute.AttributeType, Attribute> _attributeTable;

		public Dictionary<Attribute.AttributeType, Attribute> AttributeTable
		{
			get
			{
				return _attributeTable;
			}
		}
		
		void Awake()
		{
			_attributeTable = new Dictionary<Attribute.AttributeType, Attribute>();

			// TODO: read from text file
			_attributeTable.Add(Attribute.AttributeType.Health, new Attribute(Attribute.AttributeType.Health, 800, 1000));
			_attributeTable.Add(Attribute.AttributeType.Mana, new Attribute(Attribute.AttributeType.Mana, 400, 500));
			_attributeTable.Add(Attribute.AttributeType.Strength, new Attribute(Attribute.AttributeType.Strength, 100));
			_attributeTable.Add(Attribute.AttributeType.Dexterity, new Attribute(Attribute.AttributeType.Dexterity, 70));
			_attributeTable.Add(Attribute.AttributeType.Agility, new Attribute(Attribute.AttributeType.Agility, 130));
			_attributeTable.Add(Attribute.AttributeType.Intelligence, new Attribute(Attribute.AttributeType.Intelligence, 180));
		}

		public void ApplyBuff(Dictionary<Attribute.AttributeType, AttributeBuff> buffTable)
		{
			foreach (var entry in buffTable)
			{
				_attributeTable[entry.Key].AddBuff(entry.Value);
			}

			EventManager.TriggerEvent(EventName.UPDATE_ATTRIBUTE);
		}

		public void UnapplyBuff(Dictionary<Attribute.AttributeType, AttributeBuff> buffTable)
		{
			foreach (var entry in buffTable)
			{
				_attributeTable[entry.Key].RemoveBuff(entry.Value);
			}

			EventManager.TriggerEvent(EventName.UPDATE_ATTRIBUTE);
		}
	}
}
