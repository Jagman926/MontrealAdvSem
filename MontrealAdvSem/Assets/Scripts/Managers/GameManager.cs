using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        //Scenes
        private string testScene = "Test_scene";

        [Header("Objective Settings")]
        public List<GameObject> objectiveList;

        [Header("Current Scene")]
        public string currentScene;

        private void Start()
        {
            //Set current scene
            currentScene = testScene;
            //Load all objectives
            LoadObjectivesList();
        }

        private void Update()
        {
            //Check objectives
            CheckObjectives();
        }

        private void LoadObjectivesList()
        {
            foreach (GameObject objectiveObject in GameObject.FindGameObjectsWithTag("Objective"))
            {
                objectiveList.Add(objectiveObject);
            }
        }

        private void CheckObjectives()
        {
            //If no objective are left
            if (objectiveList.Count == 0)
            {
                //Display win
                Debug.Log("YOU COLLECTED ALL OBJECTIVES");
                //Reset Level
                ResetScene();
            }
        }

        public void ResetScene()
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
