using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_Master : MonoBehaviour {

        public event GameEventHandler<InputAction, InputActionState, float> InputKeyEvent;

        public event GameEventHandler<bool> ToggleMenuEvent;
        public event GameEventHandler<bool> ToggleInventoryEvent;
        public event GameEventHandler<bool> ToggleCharacterStatsEvent;

        public event GameEventHandler GameOverEvent;

        public event GameEventHandler<Location> EnterLocationEvent;
        public event GameEventHandler<Location> ExitLocationEvent;

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

        public void EnterLocation(Location location) {
            EnterLocationEvent?.Invoke(location);
        }

        public void ExitLocation(Location location) {
            ExitLocationEvent?.Invoke(location);
        }

        #region SINGLE INSTANCE

        public static Game_Master Instance;

        private void Awake() {
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        #endregion
    }
}