using BridgeMaster.Base;
using BridgeMaster.Location;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BridgeMaster.UI {

    public class LocationMaster_S_UI_Curtain : ANY_Subscriber<LocationMaster> {

        [SerializeField] private Image _curtain;
        [SerializeField] private float _fadingDuration = 0.5f;
        [SerializeField] private int _fadingSteps = 256;

        #region CTOR

        public LocationMaster_S_UI_Curtain() : base((eventRoot) => LocationMaster.Instance) { }

        #endregion

        protected override void CallOnAwakeEvent() {

            if (!_curtain)
                Logger.LogIsNotSetInInspectorError(nameof(_curtain), name, gameObject.name);

            base.CallOnAwakeEvent();
        }

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.EnterLocationEvent += EnterLocation;
                Master.ExitLocationEvent += ExitLocation;
                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.EnterLocationEvent -= EnterLocation;
                Master.ExitLocationEvent -= ExitLocation;
                base.Unsubscribe();
            }
        }

        private void EnterLocation() {
            StartCoroutine(LocationRoutine(Dicts.Locations.None));
        }

        private void ExitLocation(Dicts.Locations to) {
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
                Master.LocationEntered();
            else
                Master.LocationExited(nextLocation);
        }
    }
}
