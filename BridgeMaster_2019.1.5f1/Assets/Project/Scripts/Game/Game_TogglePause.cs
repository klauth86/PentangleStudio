using UnityEngine;

namespace BridgeMaster.Game {
    public class Game_TogglePause : MonoBehaviour {

        private void OnEnable() {
            Game_Master.Instance.ToggleMenuEvent += TogglePause;
        }

        private void OnDisable() {
            Game_Master.Instance.ToggleMenuEvent -= TogglePause;
        }

        private void TogglePause(bool isPaused) {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}