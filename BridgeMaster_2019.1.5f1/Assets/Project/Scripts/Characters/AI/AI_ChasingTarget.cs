using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_ChasingTarget : Base<AI> {

        [SerializeField] private float _reachRange = 0.5f;
        [SerializeField] private float _checkRate = 0.0125f;

        private Transform _target;

        protected override void Subscribe() {
            Master.SetTargetEvent += SetTarget;
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.SetTargetEvent -= SetTarget;
            SetTarget(null);
            base.Unsubscribe();
        }

        private void SetTarget(Transform transform) {
            if (_target == transform)
                return;

            StopAllCoroutines();
            Master.EndRun();
            _target = transform;

            if (transform != null) {
                StartCoroutine(ToTargetRoutine());
            }
        }

        private IEnumerator ToTargetRoutine() {
            while(enabled && _target != null) {
                var distance = _target.position.x - Master.MyTransform.position.x;
                
                if (Mathf.Abs(distance) < _reachRange) {
                    Master.ReachTarget(_target);
                    break;
                }

                Master.StartRun(Mathf.Sign(distance));

                yield return new WaitForSeconds(_checkRate);
            }
        }
    }
}