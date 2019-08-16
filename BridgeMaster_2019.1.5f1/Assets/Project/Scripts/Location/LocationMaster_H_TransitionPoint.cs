using BridgeMaster.Base;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Location {

    public class LocationMaster_H_TransitionPoint : ANY_Handler<LocationMaster> {

        public bool HasPrevHit;
        public Dicts.Locations NextLocation;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate = 0.125f;
        [SerializeField] private float _checkRadius = 2.5f;

        private Transform _transform;
        public Transform Transform {
            get { return _transform ?? (_transform = transform); }
        }

        #region CTOR

        public LocationMaster_H_TransitionPoint() : base((eventRoot) => LocationMaster.Instance) { }

        #endregion

        protected override void CallOnEnableEvent() {
            StartCoroutine(TransitionPointRoutine());
            base.CallOnEnableEvent();
        }

        protected override void CallOnDisableEvent() {
            StopAllCoroutines();
            base.CallOnDisableEvent();
        }        

        private IEnumerator TransitionPointRoutine() {

            yield return new WaitForSeconds(Random.Range(0, _checkRate));

            while (true) {

                var hit = Physics2D.CircleCast(Transform.position, _checkRadius, Vector2.zero, 0, _playerLayer);

                if (hit && !HasPrevHit) {
                    break;
                }
                else {
                    if (!hit && HasPrevHit)
                        HasPrevHit = false;

                    yield return new WaitForSeconds(_checkRate);
                }
            }

            Master.ExitLocation(NextLocation);
        }

        #region GIZMO
#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(Transform.position, _checkRadius);
        }
#endif
        #endregion
    }
}