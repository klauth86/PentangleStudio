using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    class Player_Health : MonoBehaviour {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Player.Instance.HealthChangedEvent += HealthChanged;
        }

        private void OnDisable() {
            Player.Instance.HealthChangedEvent -= HealthChanged;
        }

        private void HealthChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}