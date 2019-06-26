using Assets.Project.Scripts.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Wandering : Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private float _wanderingPrecision;
        [SerializeField] private Transform[] _pointsOfWandering;

        private void OnEnable() {
            StartCoroutine(WanderingRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private void OnStartWanderingEvent() {
            StartCoroutine(WanderingRoutine());
        }

        private void OnEndWanderingEvent() {
            StopAllCoroutines();
        }

        private IEnumerator WanderingRoutine() {
            int i = 0;
            while (true) {
                if (_pointsOfWandering != null && _pointsOfWandering.Length > 0 && Master.State == AIState.Wandering) {

                    var point = _pointsOfWandering[i];
                    var distance = point.position.x - Master.MyTransform.position.x;

                    if (Mathf.Abs(distance) < _wanderingPrecision) {
                        i = (i + 1) % _pointsOfWandering.Length;
                        continue;
                    }

                    Master.StartRun(Mathf.Sign(distance));
                    yield return new WaitForSeconds(_checkRate);
                }
            }
        }
    }
}
