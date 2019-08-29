using BridgeMaster.Base;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Location {

    public class LocationMaster_H_CrossPoint : ANY_Handler<LocationMaster> {

        public bool HasPrevHit;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate = 0.125f;
        [SerializeField] private float _checkRadius = 0.5f;

        [SerializeField] private Collider2D _firstCollider;
        [SerializeField] private Collider2D _secondCollider;

        private Transform _transform;
        public Transform Transform {
            get { return _transform ?? (_transform = transform); }
        }

        #region CTOR

        public LocationMaster_H_CrossPoint() : base((eventRoot) => LocationMaster.Instance) { }

        #endregion

        protected override void CallOnAwakeEvent() {
            if (!_firstCollider)
                Logger.LogIsNotSetInInspectorError(nameof(_firstCollider), name, gameObject.name);
            else
                _firstCollider.gameObject.SetActive(true);

            if (!_secondCollider)
                Logger.LogIsNotSetInInspectorError(nameof(_secondCollider), name, gameObject.name);
            else
                _secondCollider.gameObject.SetActive(false);

            base.CallOnAwakeEvent();
        }

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
                    HasPrevHit = true;
                    ActivateCross();
                }
                else if (!hit && HasPrevHit) {
                    HasPrevHit = false;
                    DeactivateCross();
                }

                yield return new WaitForSeconds(_checkRate);
            }
        }

        private void ActivateCross() {

        }

        private void DeactivateCross() {

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