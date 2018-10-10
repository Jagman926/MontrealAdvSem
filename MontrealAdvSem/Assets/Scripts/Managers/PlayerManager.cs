using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        //Manager
        InputManager inputManager;
        LevelManager levelManager;
        UiManager uiManager;

        [Header("Block Types")]
        private GameObject playerSpawn;
        public GameObject blockDormant;

        [Header("Player Spawn Settings")]
        public float spawnTimer = 0;
        public float spawnTimeSeconds = 0;
        private bool spawnNewPlayer;

        [Header("Player Container")]
        public GameObject currPlayer;
        public List<GameObject> BlockList;
        public Queue<GameObject> BlockQueue;

        void Start()
        {
            //Manager instance
            inputManager = Managers.InputManager.Instance;
            levelManager = Managers.LevelManager.Instance;
            uiManager = Managers.UiManager.Instance;
            //Get player spawn
            playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
            //Load block queue
            LoadBlockQueue();
            //Set spawn new player to false
            spawnNewPlayer = false;
        }

        void Update()
        {
            UpdateSpawnTiming();
        }

        void LoadBlockQueue()
        {
            BlockQueue = new Queue<GameObject>();
            for (int i = 0; i < BlockList.Count; i++)
            {
                BlockQueue.Enqueue(BlockList[i]);
            }
        }

        void UpdateSpawnTiming()
        {
            //Update spawn timer
            UpdateSpawnTimer();

            //Check if new player needs to spawn
            if (inputManager.playerSpawn || spawnNewPlayer)
            {
                //Set currPlayer properties before switch player
                if (currPlayer != null)
                {
                    DormantCurrPlayer();
                }
                //Instatiate new player
                InstantiatePlayer();
            }
        }

        void UpdateSpawnTimer()
        {
            //adjust timer
            spawnTimer -= Time.deltaTime;

            //If timer run out
            if (spawnTimer < 0)
            {
                spawnNewPlayer = true;
            }
        }

        void InstantiatePlayer()
        {
            if (BlockQueue.Count != 0)
            {
                //Instatiate Player
                currPlayer = Instantiate(BlockQueue.Peek(), playerSpawn.transform);
                //Reset spawn timer
                spawnTimer = spawnTimeSeconds;
                //Reset spawn bool
                spawnNewPlayer = false;
                //Set color to player color
                currPlayer.GetComponent<SpriteRenderer>().color = Color.green;
                //Update Block Queue UI
                uiManager.UpdateBlockQueue();
            }
            else
            {
                //Reset Level
                levelManager.LoadCurrentLevel();
            }
        }

        void DormantCurrPlayer()
        {
            //Remove current block from queue
            BlockQueue.Dequeue();
            //Remove PlayerMovement Code
            Destroy(currPlayer.GetComponent<PlayerMovement>());
            //Set Color to "dormant"
            currPlayer.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
