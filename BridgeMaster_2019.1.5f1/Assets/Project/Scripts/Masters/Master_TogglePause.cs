using UnityEngine;

namespace BridgeMaster.Masters {
    public class Master_TogglePause : Base {

        private void OnEnable() {
            Master.ToggleMenuEvent += TogglePause;
        }

        private void OnDisable() {
            Master.ToggleMenuEvent -= TogglePause;
        }

        private void TogglePause(bool isPaused) {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}