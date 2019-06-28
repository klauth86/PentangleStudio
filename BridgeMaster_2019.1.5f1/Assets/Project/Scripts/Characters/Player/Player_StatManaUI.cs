using UnityEngine;

namespace BridgeMaster.Characters.Player {
    class Player_StatManaUI : Base<Master> {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Master.ManaChangedEvent += ManaChanged;
        }

        private void OnDisable() {
            Master.ManaChangedEvent -= ManaChanged;
        }

        private void ManaChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}