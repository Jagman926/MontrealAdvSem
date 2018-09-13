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

        [Header("Current Scene")]
        public string currentScene;

        private void Start()
        {
            currentScene = testScene;
        }

        public void ResetScene()
        {
            SceneManager.LoadScene(currentScene);
        }
    }
}
