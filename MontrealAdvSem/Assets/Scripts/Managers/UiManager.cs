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
        private List<Image> BlockQueueUI;
        private int BlockQueueSize;

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
            //Load BlockQueue UI
            LoadBlockQueueUI();
        }
        
        private void Update()
        {
            UpdateSpawnTimer();
            UpdatePlayerCount();
            UpdateObjectivesCollected();
        }

        void LoadBlockQueueUI()
        {
            //Init list
            BlockQueueUI = new List<Image>();
            //Get parent object
            GameObject blockQueueObject = GameObject.Find("BlockQueue_UI");
            //Iterate through children
            foreach (Transform child in blockQueueObject.transform)
            {
                //Load image of object
                BlockQueueUI.Add(child.GetComponent<Image>());
            }
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

        public void UpdateBlockQueue()
        {
            //Set to new size
            BlockQueueSize = playerManager.BlockQueue.Count;
            for (int i = 0; i < BlockQueueUI.Count; i++)
            {
                if(i < BlockQueueSize)
                {
                    BlockQueueUI[i].color = playerManager.BlockQueue.ToArray()[i].GetComponent<SpriteRenderer>().color;
                }
                else
                {
                    BlockQueueUI[i].color = Color.white;
                }
            }
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
