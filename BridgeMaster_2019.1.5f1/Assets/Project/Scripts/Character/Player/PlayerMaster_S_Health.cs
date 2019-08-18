using BridgeMaster.Base;
using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    public class PlayerMaster_S_Health : ANY_Subscriber<PlayerMaster> {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;

        #region CTOR

        public PlayerMaster_S_Health() : base((eventRoot) => PlayerMaster.Instance) { }

        #endregion

        #region EVENTS

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.CharacterState.HealthStateChangedEvent += HealthStateChanged;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.CharacterState.HealthStateChangedEvent -= HealthStateChanged;
                base.Unsubscribe();
            }
        }

        private void HealthStateChanged(float healthState) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * healthState);
        }

        #endregion
    }
}