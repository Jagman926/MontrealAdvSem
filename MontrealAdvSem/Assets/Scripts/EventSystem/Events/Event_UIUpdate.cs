using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_UIUpdate : MonoBehaviour 
{
	//Manager Instance
	Managers.UiManager uiManager;

	//Actions
	private Action<EventParam> aUpdateBlockQueue;
	private Action<EventParam> aPauseGame;
	private Action<EventParam> aResumeGame;
	private Action<EventParam> aEndLevel;

	private void Awake()
	{
		aUpdateBlockQueue = new Action<EventParam>(UpdateBlockQueue);
		aPauseGame = new Action<EventParam>(PauseGame);
		aResumeGame = new Action<EventParam>(ResumeGame);
		aEndLevel = new Action<EventParam>(EndLevel);
	}

	private void Start()
	{
		uiManager = Managers.UiManager.Instance;
	}

	void OnEnable()
	{
        Managers.EventManager.StartListening("UpdateBlockQueue", aUpdateBlockQueue);
		Managers.EventManager.StartListening("PauseGame", aPauseGame);
		Managers.EventManager.StartListening("ResumeGame", aResumeGame);
		Managers.EventManager.StartListening("EndLevel", aEndLevel);
	}

	void OnDisable()
	{
        Managers.EventManager.StopListening("UpdateBlockQueue", aUpdateBlockQueue);
		Managers.EventManager.StopListening("PauseGame", aPauseGame);
		Managers.EventManager.StopListening("ResumeGame", aResumeGame);
		Managers.EventManager.StopListening("EndLevel", aEndLevel);
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

	void EndLevel(EventParam eventParam)
	{
		uiManager.EndLevelMenu();
	}
}
