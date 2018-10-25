using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_SceneButtons : MonoBehaviour 
{
	public void LoadStartMenu()
	{
		EventParam eventParam = new EventParam();
		Managers.EventManager.TriggerEvent("StartMenu", eventParam);
	}

	public void LoadOptionsMenu()
	{
		EventParam eventParam = new EventParam();
		Managers.EventManager.TriggerEvent("OptionsMenu", eventParam);
	}

	public void LoadLevelMenu()
	{
		EventParam eventParam = new EventParam();
		Managers.EventManager.TriggerEvent("LevelMenu", eventParam);
	}

	public void ExitGame()
	{
		EventParam eventParam = new EventParam();
		Managers.EventManager.TriggerEvent("ExitGame", eventParam);
	}
}

