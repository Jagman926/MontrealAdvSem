using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

namespace Managers
{
    public class UiManager : Singleton<UiManager>
    {
        //Listeners
        private UnityAction mUpdateBlockQueueListener;

        //Managers
        GameManager gameManager;
        ScenesManager scenesManager;
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
        public Sprite blankSprite;
        private List<Image> BlockQueueUI;
        private int BlockQueueSize;

        [Header("Pause UI")]
        [SerializeField]
        private GameObject pauseMenu;
        [SerializeField]
        private Button returnButton;

        [Header("End Level UI")]
        [SerializeField]
        private GameObject endLevelMenu;
        [SerializeField]
        private Button nextLevelButton;
        [SerializeField]
        private TextMeshProUGUI retryCount;
        [SerializeField]
        private TextMeshProUGUI levelTotalTime;
        [SerializeField]
        private TextMeshProUGUI levelTime;

        [Header("Highscore UI")]
        [SerializeField]
        private string initials;
        [SerializeField]
        private TextMeshProUGUI totalInit_1, totalInit_2, totalInit_3;
        [SerializeField]
        private TextMeshProUGUI lastInit_1, lastInit_2, lastInit_3;
        [SerializeField]
        private TextMeshProUGUI totalHigh_1, totalHigh_2, totalHigh_3;
        [SerializeField]
        private TextMeshProUGUI lastHigh_1, lastHigh_2, lastHigh_3;

        [Header("Highscore Menu UI")]
        [SerializeField]
        private GameObject highscoreMenu;
        [SerializeField]
        private Button continueButton;



        private void Start()
        {
            //Manager
            levelManager = Managers.LevelManager.Instance;
            playerManager = Managers.PlayerManager.Instance;
            gameManager = Managers.GameManager.Instance;
            scenesManager = Managers.ScenesManager.Instance;
        }

        private void Update()
        {
            if (!levelManager.isPaused && !gameManager.levelCompleted)
            {
                UpdateSpawnTimer();
                UpdatePlayerCount();
                UpdateObjectivesCollected();
            }
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
                //Set text to spawn timer
                spawnTimerText.text = playerManager.spawnTimer.ToString("F2");
            }
            else
            {
                //Reset color
                spawnTimerText.faceColor = Color.white;
                //Set text to spawn timer
                spawnTimerText.text = playerManager.spawnTimer.ToString("F0");
            }
        }

