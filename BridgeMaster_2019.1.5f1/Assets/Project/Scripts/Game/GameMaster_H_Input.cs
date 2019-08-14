using BridgeMaster.Base;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class GameMaster_H_Input : ANY_Handler<GameMaster> {

        [SerializeField] private string _attackButtonName;
        [SerializeField] private string _castSpellButtonName;

        [SerializeField] private string _jumpButtonName;
        [SerializeField] private string _runAxisName;

        [SerializeField] private KeyCode _toggleCharacterStatsKeyCode;
        [SerializeField] private KeyCode _toggleMenuKeyCode;
        [SerializeField] private KeyCode _toggleInventoryKeyCode;

        #region CTOR

        public GameMaster_H_Input() : base((eventRoot) => GameMaster.Instance) { }

        #endregion

        protected override void CallOnAwakeEvent() {

            if (string.IsNullOrEmpty(_runAxisName))
                Logger.LogIsNotSetInInspectorError(nameof(_runAxisName), name, gameObject.name);

            if (string.IsNullOrEmpty(_jumpButtonName))
                Logger.LogIsNotSetInInspectorError(nameof(_jumpButtonName), name, gameObject.name);

            if (string.IsNullOrEmpty(_castSpellButtonName))
                Logger.LogIsNotSetInInspectorError(nameof(_castSpellButtonName), name, gameObject.name);

            base.CallOnAwakeEvent();
        }

        private void Update() {

            if (Input.GetButtonDown(_attackButtonName))
                Master.InputKey(InputActions.AttackButton, InputActionStates.KeyDown, 0);

            if (Input.GetButtonUp(_attackButtonName))
                Master.InputKey(InputActions.AttackButton, InputActionStates.KeyUp, 0);

            if (Input.GetButtonDown(_castSpellButtonName))
                Master.InputKey(InputActions.CastSpellButton, InputActionStates.KeyDown, 0);

            if (Input.GetButtonUp(_castSpellButtonName))
                Master.InputKey(InputActions.CastSpellButton, InputActionStates.KeyUp, 0);




            if (Input.GetButtonDown(_jumpButtonName))
                Master.InputKey(InputActions.JumpButton, InputActionStates.KeyDown, 0);

            var axis = Input.GetAxis(_runAxisName);
            Master.InputKey(InputActions.RunAxisAction, InputActionStates.Unknown, axis);




            if (Input.GetKeyUp(_toggleCharacterStatsKeyCode))
                Master.InputKey(InputActions.ToggleCharacterStatsAction, InputActionStates.KeyUp, 0);

            if (Input.GetKeyUp(_toggleInventoryKeyCode))
                Master.InputKey(InputActions.ToggleInventoryAction, InputActionStates.KeyUp, 0);

            if (Input.GetKeyUp(_toggleMenuKeyCode))
                Master.InputKey(InputActions.ToggleMenuAction, InputActionStates.KeyUp, 0);
        }
    }
}