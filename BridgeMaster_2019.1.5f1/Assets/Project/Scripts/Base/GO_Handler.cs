using UnityEngine;

namespace BridgeMaster.Base {
    public abstract class GO_Handler<T> : ANY_Handler<T> where T : Component {

        public GO_Handler():base((root)=>FindObjectOfType<T>()) {}
    }
}