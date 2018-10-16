using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTranslator : MonoBehaviour 
{
	void Update () 
	{
		CheckInputs();
	}

	private void CheckInputs()
	{
		if(Input.GetButtonDown("Pause"))
		{
			EventManager.TriggerEvent("PauseGame");
		}
		if(Input.GetButtonDown("Reset"))
		{
			EventManager.TriggerEvent("ResetLevel");
		}
	}
}
