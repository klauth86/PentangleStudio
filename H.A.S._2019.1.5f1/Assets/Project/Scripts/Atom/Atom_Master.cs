using UnityEngine;

namespace HAS.Atom {
    public class Atom_Master : MonoBehaviour {
        public int n = 1;
        public int l = 0;
        public int k = 0;

        public GameEventHandler<double, bool> EmmitPhotonEvent;
        public GameEventHandler<double, bool> DevourPhotonEvent;
        public GameEventHandler<int, int> ChangeNEvent;
        public GameEventHandler<int, int> ChangeLEvent;
        public GameEventHandler<int, int> ChangeKEvent;
        public GameEventHandler<double> ChangeLambdaEvent;

        public void CallEmmitPhotonEvent(double w, bool isEnergy=false) {
            EmmitPhotonEvent?.Invoke(w, isEnergy);
        }

        public void CallDevourPhotonEvent(double w, bool isEnergy = false) {
            DevourPhotonEvent?.Invoke(w, isEnergy);
        }

        public void CallChangeNEvent(int oldValue, int newValue) {
            n = newValue;
            ChangeNEvent?.Invoke(oldValue, newValue);
        }

        public void CallChangeLEvent(int oldValue, int newValue) {
            l = newValue;
            ChangeLEvent?.Invoke(oldValue, newValue);
        }

        public void CallChangeKEvent(int oldValue, int newValue) {
            k = newValue;
            ChangeKEvent?.Invoke(oldValue, newValue);
        }

        public void CallChangeLambdaEvent(double lambda) {
            ChangeLambdaEvent?.Invoke(lambda);
        }
    }
}
