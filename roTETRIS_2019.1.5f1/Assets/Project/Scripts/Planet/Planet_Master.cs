using UnityEngine;

namespace roTETRIS.Planet {
    public class Planet_Master : MonoBehaviour {

        public static Planet_Master Instance;

        private void Awake() {
            Instance = this;
        }

        private Transform _myTransform;
        public Transform MyTransform {
            get {
                return _myTransform ?? (_myTransform = transform);
            }
        }

        public event GameEventHandler<bool> RotatePlanetEvent;

        public void CallRotatePlanetEvent(bool clockwise) {
            RotatePlanetEvent?.Invoke(clockwise);
        }
    }
}
