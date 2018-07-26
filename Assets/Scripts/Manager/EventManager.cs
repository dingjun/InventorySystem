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
		private static bool _isApplicationQuitting = false;
		
		private static EventManager Instance
		{
			get
			{
				if (_eventManager == null)
				{
					_eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
					Debug.Assert(_eventManager != null || _isApplicationQuitting == true);
					if (_isApplicationQuitting == false)
					{
						_eventManager.Init();
					}
				}
				return _eventManager;
			}
		}

		private void OnApplicationQuit()
		{
			_isApplicationQuitting = true;
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
			if (_isApplicationQuitting == false && Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
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
