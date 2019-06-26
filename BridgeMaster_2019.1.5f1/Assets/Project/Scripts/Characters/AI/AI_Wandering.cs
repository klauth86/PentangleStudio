using Assets.Project.Scripts.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Wandering : Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private float _reachRange;
        [SerializeField] private Transform[] _points;

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
                if (Master.State == AIState.Wandering && _points != null && _points.Length > 0) {

                    var point = _points[i];
                    var distance = point.position.x - Master.MyTransform.position.x;

                    if (Mathf.Abs(distance) < _reachRange) {
                        i = (i + 1) % _points.Length;
                        continue;
                    }

                    Master.StartRun(Mathf.Sign(distance));
                }
                yield return new WaitForSeconds(_checkRate);
            }
        }
    }
}
