using UnityEngine;

namespace BridgeMaster.Base {

    public class EventRoot : MonoBehaviour {

        public event EventHandler isDisablingEvent;

        protected void IsDisabling() {
            isDisablingEvent?.Invoke();
        }

        private void OnDisable() {
            IsDisabling();
        }
    }
}