using BridgeMaster.Base;
using BridgeMaster.Dicts;

namespace BridgeMaster.Game {
    public class GameMaster : EventRoot {

        #region MASTER EVENTS

        public event EventHandler<bool> ToggleMenuEvent;
        public void ToggleMenu(bool isMenuOn) {
            ToggleMenuEvent?.Invoke(isMenuOn);
        }

        public event EventHandler<bool> ToggleInventoryEvent;
        public void ToggleInventory(bool isInventoryOn) {
            ToggleInventoryEvent?.Invoke(isInventoryOn);
        }

        public event EventHandler<bool> ToggleCharacterStatsEvent;
        public void ToggleCharacterStats(bool isCharacterStatsOn) {
            ToggleCharacterStatsEvent?.Invoke(isCharacterStatsOn);
        }

        public event EventHandler<InputActions, InputActionStates, float> InputKeyEvent;
        public void InputKey(InputActions action, InputActionStates state, float axis) {
            InputKeyEvent?.Invoke(action, state, axis);
        }

        #endregion

        #region SINGLETON

        public static GameMaster Instance;

        protected override void CallOnAwakeEvent() {

            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }

            base.CallOnAwakeEvent();
        }

        private GameMaster() { }

        #endregion
    }
}