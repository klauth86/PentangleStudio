using UnityEngine;

namespace BridgeMaster {
    public abstract class ComponentSubscriber<T> : MonoBehaviour where T : MonoBehaviour {
        private T _master;
        protected T Master {
            get {
                return _master ?? (
                    _master = GetComponent<T>() ?? 
                    throw new System.Exception($"{GetType().Name} Component of {gameObject.name} GameObject doesn't find Required Component {typeof(T).Name}")
                    );
            }
        }
    }
}
