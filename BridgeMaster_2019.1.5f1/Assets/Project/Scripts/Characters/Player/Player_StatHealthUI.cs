﻿using UnityEngine;

namespace BridgeMaster.Characters.Player {
    class Player_StatHealthUI : Base<Master> {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Master.HealthChangedEvent += HealthChanged;
        }

        private void OnDisable() {
            Master.HealthChangedEvent -= HealthChanged;
        }

        private void HealthChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}