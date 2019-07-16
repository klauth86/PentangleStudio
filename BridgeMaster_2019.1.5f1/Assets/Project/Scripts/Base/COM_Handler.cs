using UnityEngine;

namespace BridgeMaster.Base {
    public abstract class COM_Handler<T> : ANY_Handler<T> where T : Component {

        public COM_Handler():base((root)=>root.GetComponent<T>()) {}
    }
}