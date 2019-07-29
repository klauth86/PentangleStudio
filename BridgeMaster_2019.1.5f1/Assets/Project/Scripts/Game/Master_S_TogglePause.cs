using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Game {
    public class Master_S_TogglePause : ANY_Subscriber<Master> {

        #region CTOR

        public Master_S_TogglePause() : base((eventRoot) => Master.Instance) { }

        #endregion

        protected override void Subscribe() {
            Master.ToggleMenuEvent += TogglePause;
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.ToggleMenuEvent -= TogglePause;
            base.Unsubscribe();
        }

        private void TogglePause(bool isPaused) {
            Time.timeScale = isPaused ? 0 : 1;
        }
    }
}