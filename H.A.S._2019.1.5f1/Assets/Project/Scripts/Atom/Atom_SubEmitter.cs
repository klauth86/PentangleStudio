using UnityEngine;

namespace HAS.Atom {
    public class Atom_SubEmitter : Atom_SubBase {
        private void OnEnable() {
            Master.EmmitPhotonEvent += OnEmmitPhotonEvent;
        }

        private void OnDisable() {
            Master.EmmitPhotonEvent -= OnEmmitPhotonEvent;
        }

        private void OnEmmitPhotonEvent(double value, bool isEnergy) {
            var E = isEnergy ? value : Constants.h * value;
            Debug.Log("EMIT PHOTON WITH ENERGY " + E);
        }
    }
}
