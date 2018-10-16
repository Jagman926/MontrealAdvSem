using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        //Listeners
        private UnityAction mPauseGameListener;

        //Manager
        LevelManager levelManager;
        UiManager uiManager;

        [Header("Level Settings")]
        public bool isPaused;
        public float levelTimer;

        [Header("Objective Settings")]
        public List<GameObject> objectiveList;
        public int maxObjectives;
        public int collectedObjectives;

	    void Awake()
	    {
            //Listeners
		    mPauseGameListener = new UnityAction (PauseToggle);
            //Manager instances
            levelManager = Managers.LevelManager.Instance;
            uiManager = Managers.UiManager.Instance;
	    }

	    void OnEnable()
	    {
		    EventManager.StartListening("PauseGame", mPauseGameListener);
	    }

	    void OnDisable()
	    {
		    EventManager.StopListening("PauseGame", mPauseGameListener);
        }

        private void Start()
        {
            //Load all objectives
            LoadObjectivesList();
            //Set pause
            isPaused = false;
            //Set timescale
            Time.timeScale = 1;
            //Set levelTimer
            levelTimer = 0.0f;
        }

        private void Update()
        {
            //Check objectives
            CheckObjectives();
        }

        private void LoadObjectivesList()
        {
            //Add all objectives on map into list
            foreach (GameObject objectiveObject in GameObject.FindGameObjectsWithTag("Objective"))
            {
                objectiveList.Add(objectiveObject);
            }
            //Set max objectives
            maxObjectives = objectiveList.Count;
        }

        private void CheckObjectives()
        {
            //update objects collected
            collectedObjectives = (maxObjectives - objectiveList.Count);
            //If no objective are left
            if (objectiveList.Count == 0)
            {
                //Display win
                Debug.Log("YOU COLLECTED ALL OBJECTIVES");
                //Reset Level
                levelManager.LoadNextLevel();
            }
            else
            {
                //Update level timer
                levelTimer += Time.deltaTime;
            }
        }

        public void PauseToggle()
        {
            //Switch for bool
            isPaused = !isPaused;
            if(isPaused)
            {
                //Pause frames
                Time.timeScale = 0;
                uiManager.PauseGame();
            }
            else
            {
                //Resume frames
                Time.timeScale = 1;
                uiManager.ResumeGame();
            }
        }
    }
}
