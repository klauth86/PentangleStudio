using UnityEngine;

namespace BridgeMaster.Base {

    public class EventRoot : MonoBehaviour {

        public event EventHandler OnEnableEvent;
        
        protected virtual void CallOnEnableEvent() {
            OnEnableEvent?.Invoke();
        }

        private void OnEnable() {
            CallOnEnableEvent();
        }



        public event EventHandler OnDisableEvent;

        protected virtual void CallOnDisableEvent() {
            OnDisableEvent?.Invoke();
        }

        private void OnDisable() {
            CallOnDisableEvent();
        }



        public event EventHandler OnAwakeEvent;

        protected virtual void CallOnAwakeEvent() {
            OnAwakeEvent?.Invoke();
        }

        private void Awake() {
            CallOnAwakeEvent();
        }
    }
}