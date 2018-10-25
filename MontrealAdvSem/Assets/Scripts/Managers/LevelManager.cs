using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        [Header("Level Settings")]
        public float levelTimer;
        public bool isPaused;

        [Header("Objective Settings")]
        public List<GameObject> objectiveList;
        public int maxObjectives;
        public int collectedObjectives;

        private void Start()
        {
            //Set pause
            isPaused = false;
            //Set timescale
            Time.timeScale = 1;
            //Set levelTimer
            levelTimer = 0.0f;
            //Load Objective List
            LoadObjectivesList();
        }

        private void Update()
        {
            CheckObjectives();
        }

        public void PauseToggle()
        {
            //Switch for bool
            isPaused = !isPaused;
            if (isPaused)
            {
                //Pause frames
                Time.timeScale = 0;
                EventParam eventParam = new EventParam();
                Managers.EventManager.TriggerEvent("PauseGame", eventParam);
            }
            else
            {
                //Resume frames
                Time.timeScale = 1;
                EventParam eventParam = new EventParam();
                Managers.EventManager.TriggerEvent("ResumeGame", eventParam);
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
                //Load next Level
                //ADD END LEVEL SCREEN HERE (maybe add event that triggers screen pop up)
            }
            else
            {
                //Update level timer
                levelTimer += Time.deltaTime;
            }
        }
    }
}
