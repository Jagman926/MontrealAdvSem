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
        public List<int> currentInitials;
        public List<char> currentCharInitials;
        //Character list
        private char[] charList;
        private string charString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        [Header("Current Level Stats")]
        public bool levelCompleted;
        public float levelTotalTimer;
        public int levelRetries;

        private void Start()
        {
            scenesManager = Managers.ScenesManager.Instance;
            //Init lists
            currentInitials = new List<int>();
            currentCharInitials = new List<char>();
            //Load char list
            charList = charString.ToCharArray();
            //Init initials
            InitCurrentInitials(0, 0, 0);
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

        public void InitCurrentInitials(int charNum1, int charNum2, int charNum3)
        {
            currentInitials.Add(charNum1);
            currentCharInitials.Add(charList[charNum1]);
            currentInitials.Add(charNum2);
            currentCharInitials.Add(charList[charNum2]);
            currentInitials.Add(charNum3);
            currentCharInitials.Add(charList[charNum3]);
        }

        public void SetCurrentInitials(int charNum1, int charNum2, int charNum3)
        {
            currentInitials[0] = charNum1;
            currentCharInitials[0] = charList[charNum1];
            currentInitials[1] = charNum2;
            currentCharInitials[1] = charList[charNum2];
            currentInitials[2] = charNum3;
            currentCharInitials[2] = charList[charNum3];
        }

        public string GetCurrentInitials()
        {
            string initials = new string(currentCharInitials.ToArray());
            return initials;
        }
    }
}
