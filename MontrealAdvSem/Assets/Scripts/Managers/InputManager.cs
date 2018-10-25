﻿using System.Collections;
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
            levelPause = Input.GetButtonDown("Pause");
            playerJump = Input.GetButtonDown("Jump");
            playerSpawn = Input.GetButtonDown("Spawn");
            playerAction = Input.GetButtonDown("Action");
            levelReset = Input.GetButtonDown("Reset");
            InputEvents();
        }

        void InputEvents()
        {
            if (levelPause)
            {
                //Pause Level
                EventParam eventParam = new EventParam();
                Managers.EventManager.TriggerEvent("PauseToggle", eventParam);
            }
            if (levelReset)
            {
                //Reset Level
                EventParam eventParam = new EventParam();
                Managers.EventManager.TriggerEvent("ResetLevel", eventParam);
            }
        }
    }
}
