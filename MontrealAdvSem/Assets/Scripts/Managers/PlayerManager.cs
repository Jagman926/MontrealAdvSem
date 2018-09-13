using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        //Manager
        InputManager inputManager;
        GameManager gameManager;

        [Header("Player Objects")]
        public GameObject playerObject;
        private GameObject playerSpawn;

        [Header("Current Player Settings")]
        private Rigidbody2D currPlayerRB;

        [Header("Dormant Player Settings")]
        public float dormantMass = 0;
        public int dormantLayer = 0;
        public PhysicsMaterial2D dormantMaterial;

        [Header("Player Spawn Settings")]
        public int maxSpawnCount = 0;
        public float spawnTimer = 0;
        public float spawnTimeSeconds = 0;
        private bool spawnNewPlayer;

        [Header("Player Container")]
        public GameObject currPlayer;
        public List<GameObject> playerList;

        void Start()
        {
            //Manager instance
            inputManager = Managers.InputManager.Instance;
            gameManager = Managers.GameManager.Instance;
            //Get player spawn
            playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
            //Set spawn new player to false
            spawnNewPlayer = false;
        }

        void Update()
        {
            UpdateSpawn();
        }

        void UpdateSpawn()
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
            if (playerList.Count != maxSpawnCount)
            {
                //Instatiate Player
                currPlayer = Instantiate(playerObject, playerSpawn.transform);
                currPlayerRB = currPlayer.GetComponent<Rigidbody2D>();
                //Add to list
                playerList.Add(currPlayer);
                //Reset spawn timer
                spawnTimer = spawnTimeSeconds;
                //Reset spawn bool
                spawnNewPlayer = false;
            }
            else
            {
                //Reset Level
                gameManager.ResetScene();
            }
        }

        void DormantCurrPlayer()
        {
            //Unmovable
            currPlayer.GetComponent<PlayerMovement>().movable = false;
            //Act as ground (For jumping on)
            currPlayer.layer = dormantLayer;
            //Change mass
            currPlayerRB.mass = dormantMass;
            //Unfreeze rotation
            currPlayerRB.constraints = RigidbodyConstraints2D.None;
            //Change color
            currPlayer.GetComponent<SpriteRenderer>().color = Color.black;
            //Change physics material
            currPlayer.GetComponent<Collider2D>().sharedMaterial = dormantMaterial;
        }
    }
}
