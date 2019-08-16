using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Game {
    public class GameMaster_S_TogglePause : ANY_Subscriber<GameMaster> {

        #region CTOR

        public GameMaster_S_TogglePause() : base((eventRoot) => GameMaster.Instance) { }

        #endregion

        #region EVENTS

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.ToggleMenuEvent += TogglePause;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.ToggleMenuEvent -= TogglePause;
                base.Unsubscribe();
            }
        }

        private void TogglePause(bool isPaused) {
            Time.timeScale = isPaused ? 0 : 1;
        }

        #endregion
    }
}