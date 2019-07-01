using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.Game {
    class UI_Health : MonoBehaviour {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Player.PlayerSession.HealthChangedEvent += HealthChanged;
        }

        private void OnDisable() {
            Player.PlayerSession.HealthChangedEvent -= HealthChanged;
        }

        private void HealthChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}