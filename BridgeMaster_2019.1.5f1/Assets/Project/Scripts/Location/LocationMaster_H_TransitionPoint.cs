using BridgeMaster.Base;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Location {

    public class LocationMaster_H_TransitionPoint : ANY_Handler<LocationMaster> {

        public bool HasPrevHit;
        public Dicts.Locations NextLocation;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate = 0.125f;
        [SerializeField] private Vector2 _checkCapsuleSize = new Vector2(2, 7);

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

            var myTransform = transform;

            while (true) {

                var hit = Physics2D.CapsuleCast(new Vector2(myTransform.position.x, myTransform.position.y),
                    _checkCapsuleSize, CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, _playerLayer);

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
    }
}