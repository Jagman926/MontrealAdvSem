using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_LevelButtons : MonoBehaviour
{
    public void LoadLevel(string levelName)
    {
        EventParam eventParam = new EventParam();
        eventParam.stringParam = levelName;
        Managers.EventManager.TriggerEvent("LoadLevel", eventParam);
    }

    public void LoadCurrent()
    {
        EventParam eventParam = new EventParam();
        Managers.EventManager.TriggerEvent("CurrentLevel", eventParam);
    }

    public void LoadNextLevel()
    {
        EventParam eventParam = new EventParam();
        Managers.EventManager.TriggerEvent("NextLevel", eventParam);
    }

    public void LoadLastLevel()
    {
        EventParam eventParam = new EventParam();
        Managers.EventManager.TriggerEvent("LastLevel", eventParam);
    }
}
