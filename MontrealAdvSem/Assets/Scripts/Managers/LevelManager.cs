using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
		//Manager
		GUIManager guiManager;

		[Header("Scene List")]
		[SerializeField]	
		private List<string> levelList;
		
		//Current Scene
		string currentLevel;
		int currentLevelNumber;

		public void Awake()
		{
			//Manager instances
			guiManager = Managers.GUIManager.Instance;

			//Set current level
			currentLevel = SceneManager.GetActiveScene().name;
			currentLevelNumber = levelList.IndexOf(currentLevel);			
		}

		public void SetCurrentLevel(string newLevel)
		{
			currentLevel = newLevel;		
		}

		public string GetCurrentLevel()
		{
			return currentLevel;
		}

		public void LoadCurrentLevel()
		{
			SceneManager.LoadScene(currentLevel);
		}

		public void LoadNextLevel()
		{
			//Make sure next level exists
			if(currentLevelNumber+1 < levelList.Count)
			{
				//Increment level
				currentLevelNumber++;
				//Load next level in list
				SceneManager.LoadScene(levelList[currentLevelNumber]);
			}
			else
			{
				//Reset current level
				currentLevelNumber = 0;
				//Return to start screen
				guiManager.LoadStartMenu();
			}
		}
	}
}
