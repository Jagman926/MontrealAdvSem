using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        //Managers
        PlayerManager playerManager;
        GameManager gameManager;

        [Header("Spawn Timer")]
        public TextMeshProUGUI spawnTimerText;
        public float spawnTimerWarning = 0;
        public Color spawnTimerWarningColor;

        [Header("Objective Count")]
        public TextMeshProUGUI objectivesCollectedText;

        [Header("Player Count")]
        public TextMeshProUGUI playerCountText;

        private void Start()
        {
            playerManager = Managers.PlayerManager.Instance;
            gameManager = Managers.GameManager.Instance;
        }

        private void Update()
        {
            UpdateSpawnTimer();
            UpdatePlayerCount();
            UpdateObjectivesCollected();
        }

        void UpdateSpawnTimer()
        {
            if (playerManager.spawnTimer <= spawnTimerWarning)
            {
                //Set text to warning color
                spawnTimerText.faceColor = spawnTimerWarningColor;
            }
            else
            {
                //Reset color
                spawnTimerText.faceColor = Color.white;
            }
            //Set text to spawn timer
            spawnTimerText.text = playerManager.spawnTimer.ToString("F2");
        }

        void UpdatePlayerCount()
        {
            playerCountText.text = (playerManager.BlockQueue.Count - 1).ToString();
        }

        void UpdateObjectivesCollected()
        {
            objectivesCollectedText.text = (gameManager.collectedObjectives + "/" + gameManager.maxObjectives).ToString();
        }
    }
}
