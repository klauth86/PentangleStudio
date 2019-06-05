using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster {
    public class Master : MonoBehaviour {
        private bool _isMenuOn;
        private bool _isInventoryOn;
        private bool _isCharacterStatsOn;

        public event GameEventHandler<InputAction> InputKeyEvent;
        public event GameEventHandler<bool> ToggleMenuEvent;
        public event GameEventHandler<bool> ToggleInventoryEvent;
        public event GameEventHandler<bool> ToggleCharacterStatsEvent;
        public event GameEventHandler GameOverEvent;
        public event GameEventHandler<Location> ExitLocationEvent;

        public void ToggleMenu() {
            _isMenuOn = !_isMenuOn;
            ToggleMenuEvent?.Invoke(_isMenuOn);
        }

        public void ToggleInventory() {
            _isInventoryOn = !_isInventoryOn;
            ToggleInventoryEvent?.Invoke(_isInventoryOn);
        }

        public void ToggleCharacterStats() {
            _isCharacterStatsOn = !_isCharacterStatsOn;
            ToggleCharacterStatsEvent?.Invoke(_isCharacterStatsOn);
        }

        public void GameOver() {
            GameOverEvent?.Invoke();
        }

        public void InputKey(InputAction action) {
            InputKeyEvent?.Invoke(action);
        }

        public void ExitLocation(Location location) {
            ExitLocationEvent?.Invoke(location);
        }
    }
}
