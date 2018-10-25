using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_InputButtons : MonoBehaviour 
{
	//Manager Instance
	Managers.GameManager gameManager;
	Managers.ScenesManager scenesManager;

	//Actions
	private Action<EventParam> aPauseGame;
    private Action<EventParam> aResetLevel;

    void Awake()
    {
		aPauseGame = new Action<EventParam>(PauseGame);
		aResetLevel = new Action<EventParam>(ResetLevel);
	}

	private void Start ()
	{
		gameManager = Managers.GameManager.Instance;
		scenesManager = Managers.ScenesManager.Instance;
	}

	void OnEnable()
	{
		Managers.EventManager.StartListening("PauseGame", aPauseGame);
		Managers.EventManager.StartListening("ResetLevel", aResetLevel);
	}

	void OnDisable()
	{
		Managers.EventManager.StopListening("PauseGame", aPauseGame);
		Managers.EventManager.StopListening("ResetLevel", aResetLevel);
	}

	void PauseGame(EventParam eventParam)
	{
		gameManager.PauseToggle();
	}

	void ResetLevel(EventParam eventParam)
	{
		scenesManager.LoadCurrentLevel();
	}
}
