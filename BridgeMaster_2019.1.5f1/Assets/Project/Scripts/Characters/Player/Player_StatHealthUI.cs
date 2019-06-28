using UnityEngine;

namespace BridgeMaster.Characters.Player {
    class Player_StatHealthUI : Base<Master> {
        [SerializeField] private RectTransform _hpIndicator;
        [SerializeField] private float _hpIndicatorFullWidth;
        
        private void OnEnable() {
            Master.HealthChangedEvent += HealthChanged;
        }

        private void OnDisable() {
            Master.HealthChangedEvent -= HealthChanged;
        }

        private void HealthChanged(float value, float max) {
            _hpIndicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _hpIndicatorFullWidth * value / max);
        }
    }
}