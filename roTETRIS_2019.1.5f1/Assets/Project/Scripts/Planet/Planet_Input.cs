using UnityEngine;

namespace roTETRIS.Planet {
    public class Planet_Input : SubBase<Planet_Master> {
        private Vector2 _touchStart;

        // Update is called once per frame
        void Update() {
            if (Input.touchCount>0) {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began) {
                    _touchStart = touch.position;
                }
                if (touch.phase == TouchPhase.Moved) {
                    var angle = Mathf.RoundToInt(Vector2.SignedAngle(_touchStart, touch.position) * 180 / Mathf.PI);
                    if (Mathf.Abs(angle)>Constant.UnitAngle) {
                        _touchStart = touch.position;
                        Master.CallRotatePlanetEvent(angle>0);
                    }
                }
            }
        }
    }
}
