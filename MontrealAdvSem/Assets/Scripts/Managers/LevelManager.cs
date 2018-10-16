using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
		//Listeners
		private UnityAction mResetLevelListener;

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
			//Listeners
			mResetLevelListener = new UnityAction (LoadCurrentLevel);
			//Manager instances
			guiManager = Managers.GUIManager.Instance;

			//Set current level
			currentLevel = SceneManager.GetActiveScene().name;
			currentLevelNumber = levelList.IndexOf(currentLevel);			
		}

	    void OnEnable()
	    {
		    EventManager.StartListening("ResetLevel", mResetLevelListener);
	    }

	    void OnDisable()
	    {
		    EventManager.StopListening("ResetLevel", mResetLevelListener);
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
