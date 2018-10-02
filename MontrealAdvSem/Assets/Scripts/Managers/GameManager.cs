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

        [Header("Level Settings")]

        [Header("Objective Settings")]
        public List<GameObject> objectiveList;
        public int maxObjectives;
        public int collectedObjectives;

        private void Awake()
        {
            //Manager instances
            levelManager = Managers.LevelManager.Instance;
        }

        private void Start()
        {
            //Load all objectives
            LoadObjectivesList();
        }

        private void Update()
        {
            //Check objectives
            CheckObjectives();
            //Reset level when pressed
            if(Managers.InputManager.Instance.levelReset)
            {
                levelManager.LoadCurrentLevel();
            }
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
    }
}
