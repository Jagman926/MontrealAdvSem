using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_LevelButtons : MonoBehaviour 
{
    //Manager Instance
    Managers.ScenesManager scenesManager;
    Managers.GameManager gameManager;

    //Actions
    private Action<EventParam> aLoadLevel;
    private Action<EventParam> aCurrentLevel;
    private Action<EventParam> aNextLevel;
    private Action<EventParam> aLastLevel;

    void Awake()
    {
        //Init Listeners
        aLoadLevel = new Action<EventParam>(LoadLevel);
        aCurrentLevel = new Action<EventParam>(LoadCurrent);
        aNextLevel = new Action<EventParam>(NextLevel);
        aLastLevel = new Action<EventParam>(LastLevel);
    }

    private void Start()
    {
        scenesManager = Managers.ScenesManager.Instance;
        gameManager = Managers.GameManager.Instance;
    }

    void OnEnable()
    {
        //Register With Action variable
        Managers.EventManager.StartListening("LoadLevel", aLoadLevel);
        Managers.EventManager.StartListening("CurrentLevel", aCurrentLevel);
        Managers.EventManager.StartListening("NextLevel", aNextLevel);
        Managers.EventManager.StartListening("LastLevel", aLastLevel);
    }

    void OnDisable()
    {
        //Un-Register With Action variable
        Managers.EventManager.StopListening("LoadLevel", aLoadLevel);
        Managers.EventManager.StopListening("CurrentLevel", aCurrentLevel);
        Managers.EventManager.StopListening("NextLevel", aNextLevel);
        Managers.EventManager.StopListening("LastLevel", aLastLevel);
    }

    void LoadLevel(EventParam eventParam)
    {
        scenesManager.LoadLevel(eventParam.stringParam);
    }

    void LoadCurrent(EventParam eventParam)
    {
        scenesManager.LoadCurrentLevel();
    }

    void NextLevel(EventParam eventParam)
    {
        scenesManager.LoadNextLevel();
    }

    void LastLevel(EventParam eventParam)
    {
        scenesManager.LoadLastLevel();
    }
}
