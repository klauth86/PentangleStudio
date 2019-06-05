using UnityEngine;

namespace BridgeMaster {
    public class Master : MonoBehaviour {
        private bool _isMenuOn;
        private bool _isInventoryOn;

        public event GameEventHandler<bool> ToggleMenuEvent;
        public event GameEventHandler<bool> ToggleInventoryEvent;
        public event GameEventHandler GameOverEvent;

        public void ToggleMenu() {
            _isMenuOn = !_isMenuOn;
            ToggleMenuEvent?.Invoke(_isMenuOn);
        }

        public void ToggleInventory() {
            _isInventoryOn = !_isInventoryOn;
            ToggleInventoryEvent?.Invoke(_isInventoryOn);
        }

        public void GameOver() {
            GameOverEvent?.Invoke();
        }
    }
}
