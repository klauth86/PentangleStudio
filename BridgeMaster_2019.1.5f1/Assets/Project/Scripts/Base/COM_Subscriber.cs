namespace BridgeMaster.Base {
    public abstract class COM_Subscriber<T> : ANY_Subscriber<T> where T : EventRoot {

        public COM_Subscriber() : base((root) => root.GetComponent<T>()) {}
    }
}
