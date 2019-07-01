namespace BridgeMaster.Characters {
    public abstract class Base<T> : ComponentSubscriber<T> where T : Character_Master {

        private bool _isSubscribed;

        private void OnEnable() {
            if (!_isSubscribed)
                Subscribe();
        }

        private void OnDisable() {
            if (_isSubscribed)
                Unsubscribe();
        }

        protected virtual void Subscribe() {
            Master.DieEvent += Die;
            _isSubscribed = true;
        }

        protected virtual void Unsubscribe() {
            Master.DieEvent -= Die;
            _isSubscribed = false;
        }

        private void Awake() {
            Master.DieEvent += Die;
        }

        void Die() {
            if (_isSubscribed)
                Unsubscribe();
            Destroy(this);
        }
    }
}
