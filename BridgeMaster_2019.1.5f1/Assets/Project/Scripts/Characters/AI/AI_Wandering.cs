using Assets.Project.Scripts.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Wandering : Base<AI> {
        private int _i;

        [SerializeField] private float _checkRate;
        [SerializeField] private float _reachRange;
        [SerializeField] private Transform[] _points;

        private void OnEnable() {
            StartCoroutine(WanderingRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator WanderingRoutine() {
            while (true) {
                if (Master.IsWandering && _points != null && _points.Length > 0) {

                    if (Master.State == AIState.Wandering) {

                        var point = _points[_i];
                        var distance = point.position.x - Master.MyTransform.position.x;

                        if (Mathf.Abs(distance) < _reachRange) {
                            _i = (_i + 1) % _points.Length;
                            continue;
                        }

                        Master.StartRun(Mathf.Sign(distance));
                    }
                }
                yield return new WaitForSeconds(_checkRate);
            }
        }
    }
}