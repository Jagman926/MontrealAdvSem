using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_InputButtons : MonoBehaviour 
{
	//Manager Instance
	Managers.GameManager gameManager;
	Managers.LevelManager levelManager;
	Managers.ScenesManager scenesManager;

	//Actions
	private Action<EventParam> aPauseToggle;
    private Action<EventParam> aResetLevel;

    void Awake()
    {
		aPauseToggle = new Action<EventParam>(PauseToggle);
		aResetLevel = new Action<EventParam>(ResetLevel);
	}

	private void Start ()
	{
		gameManager = Managers.GameManager.Instance;
		levelManager = Managers.LevelManager.Instance;
		scenesManager = Managers.ScenesManager.Instance;
	}

	void OnEnable()
	{
		Managers.EventManager.StartListening("PauseToggle", aPauseToggle);
		Managers.EventManager.StartListening("ResetLevel", aResetLevel);
	}

	void OnDisable()
	{
		Managers.EventManager.StopListening("PauseGame", aPauseToggle);
		Managers.EventManager.StopListening("ResetLevel", aResetLevel);
	}

	void PauseToggle(EventParam eventParam)
	{
		levelManager.PauseToggle();
	}

	void ResetLevel(EventParam eventParam)
	{
		//Increment level retries count
		gameManager.levelRetries++;
		scenesManager.LoadCurrentLevel();
	}
}
