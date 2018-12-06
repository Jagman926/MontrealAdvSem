using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        //Manager Instance
        GameManager gameManager;
        PlayerManager playerManager;
        ScenesManager scenesManager;

        [Header("Level Settings")]
        public float levelTimer;
        public bool isPaused;

        [Header("Objective Settings")]
        public List<GameObject> objectiveList;
        public int maxObjectives;
        public int collectedObjectives;

        [Header("Particles")]
        [SerializeField]
        private ParticleSystem endLevelParticles1;
        [SerializeField]
        private ParticleSystem endLevelParticles2;
        [SerializeField]
        private ParticleSystem endLevelParticles3;

        //Highscore tracker
        public bool newTotalHighscore;
        public bool newLastHighscore;

        private void Awake()
        {
            //Manager Instance
            gameManager = Managers.GameManager.Instance;
            scenesManager = Managers.ScenesManager.Instance;
            playerManager = Managers.PlayerManager.Instance;
        }

        private void Start()
        {
            //Set pause
            isPaused = false;
            //Set highscore bool
            newTotalHighscore = false;
            newLastHighscore = false;
            //Set levelTimer
            levelTimer = 0;
            //Load Objective List
            LoadObjectivesList();
            //Start new level GameManager
            gameManager.StartNewLevel();
        }

        private void Update()
        {
            if (!isPaused && !gameManager.levelCompleted)
            {
                CheckObjectives();
            }
        }

        public void PauseToggle()
        {
            //Switch for bool
            isPaused = !isPaused;
            if (isPaused)
            {
                //Pause frames
                EventParam eventParam = new EventParam();
                Managers.EventManager.TriggerEvent("PauseGame", eventParam);
            }
            else
            {
                //Resume frames
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
                //Level Complete
                gameManager.levelCompleted = true;
                //End Level
                StartCoroutine(EndLevel());

            }
            else if (!isPaused && !gameManager.levelCompleted)
            {
                //Update level timer
                levelTimer += Time.deltaTime;
            }
        }

        IEnumerator EndLevel()
        {
            //Play cheer audio
            EventParam eventParam = new EventParam();
            Managers.EventManager.TriggerEvent("PlayCheerSound", eventParam);
            //Start Confetti!
            endLevelParticles1.Play();
            endLevelParticles2.Play();
            endLevelParticles3.Play();
            //Wait
            yield return new WaitForSeconds(3.0f);
            //Pause
            isPaused = true;
            //Check for highscore
            CheckForHighscore();
            //If new highscore
            if (newTotalHighscore || newLastHighscore)
            {
                //If new highscore, display highscore menu
                Managers.EventManager.TriggerEvent("Highscore", eventParam);
            }
            else
            {
                //Display Menu
                Managers.EventManager.TriggerEvent("EndLevel", eventParam);
            }
        }

        private void CheckForHighscore()
        {
            for (int i = 0; i < 3; i++)
            {
                //If highscore is lower or there is no highscore
                if (!newTotalHighscore)
                {
                    if (gameManager.levelTotalTimer < scenesManager.currentLevel.totalLevelHighscores_values[i] || scenesManager.currentLevel.totalLevelHighscores_values[i] == 0.0f)
                    {
                        newTotalHighscore = true;
                    }
                }
                if (!newLastHighscore)
                {
                    if (levelTimer < scenesManager.currentLevel.lastLevelHighscores_values[i] || scenesManager.currentLevel.lastLevelHighscores_values[i] == 0.0f)
                    {
                        newLastHighscore = true;
                    }
                }
            }
        }
    }
}
