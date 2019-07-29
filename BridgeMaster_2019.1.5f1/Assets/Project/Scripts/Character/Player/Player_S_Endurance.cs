using BridgeMaster.Base;
using BridgeMaster.Characters.Player;
using UnityEngine;

namespace BridgeMaster.UI {
    public class Player_S_Endurance : ANY_Subscriber<Player> {
        [SerializeField] private RectTransform _indicator;
        [SerializeField] private float _indicatorFullWidth;

        #region CTOR

        public Player_S_Endurance() : base((eventRoot) => Player.Instance) { }

        #endregion

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.CharacterState.EnduranceStateChangedEvent += EnduranceStateChanged;
                base.Subscribe();
            }            
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.CharacterState.EnduranceStateChangedEvent -= EnduranceStateChanged;
                base.Unsubscribe();
            }
        }

        private void EnduranceStateChanged(float enduranceState) {
            _indicator.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _indicatorFullWidth * enduranceState);
        }
    }
}