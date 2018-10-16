using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        //Manager
        InputManager inputManager;

        //Script references
        PlayerMovement playerMovementScript;
        ZoneCheck playerZoneCheckScript;

        [Header("Block Types")]
        private GameObject playerSpawn;
        public GameObject blockDormant;

        [Header("Player Spawn Settings")]
        public Color playerColor;
        [HideInInspector]
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
            //Script reference
            playerMovementScript = GetComponent<PlayerMovement>();
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
            EventManager.TriggerEvent("UpdateBlockQueue");
        }

        void UpdateSpawnTiming()
        {
            if(currPlayer == null)
            {
            //Spawn first player
            InstantiatePlayer();
            }
            //Check if new player needs to spawn
            if ((inputManager.playerSpawn || spawnNewPlayer) && !playerZoneCheckScript.inNoDormantZone)
            {
                //Set currPlayer properties before switch player
                if (currPlayer != null)
                {
                    DormantCurrPlayer();
                }
                //Instatiate new player
                InstantiatePlayer();
            }

            //Update spawn timer
            UpdateSpawnTimer();
        }

        void UpdateSpawnTimer()
        {
            //adjust timer
            if(spawnTimer > 0.0f)
            {
                if(!playerZoneCheckScript.inNoDormantZone)
                {
                    spawnTimer -= Time.deltaTime;
                }
            }
            //If timer run out
            else
            {
                spawnTimer = 0.0f;
                spawnNewPlayer = true;
            }
        }

        void InstantiatePlayer()
        {
            if (BlockQueue.Count != 0)
            {
                //Instatiate Player
                currPlayer = Instantiate(BlockQueue.Peek(), playerSpawn.transform);
                //Set movement script to curr player
                playerMovementScript.UpdateCurrentPlayer(currPlayer);
                //Set zone check script
                playerZoneCheckScript = currPlayer.GetComponent<ZoneCheck>();
                //Reset spawn timer
                spawnTimer = spawnTimeSeconds;
                //Reset spawn bool
                spawnNewPlayer = false;
                //Set color to player color
                currPlayer.GetComponent<SpriteRenderer>().color = playerColor;
                //Update Block Queue UI
                EventManager.TriggerEvent("UpdateBlockQueue");
            }
            else
            {
                //Reset Level
                EventManager.TriggerEvent("ResetLevel");
            }
        }

        void DormantCurrPlayer()
        {
            //Remove current block from queue
            BlockQueue.Dequeue();
            //Set Color to "dormant"
            currPlayer.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
