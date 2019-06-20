using UnityEngine;

namespace roTETRIS {
    public class GlobalBase<T> : MonoBehaviour where T : Object {
        private T _master;
        public T Master {
            get {
                return _master ?? (_master = FindObjectOfType<T>());
            }
        }
    }
}