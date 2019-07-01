using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BridgeMaster.GameLocation {

    public class GameLocation_Curtain : MonoBehaviour {
        [SerializeField] private Image _curtain;
        [SerializeField] private float _fadingDuration;
        [SerializeField] private int _fadingSteps;

        private void OnEnable() {
            Master.LocationSession.EnterLocationEvent += EnterLocation;
            Master.LocationSession.ExitLocationEvent += ExitLocation;
        }

        private void OnDisable() {
            StopAllCoroutines();
            Master.LocationSession.EnterLocationEvent -= EnterLocation;
            Master.LocationSession.ExitLocationEvent -= ExitLocation;
        }

        private void EnterLocation() {
            StartCoroutine(LocationRoutine(Dicts.Location.None));
        }

        private void ExitLocation(Dicts.Location from, Dicts.Location to) {
            StartCoroutine(LocationRoutine(to));
        }

        private IEnumerator LocationRoutine(Dicts.Location nextLocation) {
            if (_curtain) {
                for (int i = 1; i <= _fadingSteps; i++) {

                    var step = nextLocation == Dicts.Location.None
                        ? (1.0f * _fadingSteps - i)
                        : (1.0f * i);

                    _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, step / _fadingSteps);
                    yield return new WaitForSeconds(_fadingDuration / _fadingSteps);
                }
            }

            if (nextLocation == Dicts.Location.None)
                Master.LocationSession.LocationEntered();
            else
                Master.LocationSession.LocationExited(nextLocation);
        }
    }
}
