using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    class Player_Mana : MonoBehaviour {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;
        
        private void OnEnable() {
            Player.Instance.ManaChangedEvent += ManaChanged;
        }

        private void OnDisable() {
            Player.Instance.ManaChangedEvent -= ManaChanged;
        }

        private void ManaChanged(float value, float max) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * value / max);
        }
    }
}