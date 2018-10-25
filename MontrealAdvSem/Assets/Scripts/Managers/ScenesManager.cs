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
		public bool inLevel;
		[SerializeField]
		private int currentLevelIndex;
		public Level_Data currentLevel;

		[Header("Menu Scenes")]
		[SerializeField]
		private string startScene;
		[SerializeField]
		private string optionsScene;
		[SerializeField]
		private string levelScene;

		private void Start()
		{
			//Load level master information
			LoadLevelMasterInformation();
			//Load menu Scene
			LoadNewScene(startScene);
		}

		private void Update()
		{
		
		}

		private void LoadLevelMasterInformation()
		{
			//If no previous current level, then set current level to first level
			if(levelMaster.currentLevel == null)
			{
				currentLevel = levelMaster.level_Datas[0];
			}
			//Load previous current level
			else
			{
			currentLevelIndex = levelMaster.level_Datas.IndexOf(levelMaster.currentLevel);
			}
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
			//Set inLevel
			inLevel = false;
		}

		public void LoadOptionsMenu()
		{
			//Load scene
			LoadNewScene(optionsScene);
			//Set inLevel
			inLevel = false;
		}

		public void LoadLevelMenu()
		{
			//Load scene
			LoadNewScene(levelScene);
			//Set inLevel
			inLevel = false;
		}

		public void ExitGame()
		{
			//Exit Application
			Application.Quit();
		}

//////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////       LEVEL LOADING          ////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////

		private void IncrementLevel()
		{
			if(currentLevelIndex + 1 < levelMaster.level_Datas.Count)
			{
				currentLevelIndex++;
				currentLevel = levelMaster.level_Datas[currentLevelIndex];
			}
			else
			{
				Debug.Log("Level Out of Bounds (Too High)");
			}
		}

		private void DecrementLevel()
		{
			if(currentLevelIndex - 1 >= 0)
			{
				currentLevelIndex--;
				currentLevel = levelMaster.level_Datas[currentLevelIndex];
			}
			else
			{
				Debug.Log("Level Out of Bounds (Too Low)");
			}
		}

		public void LoadLevel(string levelName)
		{
			LoadNewScene(levelName);
			//Set inLevel
			inLevel = true;
		}

		public void LoadCurrentLevel()
		{
			//Load Scene
			LoadNewScene(currentLevel.name);
			//Set inLevel
			inLevel = true;
		}

		public void LoadNextLevel()
		{
			IncrementLevel();
			//Load scene
			LoadNewScene(currentLevel.name);
			//Set inLevel
			inLevel = true;
		}

		public void LoadLastLevel()
		{
			DecrementLevel();
			//Load scene
			LoadNewScene(currentLevel.name);
			//Set inLevel
			inLevel = true;
		}
	}
}
