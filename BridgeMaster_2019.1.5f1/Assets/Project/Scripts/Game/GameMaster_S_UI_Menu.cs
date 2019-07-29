using BridgeMaster.Base;
using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class GameMaster_S_UI_Menu : ANY_Subscriber<GameMaster> {

        [SerializeField] private GameObject _menuPanel;

        #region CTOR

        public GameMaster_S_UI_Menu() : base((eventRoot) => GameMaster.Instance) { }

        #endregion

        private void Awake() {
            if (!_menuPanel)
                Logger.LogIsNotSetInInspectorError(nameof(_menuPanel), name, gameObject.name);
        }

        protected override void Subscribe() {
            if (!_isSubscribed) {
                GameMaster.Instance.InputKeyEvent += ToggleMenu;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                GameMaster.Instance.InputKeyEvent -= ToggleMenu;
                base.Unsubscribe();
            }
        }

        private void ToggleMenu(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleMenuAction) {
                _menuPanel.SetActive(!_menuPanel.activeSelf);
                GameMaster.Instance.ToggleMenu(_menuPanel.activeSelf);
            }
        }
    }
}