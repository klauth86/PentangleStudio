using UnityEngine;

namespace BridgeMaster.Masters {
    [RequireComponent(typeof(Master))]
    public abstract class Base : MonoBehaviour {
        private Master _master;
        protected Master Master {
            get {
                return _master ?? (_master = GetComponent<Master>());
            }
        }
    }
}
