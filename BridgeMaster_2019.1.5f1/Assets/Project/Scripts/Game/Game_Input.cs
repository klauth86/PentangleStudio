using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_Input : Base {
        [SerializeField] private KeyCode _toggleCharacterStatsKeyCode;
        [SerializeField] private KeyCode _toggleMenuKeyCode;
        [SerializeField] private KeyCode _toggleInventoryKeyCode;

        [SerializeField] private string _runAxisName;
        [SerializeField] private string _jumpButtonName;
        [SerializeField] private string _castSpellButtonName;

        private void Update() {
            if (Input.GetKeyUp(_toggleCharacterStatsKeyCode))
                Master.InputKey(InputAction.ToggleCharacterStatsAction, InputActionState.KeyUp, 0);

            if (Input.GetKeyUp(_toggleInventoryKeyCode))
                Master.InputKey(InputAction.ToggleInventoryAction, InputActionState.KeyUp, 0);

            if (Input.GetKeyUp(_toggleMenuKeyCode))
                Master.InputKey(InputAction.ToggleMenuAction, InputActionState.KeyUp, 0);


            var axis = Input.GetAxis(_runAxisName);
            Master.InputKey(InputAction.RunAxisAction, InputActionState.Unknown, axis);

            if (Input.GetButtonDown(_jumpButtonName))
                Master.InputKey(InputAction.JumpButton, InputActionState.KeyDown, 0);

            if (Input.GetButtonDown(_castSpellButtonName))
                Master.InputKey(InputAction.CastSpellButton, InputActionState.KeyDown, 0);

            if (Input.GetButtonUp(_castSpellButtonName))
                Master.InputKey(InputAction.CastSpellButton, InputActionState.KeyUp, 0);
        }
    }
}