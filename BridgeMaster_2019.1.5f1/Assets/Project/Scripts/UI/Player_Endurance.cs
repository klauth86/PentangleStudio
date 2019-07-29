using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Player_Endurance : MonoBehaviour {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;

        private void OnEnable() {
            Player.Instance.EnduranceChangedEvent += EnduranceChanged;
        }

        private void OnDisable() {
            Player.Instance.EnduranceChangedEvent -= EnduranceChanged;
        }

        private void EnduranceChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}