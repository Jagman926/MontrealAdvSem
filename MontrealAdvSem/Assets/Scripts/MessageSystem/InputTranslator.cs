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
		if(Input.GetKeyDown("q"))
		{
			EventManager.TriggerEvent("test");
		}
	}
}
