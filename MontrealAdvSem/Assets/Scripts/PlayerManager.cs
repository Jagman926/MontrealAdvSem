using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : Singleton<PlayerManager>
    {

        [Header("Player Objects")]
        public GameObject playerObject;
        public GameObject playerSpawn;

        [Header("Current Player Settings")]
        private Rigidbody2D currPlayerRB;

        [Header("Dormant Player Settings")]
        public float dormantMass = 0;

        [Header("Player Container")]
        private GameObject currPlayer;
        public List<GameObject> playerObjects;

        void Start()
        {

        }

        void Update()
        {
            UpdateSpawn();
        }

        void UpdateSpawn()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                //Set currPlayer properties before switch player
                if (currPlayer != null)
                {
                    DormantCurrPlayer();
                }
                InstantiatePlayer();
            }
        }

        void InstantiatePlayer()
        {
            //Instatiate Player
            currPlayer = Instantiate(playerObject, playerSpawn.transform);
            currPlayerRB = currPlayer.GetComponent<Rigidbody2D>();
            //Add to list
            playerObjects.Add(currPlayer);
        }

        void DormantCurrPlayer()
        {
            //Unmovable
            currPlayer.GetComponent<PlayerMovement>().movable = false;
            //Act as ground (For jumping on)
            currPlayer.layer = 8;
            //Change mass
            currPlayerRB.mass = 5;
            //Unfreeze rotation
            currPlayerRB.constraints = RigidbodyConstraints2D.None;
            //Change color
            currPlayer.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
}
