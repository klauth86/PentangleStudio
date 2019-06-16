using UnityEngine;

namespace HAS.Atom {
    public class Atom_SubBase : MonoBehaviour {
        private Atom_Master _master;
        public Atom_Master Master {
            get {
                return _master ?? (_master = GetComponent<Atom_Master>());
            }
        }
    }
}
