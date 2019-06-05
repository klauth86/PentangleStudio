using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_Input : Base {
        [SerializeField] private KeyCode _toggleMenuKeyCode;
        [SerializeField] private KeyCode _toggleInventoryKeyCode;

        private void Update() {
            if (Input.GetKeyUp(_toggleMenuKeyCode)) {
                Master.InputKey(InputAction.ToggleMenuAction);
            }
            if (Input.GetKeyUp(_toggleInventoryKeyCode)) {
                Master.InputKey(InputAction.ToggleInventoryAction);
            }
        }
    }
}
