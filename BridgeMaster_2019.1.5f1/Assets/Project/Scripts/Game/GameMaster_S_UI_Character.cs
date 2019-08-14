using BridgeMaster.Base;
using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class GameMaster_S_UI_Character : ANY_Subscriber<GameMaster> {
        [SerializeField] private GameObject _characterPanel;

        #region CTOR

        public GameMaster_S_UI_Character() : base((eventRoot) => GameMaster.Instance) { }

        #endregion

        protected override void CallOnAwakeEvent() {

            if (!_characterPanel)
                Logger.LogIsNotSetInInspectorError(nameof(_characterPanel), name, gameObject.name);

            base.CallOnAwakeEvent();
        }

        protected override void Subscribe() {
            if (!_isSubscribed) {
                GameMaster.Instance.InputKeyEvent += ToggleStats;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                GameMaster.Instance.InputKeyEvent -= ToggleStats;
                base.Unsubscribe();
            }
        }

        private void ToggleStats(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleCharacterStatsAction) {
                _characterPanel.SetActive(!_characterPanel.activeSelf);
                GameMaster.Instance.ToggleCharacterStats(_characterPanel.activeSelf);
            }
        }
    }
}