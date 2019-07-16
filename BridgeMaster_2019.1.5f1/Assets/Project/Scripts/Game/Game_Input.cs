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
                Game_Master.Instance.InputKey(InputActions.ToggleCharacterStatsAction, InputActionStates.KeyUp, 0);

            if (Input.GetKeyUp(_toggleInventoryKeyCode))
                Game_Master.Instance.InputKey(InputActions.ToggleInventoryAction, InputActionStates.KeyUp, 0);

            if (Input.GetKeyUp(_toggleMenuKeyCode))
                Game_Master.Instance.InputKey(InputActions.ToggleMenuAction, InputActionStates.KeyUp, 0);


            var axis = Input.GetAxis(_runAxisName);
            Game_Master.Instance.InputKey(InputActions.RunAxisAction, InputActionStates.Unknown, axis);

            if (Input.GetButtonDown(_jumpButtonName))
                Game_Master.Instance.InputKey(InputActions.JumpButton, InputActionStates.KeyDown, 0);

            if (Input.GetButtonDown(_castSpellButtonName))
                Game_Master.Instance.InputKey(InputActions.CastSpellButton, InputActionStates.KeyDown, 0);

            if (Input.GetButtonUp(_castSpellButtonName))
                Game_Master.Instance.InputKey(InputActions.CastSpellButton, InputActionStates.KeyUp, 0);
        }
    }
}