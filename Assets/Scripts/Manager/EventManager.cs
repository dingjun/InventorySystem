using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
	public class EventManager : MonoBehaviour
	{
		private Dictionary<string, UnityEvent> _eventDictionary;

		private static EventManager _eventManager;

		public static EventManager Instance
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
					Debug.Assert(_eventManager != null);
					_eventManager.Init();
				}
				return _eventManager;
			}
		}

		private void Init()
		{
			if (_eventDictionary == null)
			{
				_eventDictionary = new Dictionary<string, UnityEvent>();
			}
		}

		public static void StartListening(string eventName, UnityAction listener)
		{
			UnityEvent thisEvent;
			if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.AddListener(listener);
			}
			else
			{
				thisEvent = new UnityEvent();
				thisEvent.AddListener(listener);
				Instance._eventDictionary.Add(eventName, thisEvent);
			}
		}

		public static void StopListening(string eventName, UnityAction listener)
		{
			UnityEvent thisEvent;
			if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.RemoveListener(listener);
			}
		}

		public static void TriggerEvent(string eventName)
		{
			UnityEvent thisEvent;
			if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
			{
				thisEvent.Invoke();
			}
		}
	}
}
