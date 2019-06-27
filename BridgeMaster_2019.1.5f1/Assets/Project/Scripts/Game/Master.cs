using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Master : MonoBehaviour {

        public event GameEventHandler<InputAction, InputActionState, float> InputKeyEvent;

        public event GameEventHandler<bool> ToggleMenuEvent;
        public event GameEventHandler<bool> ToggleInventoryEvent;
        public event GameEventHandler<bool> ToggleCharacterStatsEvent;

        public event GameEventHandler GameOverEvent;

        public event GameEventHandler EnterLocationEvent;
        public event GameEventHandler<Location> ExitLocationEvent;

        private void Start() {
            EnterLocation();
        }

        public void ToggleMenu(bool isMenuOn) {
            ToggleMenuEvent?.Invoke(isMenuOn);
        }

        public void ToggleInventory(bool isInventoryOn) {
            ToggleInventoryEvent?.Invoke(isInventoryOn);
        }

        public void ToggleCharacterStats(bool isCharacterStatsOn) {
            ToggleCharacterStatsEvent?.Invoke(isCharacterStatsOn);
        }

        public void InputKey(InputAction action, InputActionState state, float axis) {
            InputKeyEvent?.Invoke(action, state, axis);
        }

        public void GameOver() {
            GameOverEvent?.Invoke();
        }

        public void EnterLocation() {
            EnterLocationEvent?.Invoke();
        }

        public void ExitLocation(Location location) {
            ExitLocationEvent?.Invoke(location);
        }
    }
}
