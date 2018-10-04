using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        //Manager
        LevelManager levelManager;
        InputManager inputManager;
        UiManager uiManager;

        [Header("Level Settings")]
        public bool isPaused;

        [Header("Objective Settings")]
        public List<GameObject> objectiveList;
        public int maxObjectives;
        public int collectedObjectives;

        private void Awake()
        {
            //Manager instances
            levelManager = Managers.LevelManager.Instance;
            inputManager = Managers.InputManager.Instance;
            uiManager = Managers.UiManager.Instance;
        }

        private void Start()
        {
            //Load all objectives
            LoadObjectivesList();
            //Set pause
            isPaused = false;
            //Set timescale
            Time.timeScale = 1;
        }

        private void Update()
        {
            //Check level inputs
            CheckLevelInputs();
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
        }

        private void CheckLevelInputs()
        {
            //Pause level when pressed
            if(inputManager.levelPause)
            {
                PauseGame();
            }
            //Reset level when pressed
            if(inputManager.levelReset)
            {
                levelManager.LoadCurrentLevel();
            }
        }

        public void PauseGame()
        {
            //Switch for bool
            isPaused = !isPaused;
            if(isPaused)
            {
                //Pause frames
                Time.timeScale = 0;
                uiManager.PauseMenu();
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
