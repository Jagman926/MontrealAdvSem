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
	    [Header("Curent Initials")]
	    public char[] currentInitials;

        [Header("Current Level Stats")]
        public bool levelCompleted;
        public float levelTotalTimer;
        public int levelRetries;

        private void Start()
        {
            scenesManager = Managers.ScenesManager.Instance;
            //Init initials
            currentInitials = new char[3];
            SetCurrentInitials('A', 'A', 'A');
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

        public void SetCurrentInitials(char char1, char char2, char char3)
        {
            currentInitials[0] = char1;
            currentInitials[1] = char2;
            currentInitials[2] = char3;
        }
    }
}
