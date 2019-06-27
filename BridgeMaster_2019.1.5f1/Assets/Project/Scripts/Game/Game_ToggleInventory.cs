using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_ToggleInventory : ComponentSubscriber<Master> {
        [SerializeField] private GameObject _inventory;

        private void OnEnable() {
            Master.InputKeyEvent += ToggleInventory;
        }

        private void OnDisable() {
            Master.InputKeyEvent -= ToggleInventory;
        }

        private void ToggleInventory(InputAction action, InputActionState state, float axis) {
            if (action == InputAction.ToggleInventoryAction) {
                _inventory.SetActive(!_inventory.activeSelf);
                Master.ToggleInventory(_inventory.activeSelf);
            }
        }
    }
}