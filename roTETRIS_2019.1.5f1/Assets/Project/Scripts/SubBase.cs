using UnityEngine;

namespace roTETRIS {
    public class SubBase<T> : MonoBehaviour where T : class {
        private T _master;
        public T Master {
            get {
                return _master ?? (_master = GetComponent<T>());
            }
        }
    }
}