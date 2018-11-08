using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        //Manager
        LevelManager levelManager;
        InputManager inputManager;
        ScenesManager scenesManager;
        BlockType blockType;

        //Script references
        PlayerMovement playerMovementScript;
        ZoneCheck playerZoneCheckScript;

        [Header("Block Types")]
        private GameObject playerSpawn;

        [Header("Player Spawn Settings")]
        public Color playerColor;
        public Sprite playerSprite;
        [HideInInspector]
        public float spawnTimer = 0;
        public float spawnTimeSeconds = 0;
        private bool spawnNewPlayer;

        [Header("Player Container")]
        public GameObject currPlayer;
        private List<BlockType.BlockTypes> BlockList;
        public Queue<GameObject> BlockQueue;

        void Start()
        {
            //Manager instance
            levelManager = Managers.LevelManager.Instance;
            inputManager = Managers.InputManager.Instance;
            scenesManager = Managers.ScenesManager.Instance;
            //Script reference
            playerMovementScript = GetComponent<PlayerMovement>();
            blockType = GetComponent<BlockType>();
            //Get player spawn
            playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
            //Load block queue
            BlockList = scenesManager.currentLevel.levelBlockOrder;
            LoadBlockQueue();
            //Set spawn new player to false
            spawnNewPlayer = false;
        }

        void Update()
        {
            if (!levelManager.isPaused)
            {
                UpdateSpawnTiming();
            }
        }

        void LoadBlockQueue()
        {
            BlockQueue = new Queue<GameObject>();
            for (int i = 0; i < BlockList.Count; i++)
            {
                BlockQueue.Enqueue(blockType.ReturnBlockType(BlockList[i]));
            }
            //Add final block
            BlockQueue.Enqueue(blockType.ReturnBlockType(BlockType.BlockTypes.DORMANT));
            //Update Block Queue
            EventParam eventParam = new EventParam();
            Managers.EventManager.TriggerEvent("UpdateBlockQueue", eventParam);
        }

        void UpdateSpawnTiming()
        {
            if (currPlayer == null)
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
            if (spawnTimer > 0.0f)
            {
                if (!playerZoneCheckScript.inNoDormantZone)
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
                //Set sprite to player sprite
                currPlayer.GetComponent<SpriteRenderer>().sprite = playerSprite;
                //Update Block Queue UI
                EventParam eventParam = new EventParam();
                Managers.EventManager.TriggerEvent("UpdateBlockQueue", eventParam);
            }
            else
            {
                //Reset Level
                EventParam eventParam = new EventParam();
                Managers.EventManager.TriggerEvent("ResetLevel", eventParam);
            }
        }

        void DormantCurrPlayer()
        {
            //Remove current block from queue
            BlockQueue.Dequeue();
            //Set Color to "dormant"
            currPlayer.GetComponent<SpriteRenderer>().color = Color.white;
            //Play Dormant sound
            EventParam eventParam = new EventParam();
            Managers.EventManager.TriggerEvent("PlayLandSound", eventParam);
        }
    }
}
