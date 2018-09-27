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

        private void Update()
        {
            playerJump = Input.GetButtonDown("Jump");
            playerSpawn = Input.GetButtonDown("Fire1");
            playerAction = Input.GetButtonDown("Fire2");
        }
    }
}
