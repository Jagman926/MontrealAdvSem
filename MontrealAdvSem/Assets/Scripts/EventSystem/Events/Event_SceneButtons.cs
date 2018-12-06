using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_SceneButtons : MonoBehaviour
{
	//Manager Instance
	Managers.ScenesManager scenesManager;
    Managers.GameManager gameManager;

    //Actions
    private Action<EventParam> aStartMenu;
    private Action<EventParam> aOptionsMenu;
    private Action<EventParam> aLevelMenu;
    private Action<EventParam> aExitGame;

    void Awake()
    {
        //Init Listeners
        aStartMenu = new Action<EventParam>(StartMenu);
        aOptionsMenu = new Action<EventParam>(OptionsMenu);
        aLevelMenu = new Action<EventParam>(LevelMenu);
        aExitGame = new Action<EventParam>(ExitGame);
    }

	private void Start()
	{
		scenesManager = Managers.ScenesManager.Instance;
        gameManager = Managers.GameManager.Instance;
	}

    void OnEnable()
    {
        //Register With Action variable
        Managers.EventManager.StartListening("StartMenu", aStartMenu);
        Managers.EventManager.StartListening("OptionsMenu", aOptionsMenu);
        Managers.EventManager.StartListening("LevelMenu", aLevelMenu);
        Managers.EventManager.StartListening("ExitGame", aExitGame);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        Managers.EventManager.StopListening("StartMenu", aStartMenu);
        Managers.EventManager.StopListening("OptionsMenu", aOptionsMenu);
        Managers.EventManager.StopListening("LevelMenu", aLevelMenu);
        Managers.EventManager.StopListening("ExitGame", aExitGame);
    }

    void StartMenu(EventParam eventParam)
    {
		scenesManager.LoadStartMenu();
        gameManager.levelCompleted = true;
    }

    void OptionsMenu(EventParam eventParam)
    {
        scenesManager.LoadOptionsMenu();
    }

    void LevelMenu(EventParam eventParam)
    {
        scenesManager.LoadLevelMenu();
    }

    void ExitGame(EventParam eventParam)
    {
        scenesManager.ExitGame();
    }
}
