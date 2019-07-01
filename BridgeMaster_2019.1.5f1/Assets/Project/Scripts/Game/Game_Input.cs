using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_Input : MonoBehaviour {
        [SerializeField] private KeyCode _toggleCharacterStatsKeyCode;
        [SerializeField] private KeyCode _toggleMenuKeyCode;
        [SerializeField] private KeyCode _toggleInventoryKeyCode;

        [SerializeField] private string _runAxisName;
        [SerializeField] private string _jumpButtonName;
        [SerializeField] private string _castSpellButtonName;

        private void Update() {
            if (Input.GetKeyUp(_toggleCharacterStatsKeyCode))
                Game_Master.Instance.InputKey(InputAction.ToggleCharacterStatsAction, InputActionState.KeyUp, 0);

            if (Input.GetKeyUp(_toggleInventoryKeyCode))
                Game_Master.Instance.InputKey(InputAction.ToggleInventoryAction, InputActionState.KeyUp, 0);

            if (Input.GetKeyUp(_toggleMenuKeyCode))
                Game_Master.Instance.InputKey(InputAction.ToggleMenuAction, InputActionState.KeyUp, 0);


            var axis = Input.GetAxis(_runAxisName);
            Game_Master.Instance.InputKey(InputAction.RunAxisAction, InputActionState.Unknown, axis);

            if (Input.GetButtonDown(_jumpButtonName))
                Game_Master.Instance.InputKey(InputAction.JumpButton, InputActionState.KeyDown, 0);

            if (Input.GetButtonDown(_castSpellButtonName))
                Game_Master.Instance.InputKey(InputAction.CastSpellButton, InputActionState.KeyDown, 0);

            if (Input.GetButtonUp(_castSpellButtonName))
                Game_Master.Instance.InputKey(InputAction.CastSpellButton, InputActionState.KeyUp, 0);
        }
    }
}