using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_ToggleInventory : Base {
        [SerializeField] private GameObject _inventory;

        private void OnEnable() {
            Master.InputKeyEvent += ToggleInventory;
        }

        private void OnDisable() {
            Master.InputKeyEvent -= ToggleInventory;
        }

        private void ToggleInventory(InputAction action) {
            if (action == InputAction.ToggleInventoryAction) {
                _inventory.SetActive(!_inventory.activeSelf);
                Master.ToggleInventory();
            }
        }
    }
}