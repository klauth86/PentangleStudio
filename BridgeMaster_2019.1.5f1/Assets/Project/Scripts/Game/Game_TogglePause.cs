using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_TogglePause : MonoBehaviour {

        private void OnEnable() {
            Master.Instance.ToggleMenuEvent += TogglePause;
        }

        private void OnDisable() {
            Master.Instance.ToggleMenuEvent -= TogglePause;
        }

        private void TogglePause(bool isPaused) {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}