using UnityEngine;

namespace BridgeMaster.Base {
    public abstract class ANY_Subscriber<T> : ANY_Handler<T> where T : EventRoot {

        protected bool _isSubscribed;

        #region CTOR

        public ANY_Subscriber(Functor<T, EventRoot> accessFunctor) : base(accessFunctor) { }

        #endregion

        protected virtual void Subscribe() {
            Master.isDisablingEvent += TargetOrSubsciberIsDisabling;
            _isSubscribed = true;
        }

        protected virtual void Unsubscribe() {
            Master.isDisablingEvent -= TargetOrSubsciberIsDisabling;
            _isSubscribed = false;
        }

        private void OnDisable() {
            TargetOrSubsciberIsDisabling();
        }

        protected virtual void TargetOrSubsciberIsDisabling() {
            if (_isSubscribed)
                Unsubscribe();
        }
    }
}
