using UnityEngine;

namespace BridgeMaster {
    public abstract class GameObjectSubscriber<T> : MonoBehaviour where T : MonoBehaviour {
        private T _target;
        public T Target {
            get {
                return _target ??
                    (_target = FindObjectOfType<T>() ??
                    throw new System.Exception($"{GetType().Name} Component of {gameObject.name} GameObject doesn't find Object of {typeof(T).Name} Type on the Scene")
                    );
            }
        }
    }
}
