using System.Collections;
using System.Linq;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Wandering : Base<AI> {
        private int _i;
        [SerializeField] private Transform[] _wanderingPoints;
        [SerializeField] private float _standingRate = 1;

        protected override void Subscribe() {
            Master.SetTargetEvent += SetTarget;
            Master.ReachTargetEvent += ReachTarget;
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.SetTargetEvent -= SetTarget;
            Master.ReachTargetEvent -= ReachTarget;
            base.Unsubscribe();
        }

        private void ReachTarget(Transform transform) {
            if (_wanderingPoints.Any(p => p == transform)) {
                StartCoroutine(ToTheNextPointRoutine());
            }
        }

        private IEnumerator ToTheNextPointRoutine() {
            yield return new WaitForSeconds(_standingRate);
            _i = (_i + 1) % _wanderingPoints.Length;
            Master.SetTarget(_wanderingPoints[_i]);
            Debug.Log(_i);
        }

        private void SetTarget(Transform transform) {
            if (transform == null) {
                _i = 0;
                Master.SetTarget(_wanderingPoints[_i]);
            }
        }
    }
}