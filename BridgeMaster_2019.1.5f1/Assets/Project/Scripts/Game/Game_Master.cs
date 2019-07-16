using BridgeMaster.Base;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_Master : MonoBehaviour {

        public event EventHandler<InputActions, InputActionStates, float> InputKeyEvent;

        public event EventHandler<bool> ToggleMenuEvent;
        public event EventHandler<bool> ToggleInventoryEvent;
        public event EventHandler<bool> ToggleCharacterStatsEvent;

        public event EventHandler GameOverEvent;

        public event EventHandler<Locations> EnterLocationEvent;
        public event EventHandler<Locations> ExitLocationEvent;

        public void ToggleMenu(bool isMenuOn) {
            ToggleMenuEvent?.Invoke(isMenuOn);
        }

        public void ToggleInventory(bool isInventoryOn) {
            ToggleInventoryEvent?.Invoke(isInventoryOn);
        }

        public void ToggleCharacterStats(bool isCharacterStatsOn) {
            ToggleCharacterStatsEvent?.Invoke(isCharacterStatsOn);
        }

        public void InputKey(InputActions action, InputActionStates state, float axis) {
            InputKeyEvent?.Invoke(action, state, axis);
        }

        public void GameOver() {
            GameOverEvent?.Invoke();
        }

        public void EnterLocation(Locations location) {
            EnterLocationEvent?.Invoke(location);
        }

        public void ExitLocation(Locations location) {
            ExitLocationEvent?.Invoke(location);
        }

        #region SINGLETON

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

        private Game_Master() {}

        #endregion
    }
}