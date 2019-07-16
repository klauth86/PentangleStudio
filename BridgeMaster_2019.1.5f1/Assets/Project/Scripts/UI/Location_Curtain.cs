using BridgeMaster.Location;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BridgeMaster.UI {

    public class Location_Curtain : MonoBehaviour {
        [SerializeField] private Image _curtain;
        [SerializeField] private float _fadingDuration;
        [SerializeField] private int _fadingSteps;

        private void OnEnable() {
            Location_Master.LocationSession.EnterLocationEvent += EnterLocation;
            Location_Master.LocationSession.ExitLocationEvent += ExitLocation;
        }

        private void OnDisable() {
            StopAllCoroutines();
            Location_Master.LocationSession.EnterLocationEvent -= EnterLocation;
            Location_Master.LocationSession.ExitLocationEvent -= ExitLocation;
        }

        private void EnterLocation() {
            StartCoroutine(LocationRoutine(Dicts.Locations.None));
        }

        private void ExitLocation(Dicts.Locations from, Dicts.Locations to) {
            StartCoroutine(LocationRoutine(to));
        }

        private IEnumerator LocationRoutine(Dicts.Locations nextLocation) {
            if (_curtain) {
                for (int i = 1; i <= _fadingSteps; i++) {

                    var step = nextLocation == Dicts.Locations.None
                        ? (1.0f * _fadingSteps - i)
                        : (1.0f * i);

                    _curtain.color = new Color(_curtain.color.r, _curtain.color.g, _curtain.color.b, step / _fadingSteps);
                    yield return new WaitForSeconds(_fadingDuration / _fadingSteps);
                }
            }

            if (nextLocation == Dicts.Locations.None)
                Location_Master.LocationSession.LocationEntered();
            else
                Location_Master.LocationSession.LocationExited(nextLocation);
        }
    }
}
