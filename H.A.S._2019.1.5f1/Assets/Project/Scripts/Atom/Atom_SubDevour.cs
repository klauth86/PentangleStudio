using UnityEngine;

namespace HAS.Atom {
    public class Atom_SubDevour : Atom_SubBase {
        private void OnEnable() {
            Master.DevourPhotonEvent += OnDevourPhotonEvent;
        }

        private void OnDisable() {
            Master.DevourPhotonEvent -= OnDevourPhotonEvent;
        }

        private void OnDevourPhotonEvent(double value, bool isEnergy) {
            var R = Constants.R;
            var h = Constants.h;

            var E = isEnergy ? value : h * value;
            Debug.Log("DEVOUR PHOTON WITH ENERGY " + E);

            var n = Master.n;
            var i = Master.n;

            while (R * h / n / n - R * h / (i + 1) / (i + 1) < E) {
                i++;
            }

            Master.CallChangeNEvent(n, i);
            Master.CallEmmitPhotonEvent(E - R * h / n / n + R * h / (i + 1) / (i + 1), true);
        }
    }
}
