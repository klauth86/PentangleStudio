using BridgeMaster.Base;
using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class GameMaster_S_UI_Inventory : ANY_Subscriber<GameMaster> {

        [SerializeField] private GameObject _inventoryPanel;

        #region CTOR

        public GameMaster_S_UI_Inventory() : base((eventRoot) => GameMaster.Instance) { }

        #endregion

        #region EVENTS

        protected override void Subscribe() {
            if (!_isSubscribed) {
                GameMaster.Instance.InputKeyEvent += ToggleInventory;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                GameMaster.Instance.InputKeyEvent -= ToggleInventory;
                base.Unsubscribe();
            }
        }

        private void ToggleInventory(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleInventoryAction) {
                _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
                GameMaster.Instance.ToggleInventory(_inventoryPanel.activeSelf);
            }
        }

        #endregion

        protected override void CallOnAwakeEvent() {

            base.CallOnAwakeEvent();

            if (!_inventoryPanel)
                Logger.LogIsNotSetInInspectorError(nameof(_inventoryPanel), name, gameObject.name);
        }
    }
}