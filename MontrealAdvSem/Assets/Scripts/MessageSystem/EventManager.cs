using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour 
{
	//Event dictionary
	private Dictionary <string, UnityEvent> eventDictionary;
	//Manager reference
	private static EventManager eventManager;
	//Manager instance
	public static EventManager instance
	{
		get
		{
			if(!eventManager)
			{
				//Search for event manager in scene
				eventManager = FindObjectOfType(typeof (EventManager)) as EventManager;

				if(!eventManager)
				{
					Debug.Log("There needs to be one actice EventManager script on a Gameobject in your scene.");
				}
				else
				{
					//Init event manager
					eventManager.Init();
				}
			}
			//Return event manager
			return eventManager;
		}
	}

	void Init()
	{
		if(eventDictionary == null)
		{
			//Init event dictionary
			eventDictionary = new Dictionary<string, UnityEvent>();
		}
	}

	public static void StartListening(string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		//See if in dictionary
		if(instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new UnityEvent();
			thisEvent.AddListener(listener);
			instance.eventDictionary.Add(eventName, thisEvent);
		}
	}

	public static void StopListening(string eventName, UnityAction listener)
	{
		if(eventManager == null) return;
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue (eventName, out thisEvent))
		{
			thisEvent.RemoveListener(listener);
		}
	}

	public static void TriggerEvent (string eventName)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.Invoke();
		}
	}
}
