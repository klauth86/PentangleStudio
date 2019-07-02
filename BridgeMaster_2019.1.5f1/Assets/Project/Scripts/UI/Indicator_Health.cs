using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    class Indicator_Health : MonoBehaviour {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Player.PlayerInstance.HealthChangedEvent += HealthChanged;
        }

        private void OnDisable() {
            Player.PlayerInstance.HealthChangedEvent -= HealthChanged;
        }

        private void HealthChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}