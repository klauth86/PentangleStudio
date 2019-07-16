namespace BridgeMaster.Base {
    public abstract class GO_Subscriber<T> : ANY_Subscriber<T> where T : EventRoot {

        public GO_Subscriber() : base((root) => FindObjectOfType<T>()) { }
    }
}
