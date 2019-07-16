using UnityEngine;

namespace BridgeMaster.Base {
    public abstract class ANY_Handler<T> : EventRoot where T : Component {

        protected Functor<T, EventRoot> _accessFunctor;

        private T _master;
        public T Master {
            get {
                return _master ??
                    (_master = _accessFunctor?.Invoke(this) ??
                    throw new System.Exception($"{GetType().Name} Component of {gameObject.name} GameObject doesn't find Object of {typeof(T).Name} Type on the Scene")
                    );
            }
        }

        #region CTOR

        public ANY_Handler(Functor<T, EventRoot> accessFunctor) {
            _accessFunctor = accessFunctor ?? throw new System.NullReferenceException(nameof(accessFunctor));
        }

        #endregion
    }
}
