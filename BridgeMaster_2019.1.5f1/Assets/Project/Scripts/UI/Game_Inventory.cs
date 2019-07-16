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

        private void ToggleInventory(InputActions action, InputActionStates state, float axis) {
            if (action == InputActions.ToggleInventoryAction) {
                _inventory.SetActive(!_inventory.activeSelf);
                Game_Master.Instance.ToggleInventory(_inventory.activeSelf);
            }
        }
    }
}