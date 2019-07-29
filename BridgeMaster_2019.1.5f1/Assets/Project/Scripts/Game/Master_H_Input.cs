using BridgeMaster.Base;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Master_H_Input : ANY_Handler<Master> {

        [SerializeField] private KeyCode _toggleCharacterStatsKeyCode;
        [SerializeField] private KeyCode _toggleMenuKeyCode;
        [SerializeField] private KeyCode _toggleInventoryKeyCode;

        [SerializeField] private string _runAxisName;
        [SerializeField] private string _jumpButtonName;
        [SerializeField] private string _castSpellButtonName;

        #region CTOR

        public Master_H_Input() : base((eventRoot) => Master.Instance) { }

        #endregion

        private void Update() {

            if (Input.GetKeyUp(_toggleCharacterStatsKeyCode))
                Master.InputKey(InputActions.ToggleCharacterStatsAction, InputActionStates.KeyUp, 0);

            if (Input.GetKeyUp(_toggleInventoryKeyCode))
                Master.InputKey(InputActions.ToggleInventoryAction, InputActionStates.KeyUp, 0);

            if (Input.GetKeyUp(_toggleMenuKeyCode))
                Master.InputKey(InputActions.ToggleMenuAction, InputActionStates.KeyUp, 0);

            var axis = Input.GetAxis(_runAxisName);
            Master.InputKey(InputActions.RunAxisAction, InputActionStates.Unknown, axis);

            if (Input.GetButtonDown(_jumpButtonName))
                Master.InputKey(InputActions.JumpButton, InputActionStates.KeyDown, 0);

            if (Input.GetButtonDown(_castSpellButtonName))
                Master.InputKey(InputActions.CastSpellButton, InputActionStates.KeyDown, 0);

            if (Input.GetButtonUp(_castSpellButtonName))
                Master.InputKey(InputActions.CastSpellButton, InputActionStates.KeyUp, 0);
        }
    }
}