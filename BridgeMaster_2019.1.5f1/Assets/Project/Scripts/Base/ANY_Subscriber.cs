namespace BridgeMaster.Base {
    public abstract class ANY_Subscriber<T> : ANY_Handler<T> where T : EventRoot {

        protected bool _isSubscribed;

        #region CTOR

        public ANY_Subscriber(Functor<T, EventRoot> accessFunctor) : base(accessFunctor) { }

        #endregion

        protected virtual void Subscribe() {
            if (!_isSubscribed) {
                Master.OnEnableEvent += Subscribe;
                Master.OnDisableEvent += Unsubscribe;
                _isSubscribed = true;
            }
        }

        protected virtual void Unsubscribe() {
            if (_isSubscribed) {
                Master.OnEnableEvent -= Subscribe;
                Master.OnDisableEvent -= Unsubscribe;
                _isSubscribed = false;
            }
        }

        protected override sealed void CallOnEnableEvent() {
            Subscribe();
            base.CallOnEnableEvent();
        }

        protected override sealed void CallOnDisableEvent() {
            Unsubscribe();
            base.CallOnDisableEvent();
        }
    }
}