        public void UpdateBlockQueue()
        {
            if (BlockQueueUI == null)
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
                    //Load block colors
                    if (i < BlockQueueSize - 1)
                    {
                        BlockQueueUI[i].sprite = playerManager.BlockQueue.ToArray()[i].GetComponent<SpriteRenderer>().sprite;
                    }
                    else
                    {
                        BlockQueueUI[i].sprite = blankSprite;
                    }
                }
            }
        }

        void UpdatePlayerCount()
        {
            playerCountText.text = (playerManager.BlockQueue.Count - 1).ToString();
            //Warning for 0 left
            if (playerManager.BlockQueue.Count - 1 == 0)
            {
                playerCountText.color = spawnTimerWarningColor;
            }
        }

        void UpdateObjectivesCollected()
        {
            objectivesCollectedText.text = (levelManager.collectedObjectives + "/" + levelManager.maxObjectives).ToString();
        }

        public void PauseGame()
        {
            //Reset Pause Menu
            pauseMenu.transform.localScale = Vector3.one;
            //Activate menu
            pauseMenu.SetActive(true);
            //Tween Menu
            pauseMenu.transform.DOScale(Vector3.zero, 0.25f).From();
            //Pause Buffer
            StartCoroutine(PauseBuffer());
            //Set return as selected button
            returnButton.Select();
        }

        public void ResumeGame()
        {
            //Reset Pause Menu
            pauseMenu.transform.localScale = Vector3.one;
            //Deactivate select
            EventSystem.current.SetSelectedGameObject(null);
            //Deactivate menu
            pauseMenu.SetActive(false);
            StartCoroutine(PauseBuffer());
        }

        IEnumerator PauseBuffer()
        {
            //Wait
            yield return new WaitForSeconds(0.1f);
        }

        IEnumerator SelectButtonWait(float seconds, Button button)
        {
            //Wait
            yield return new WaitForSeconds(seconds);
            //Set Next level as selected button
            button.Select();
        }

        public void EndLevelMenu()
        {
            //Activate menu
            endLevelMenu.SetActive(true);
            //Tween Menu
            endLevelMenu.transform.DOScale(Vector3.zero, 0.25f).From();
            //Select button wait
            StartCoroutine(SelectButtonWait(0.5f, nextLevelButton));
            //Update level menu text
            UpdateEndLevelMenu();
        }

        private void UpdateEndLevelMenu()
        {
            //Retrieve Data from Game Manager
            retryCount.text = gameManager.levelRetries.ToString();
            levelTime.text = levelManager.levelTimer.ToString("F2");
            //If no retries than set total time to level time
            if (gameManager.levelRetries == 0)
            {
                gameManager.levelTotalTimer = levelManager.levelTimer;
            }
            levelTotalTime.text = gameManager.levelTotalTimer.ToString("F2");
            //Update Highscores
            if (levelManager.newTotalHighscore || levelManager.newLastHighscore)
            {
                UpdateHighscores();
            }
            //Set Highscore table
            SetHighscoreUI();
        }

        private void UpdateHighscores()
        {
            if (levelManager.newTotalHighscore)
            {
                for (int i = 0; i < 3; i++)
                {
                    if(gameManager.levelTotalTimer < scenesManager.currentLevel.totalLevelHighscores_values[i] || scenesManager.currentLevel.totalLevelHighscores_values[i] == 0)
                    {
                        scenesManager.currentLevel.totalLevelHighscores_values.Insert(i, gameManager.levelTotalTimer);
                        scenesManager.currentLevel.totalLevelHighscores_values.RemoveAt(scenesManager.currentLevel.totalLevelHighscores_values.Count - 1);
                        scenesManager.currentLevel.totalLevelHighscores_Initials.Insert(i, gameManager.GetCurrentInitials());
                        scenesManager.currentLevel.totalLevelHighscores_Initials.RemoveAt(scenesManager.currentLevel.totalLevelHighscores_Initials.Count - 1);
                        break;
                    }
                }
            }
            if (levelManager.newLastHighscore)
            {
                for (int i = 0; i < 3; i++)
                {
                    if(levelManager.levelTimer < scenesManager.currentLevel.lastLevelHighscores_values[i] || scenesManager.currentLevel.lastLevelHighscores_values[i] == 0)
                    {
                        scenesManager.currentLevel.lastLevelHighscores_values.Insert(i, levelManager.levelTimer);
                        scenesManager.currentLevel.lastLevelHighscores_values.RemoveAt(scenesManager.currentLevel.lastLevelHighscores_values.Count - 1);
                        scenesManager.currentLevel.lastLevelHighscores_Initials.Insert(i, gameManager.GetCurrentInitials());
                        scenesManager.currentLevel.lastLevelHighscores_Initials.RemoveAt(scenesManager.currentLevel.lastLevelHighscores_Initials.Count - 1);
                        break;
                    }
                }
            }
        }

        private void SetHighscoreUI()
        {
            //Total Initials
            totalInit_1.text = scenesManager.currentLevel.totalLevelHighscores_Initials[0];
            totalInit_2.text = scenesManager.currentLevel.totalLevelHighscores_Initials[1];
            totalInit_3.text = scenesManager.currentLevel.totalLevelHighscores_Initials[2];
            //Total Highscores
            totalHigh_1.text = scenesManager.currentLevel.totalLevelHighscores_values[0].ToString("F2");
            totalHigh_2.text = scenesManager.currentLevel.totalLevelHighscores_values[1].ToString("F2");
            totalHigh_3.text = scenesManager.currentLevel.totalLevelHighscores_values[2].ToString("F2");
            ///Last Initials
            lastInit_1.text = scenesManager.currentLevel.lastLevelHighscores_Initials[0];
            lastInit_2.text = scenesManager.currentLevel.lastLevelHighscores_Initials[1];
            lastInit_3.text = scenesManager.currentLevel.lastLevelHighscores_Initials[2];
            //Last Highscores
            lastHigh_1.text = scenesManager.currentLevel.lastLevelHighscores_values[0].ToString("F2");
            lastHigh_2.text = scenesManager.currentLevel.lastLevelHighscores_values[1].ToString("F2");
            lastHigh_3.text = scenesManager.currentLevel.lastLevelHighscores_values[2].ToString("F2");
        }

        public void HighscoreMenu()
        {
            //Activate menu
            highscoreMenu.SetActive(true);
            //Tween Menu
            highscoreMenu.transform.DOScale(Vector3.zero, 0.25f).From();
            //Select button wait
            StartCoroutine(SelectButtonWait(0.5f, continueButton));
        }

        public void HighscoreToEndLevelTransition()
        {
            //Reset Pause Menu
            highscoreMenu.transform.localScale = Vector3.one;
            //Deactivate select
            EventSystem.current.SetSelectedGameObject(null);
            //Deactivate menu
            highscoreMenu.SetActive(false);
            StartCoroutine(PauseBuffer());
            //Call end level menu
            EndLevelMenu();
        }
    }
}
