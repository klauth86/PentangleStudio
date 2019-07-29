using BridgeMaster.Base;
using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Master_S_UI_Stats : ANY_Subscriber<Master> {
        [SerializeField] private GameObject _characterStats;

        #region CTOR

        public Master_S_UI_Stats() : base((eventRoot) => Master.Instance) { }

        #endregion

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.Instance.InputKeyEvent += ToggleStats;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.Instance.InputKeyEvent -= ToggleStats;
                base.Unsubscribe();
            }
        }

        private void ToggleStats(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleCharacterStatsAction) {
                _characterStats.SetActive(!_characterStats.activeSelf);
                Master.Instance.ToggleCharacterStats(_characterStats.activeSelf);
            }
        }
    }
}