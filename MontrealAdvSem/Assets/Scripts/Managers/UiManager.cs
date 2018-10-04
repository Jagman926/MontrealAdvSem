using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        //Managers
        PlayerManager playerManager;
        GameManager gameManager;
        InputManager inputManager;

        [Header("Spawn Timer")]
        public TextMeshProUGUI spawnTimerText;
        public float spawnTimerWarning = 0;
        public Color spawnTimerWarningColor;

        [Header("Objective Count")]
        public TextMeshProUGUI objectivesCollectedText;

        [Header("Player Count")]
        public TextMeshProUGUI playerCountText;

        [Header("Pause UI")]
		[SerializeField]
		private GameObject pauseMenu;
		[SerializeField]
		private Button returnButton;

        private void Start()
        {
            //Managers
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

		public void PauseMenu()
		{
			//Activate menu
			pauseMenu.SetActive(true);
			StartCoroutine (Pause());
			//Set return as selected button
			returnButton.Select();
			//Set pause check to false
			Managers.InputManager.Instance.pauseCheck = false;
		}

		public void ResumeGame()
		{
			//Deactivate select
			EventSystem.current.SetSelectedGameObject(null);
			//Deactivate menu
			pauseMenu.SetActive(false);
			StartCoroutine (Pause());
		}

		IEnumerator Pause()
    	{
		//Wait
        yield return new WaitForSeconds(0.1f);
		//Set pause check to true
		Managers.InputManager.Instance.pauseCheck = true;
     	}
    }
}
