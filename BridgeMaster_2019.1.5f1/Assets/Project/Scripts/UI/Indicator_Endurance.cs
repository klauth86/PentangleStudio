using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Indicator_Endurance : MonoBehaviour {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;

        private void OnEnable() {
            Player.PlayerInstance.EnduranceChangedEvent += EnduranceChanged;
        }

        private void OnDisable() {
            Player.PlayerInstance.EnduranceChangedEvent -= EnduranceChanged;
        }

        private void EnduranceChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}