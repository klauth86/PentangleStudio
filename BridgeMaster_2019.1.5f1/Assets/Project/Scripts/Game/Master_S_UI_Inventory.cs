using BridgeMaster.Base;
using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Master_S_UI_Inventory : ANY_Subscriber<Master> {

        [SerializeField] private GameObject _inventory;

        #region CTOR

        public Master_S_UI_Inventory() : base((eventRoot) => Master.Instance) { }

        #endregion

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.Instance.InputKeyEvent += ToggleInventory;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.Instance.InputKeyEvent -= ToggleInventory;
                base.Unsubscribe();
            }
        }

        private void ToggleInventory(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleInventoryAction) {
                _inventory.SetActive(!_inventory.activeSelf);
                Master.Instance.ToggleInventory(_inventory.activeSelf);
            }
        }
    }
}