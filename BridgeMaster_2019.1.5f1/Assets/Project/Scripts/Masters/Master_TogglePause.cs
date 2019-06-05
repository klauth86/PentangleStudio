using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_TogglePause : Master_Base {

        private void OnEnable() {
            Master.ToggleInventoryEvent += TogglePause;
            Master.ToggleMenuEvent += TogglePause;
        }

        private void OnDisable() {
            Master.ToggleInventoryEvent -= TogglePause;
            Master.ToggleMenuEvent -= TogglePause;
        }

        private void TogglePause(bool isPaused) {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}