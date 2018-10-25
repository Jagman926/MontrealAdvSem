using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Managers
{
	public class ScenesManager : Singleton<ScenesManager> 
	{
		[Header("Scene Information")]
		[SerializeField]
		private string currentScene;

		[Header("Level Data")]
		public LEVEL_MASTER levelMaster;
		[SerializeField]
		private string currentLevel;
		[SerializeField]
		private string nextLevel;
		[SerializeField]
		private string lastLevel;

		[Header("Menu Scenes")]
		[SerializeField]
		private string startScene;
		[SerializeField]
		private string optionsScene;
		[SerializeField]
		private string levelScene;

		private void Start()
		{
			//Load menu Scene
			LoadNewScene(startScene);
		}

		private void Update()
		{
		
		}

		public void LoadNewScene(string sceneName)
		{
			//Unload previous Scene (if applicable)
			if(currentScene != "")
			{
				SceneManager.UnloadSceneAsync(currentScene);
			}
			//Load Scene sceneName
			SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
			//Set to current scene
			currentScene = sceneName;
		}

		public void LoadCurrentScene()
		{
			//Load current Scene (if applicable)
			if(currentScene != "")
			{
				//Load scene
				SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
			}
		}

		public void LoadStartMenu()
		{
			//Load scene
			LoadNewScene(startScene);
		}

		public void LoadOptionsMenu()
		{
			//Load scene
			LoadNewScene(optionsScene);
		}

		public void LoadLevelMenu()
		{
			//Load scene
			LoadNewScene(levelScene);
		}

		public void ExitGame()
		{
			//Exit Application
			Application.Quit();
		}

//////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////       LEVEL LOADING          ////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////

		public void LoadLevel(string levelName)
		{
			LoadNewScene(levelName);
		}

		public void LoadCurrentLevel()
		{
			//Load Scene
			LoadNewScene(currentLevel);
		}

		public void LoadNextLevel()
		{
			//Load scene
			LoadNewScene(nextLevel);
		}

		public void LoadLastLevel()
		{
			//Load scene
			LoadNewScene(lastLevel);
		}
	}
}
