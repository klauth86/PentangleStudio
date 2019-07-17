namespace BridgeMaster.Base {
    public abstract class ANY_Subscriber<T> : ANY_Handler<T> where T : EventRoot {

        protected bool _isSubscribed;

        #region CTOR

        public ANY_Subscriber(Functor<T, EventRoot> accessFunctor) : base(accessFunctor) { }

        #endregion

        protected virtual void Subscribe() {
            if (!_isSubscribed) {
                Master.isDisablingEvent += TargetOrSubsciberIsDisabling;
                _isSubscribed = true;
            }
        }

        protected virtual void Unsubscribe() {
            if (_isSubscribed) {
                Master.isDisablingEvent -= TargetOrSubsciberIsDisabling;
                _isSubscribed = false;
            }
        }

        protected virtual void TargetOrSubsciberIsDisabling() {
            Unsubscribe();
        }

        private void OnEnable() {
            Subscribe();
        }

        private void OnDisable() {
            Unsubscribe();
        }
    }
}
