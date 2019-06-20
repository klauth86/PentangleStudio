using UnityEngine;

namespace roTETRIS.Planet {
    public class Planet_Rotator : SubBase<Planet_Master> {
        private void OnEnable() {
            Master.RotatePlanetEvent += OnRotatePlanetEvent;
        }

        private void OnDisable() {
            Master.RotatePlanetEvent -= OnRotatePlanetEvent;
        }

        private void OnRotatePlanetEvent(bool clockwise) {
            Master.MyTransform.Rotate(Vector3.back, clockwise ? Constant.UnitAngle : (-1) * Constant.UnitAngle);
        }
    }
}
