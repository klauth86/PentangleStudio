﻿using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_Input : Base {
        [SerializeField] private KeyCode _toggleCharacterStatsKeyCode;
        [SerializeField] private KeyCode _toggleMenuKeyCode;
        [SerializeField] private KeyCode _toggleInventoryKeyCode;

        private void Update() {
            if (Input.GetKeyUp(_toggleCharacterStatsKeyCode)) {
                Master.InputKey(InputAction.ToggleCharacterStatsAction);
            }
            if (Input.GetKeyUp(_toggleInventoryKeyCode)) {
                Master.InputKey(InputAction.ToggleInventoryAction);
            }
            if (Input.GetKeyUp(_toggleMenuKeyCode)) {
                Master.InputKey(InputAction.ToggleMenuAction);
            }
        }
    }
}
