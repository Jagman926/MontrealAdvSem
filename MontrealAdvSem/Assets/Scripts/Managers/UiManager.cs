using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        //Listeners
        private UnityAction mUpdateBlockQueueListener;

        //Managers
        PlayerManager playerManager;
        LevelManager levelManager;

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
            //Manager
            levelManager = Managers.LevelManager.Instance;
            playerManager = Managers.PlayerManager.Instance;
        }
        
        private void Update()
        {
            UpdateSpawnTimer();
            UpdatePlayerCount();
            UpdateObjectivesCollected();
        }

        public void LoadBlockQueueUI()
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
            if(BlockQueueUI == null)
            {
                //Load block queue
                LoadBlockQueueUI();
            }
            else
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
        }

        void UpdatePlayerCount()
        {
            playerCountText.text = (playerManager.BlockQueue.Count - 1).ToString();
        }

        void UpdateObjectivesCollected()
        {
            objectivesCollectedText.text = (levelManager.collectedObjectives + "/" + levelManager.maxObjectives).ToString();
        }

		public void PauseGame()
		{
			//Activate menu
			pauseMenu.SetActive(true);
			StartCoroutine (PauseBuffer());
			//Set return as selected button
			returnButton.Select();
			//Set pause check to false
			playerManager.GetComponent<PlayerMovement>().pauseCheck = false;
		}

		public void ResumeGame()
		{
			//Deactivate select
			EventSystem.current.SetSelectedGameObject(null);
			//Deactivate menu
			pauseMenu.SetActive(false);
			StartCoroutine (PauseBuffer());
		}

		IEnumerator PauseBuffer()
    	{
		    //Wait
            yield return new WaitForSeconds(0.1f);
		    //Set pause check to true
		    playerManager.GetComponent<PlayerMovement>().pauseCheck = true;
     	}
    }
}
