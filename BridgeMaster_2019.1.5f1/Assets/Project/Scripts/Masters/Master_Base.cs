using UnityEngine;

namespace BridgeMaster.Masters {
    [RequireComponent(typeof(Master))]
    public class Master_Base : MonoBehaviour {
        private Master _master;
        protected Master Master {
            get {
                return _master ?? (_master = GetComponent<Master>());
            }
        }
    }
}
