using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public bool pauseCheck;
        [Header("Player Control Booleans")]
        public bool playerJump;
        public bool playerSpawn;
        public bool playerAction;
        public bool levelReset;
        public bool levelPause;

        private void Start()
        {
            //set pause check
            pauseCheck = true;
        }

        private void Update()
        {
            //Check Pause
            levelPause = Input.GetButtonDown("Pause");
            //While paused, don't get game input
            if(pauseCheck)
            {
                playerJump = Input.GetButtonDown("Jump");
                playerSpawn = Input.GetButtonDown("Fire1");
                playerAction = Input.GetButtonDown("Fire2");
                levelReset = Input.GetButtonDown("Fire3");
            }
        }
    }
}
