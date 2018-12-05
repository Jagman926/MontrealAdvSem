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
	    public int[] currentInitials;

        [Header("Current Level Stats")]
        public bool levelCompleted;
        public float levelTotalTimer;
        public int levelRetries;

        private void Start()
        {
            scenesManager = Managers.ScenesManager.Instance;
            //Init initials
            currentInitials = new int[3];
            SetCurrentInitials(0, 0, 0);
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

        public void SetCurrentInitials(int charNum1, int charNum2, int charNum3)
        {
            currentInitials[0] = charNum1;
            currentInitials[1] = charNum2;
            currentInitials[2] = charNum3;
        }
    }
}
