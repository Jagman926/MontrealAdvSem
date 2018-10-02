using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Managers
{
    public class GUIManager : Singleton<GUIManager>
    {

		//Menus scenes
		[Header("Menu Scenes")]
		[SerializeField]
		private string startMenu;
		[SerializeField]
		private string optionsMenu;
		[SerializeField]
		private string levelMenu;
		
		public void PlayGame()
		{
			//Load next scene in build index
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);		
		}

		public void LoadStartMenu()
		{
			//Load start menu
			SceneManager.LoadScene(startMenu);
		}

		public void LoadOptionsMenu()
		{
			//Load options menu
			SceneManager.LoadScene(optionsMenu);
		}

		public void LoadLevelMenu()
		{
			//Load level menu
			SceneManager.LoadScene(levelMenu);
		}

		public void ExitGame()
		{
			//Exit game
			Application.Quit();			
		}
	}
}
