using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_UIUpdate : MonoBehaviour 
{
	//Manager Instance
	Managers.LevelManager levelManager;
	Managers.UiManager uiManager;

	//Actions
	private Action<EventParam> aUpdateBlockQueue;
	private Action<EventParam> aPauseGame;
	private Action<EventParam> aResumeGame;

	private void Awake()
	{
		aUpdateBlockQueue = new Action<EventParam>(UpdateBlockQueue);
		aPauseGame = new Action<EventParam>(PauseGame);
		aResumeGame = new Action<EventParam>(ResumeGame);
	}

	private void Start()
	{
		levelManager = Managers.LevelManager.Instance;
		uiManager = Managers.UiManager.Instance;
	}

	void OnEnable()
	{
        Managers.EventManager.StartListening("UpdateBlockQueue", aUpdateBlockQueue);
		Managers.EventManager.StartListening("PauseGame", aPauseGame);
		Managers.EventManager.StartListening("ResumeGame", aResumeGame);
	}

	void OnDisable()
	{
        Managers.EventManager.StopListening("UpdateBlockQueue", aUpdateBlockQueue);
		Managers.EventManager.StopListening("PauseGame", aPauseGame);
		Managers.EventManager.StopListening("ResumeGame", aResumeGame);
	}

	void UpdateBlockQueue(EventParam eventParam)
	{
		uiManager.UpdateBlockQueue();
	}

	void PauseGame(EventParam eventParam)
	{
		uiManager.PauseGame();
	}

	void ResumeGame(EventParam eventParam)
	{
		uiManager.ResumeGame();
	}
}
