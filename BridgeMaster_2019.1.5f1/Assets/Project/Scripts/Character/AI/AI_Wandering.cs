using BridgeMaster.Base;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Wandering : COM_Subscriber<AI> {
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

        private void SetTarget(Transform transform) {
            if (transform == null) {
                _i = -1;
                StartCoroutine(ToTheNextPointRoutine());
            }
        }

        private void ReachTarget() {
            if (_wanderingPoints.Any(p => p == Master.Target)) {
                Master.Target = null;
                StartCoroutine(ToTheNextPointRoutine());
            }
        }

        private IEnumerator ToTheNextPointRoutine() {
            yield return new WaitForSeconds(_standingRate);
            _i = (_i + 1) % _wanderingPoints.Length;
            Master.SetTarget(_wanderingPoints[_i]);
        }
    }
}