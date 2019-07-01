using BridgeMaster.Dicts;
using BridgeMaster.Game;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Game_Inventory : MonoBehaviour {
        [SerializeField] private GameObject _inventory;

        private void OnEnable() {
            Game_Master.Instance.InputKeyEvent += ToggleInventory;
        }

        private void OnDisable() {
            Game_Master.Instance.InputKeyEvent -= ToggleInventory;
        }

        private void ToggleInventory(InputAction action, InputActionState state, float axis) {
            if (action == InputAction.ToggleInventoryAction) {
                _inventory.SetActive(!_inventory.activeSelf);
                Game_Master.Instance.ToggleInventory(_inventory.activeSelf);
            }
        }
    }
}