using UnityEngine;

namespace BridgeMaster.Characters.Player {
    class Player_StatEnduranceUI : Base<Master> {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Master.EnduranceChangedEvent += EnduranceChanged;
        }

        private void OnDisable() {
            Master.EnduranceChangedEvent -= EnduranceChanged;
        }

        private void EnduranceChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}