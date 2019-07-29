using BridgeMaster.Base;
using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Master_S_UI_Menu : ANY_Subscriber<Master> {

        [SerializeField] private GameObject _menu;

        #region CTOR

        public Master_S_UI_Menu() : base((eventRoot) => Master.Instance) { }

        #endregion

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.Instance.InputKeyEvent += ToggleMenu;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.Instance.InputKeyEvent -= ToggleMenu;
                base.Unsubscribe();
            }
        }

        private void ToggleMenu(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleMenuAction) {
                _menu.SetActive(!_menu.activeSelf);
                Master.Instance.ToggleMenu(_menu.activeSelf);
            }
        }
    }
}