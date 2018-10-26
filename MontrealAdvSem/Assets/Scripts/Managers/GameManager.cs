using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        //Manager instances
        ScenesManager scenesManager;

        [Header("Current Level Stats")]
        public bool levelCompleted;
        public float levelTotalTimer;
        public int levelRetries;

        private void Start()
        {
            scenesManager = Managers.ScenesManager.Instance;
        }

        private void Update()
        {
            if (scenesManager.inLevel)
            {
                if (!levelCompleted)
                {
                    UpdateTimers();
                }
            }
        }

        public void StartNewLevel()
        {
            if (levelCompleted)
            {
                levelTotalTimer = 0.0f;
                levelRetries = 0;
                levelCompleted = false;
            }
        }

        private void UpdateTimers()
        {
            levelTotalTimer += Time.deltaTime;
        }
    }
}
