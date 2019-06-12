using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_Input : Game_Base {
        [SerializeField] private KeyCode _toggleCharacterStatsKeyCode;
        [SerializeField] private KeyCode _toggleMenuKeyCode;
        [SerializeField] private KeyCode _toggleInventoryKeyCode;

        [SerializeField] private string _runAxisName;
        [SerializeField] private string _jumpButtonName;
        [SerializeField] private string _castSpellButtonName;

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

            var axis = Input.GetAxis(_runAxisName);
            if (axis != 0)
                Game_Reference.PlayerRef.StartRun(axis);
            else
                Game_Reference.PlayerRef.EndRun();

            if (Input.GetButtonDown(_jumpButtonName))
                Game_Reference.PlayerRef.StartJump();

            if (Input.GetButtonDown(_castSpellButtonName)) {
                Game_Reference.PlayerRef.StartCastSpell();
            }

            if (Input.GetButtonUp(_castSpellButtonName)) {
                Game_Reference.PlayerRef.EndCastSpell();
            }
        }
    }
}
