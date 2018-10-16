using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InputManager : Singleton<InputManager>
    {
        [Header("Player Control Booleans")]
        public bool playerJump;
        public bool playerSpawn;
        public bool playerAction;
        public bool levelReset;
        public bool levelPause;

        private void Update()
        {
            //Check Pause
            levelPause = Input.GetButtonDown("Pause");
            playerJump = Input.GetButtonDown("Jump");
            playerSpawn = Input.GetButtonDown("Spawn");
            playerAction = Input.GetButtonDown("Action");
            levelReset = Input.GetButtonDown("Reset");
        }
    }
}
