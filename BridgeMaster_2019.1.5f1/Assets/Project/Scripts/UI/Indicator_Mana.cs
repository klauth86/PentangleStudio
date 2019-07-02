using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    class Indicator_Mana : MonoBehaviour {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Player.PlayerInstance.ManaChangedEvent += ManaChanged;
        }

        private void OnDisable() {
            Player.PlayerInstance.ManaChangedEvent -= ManaChanged;
        }

        private void ManaChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}