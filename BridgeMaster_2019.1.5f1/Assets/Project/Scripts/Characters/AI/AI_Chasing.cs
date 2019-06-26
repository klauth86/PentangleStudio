using Assets.Project.Scripts.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Chasing: Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private float _chasingRange;
        [SerializeField] private LayerMask _targetLayerMask;

        private void OnEnable() {
            StartCoroutine(ChaseIfTargetInRangeRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator ChaseIfTargetInRangeRoutine() {
            while (true) {
                if (Master.IsChasing) {

                    if (Master.State == AIState.Chasing) {

                    }
                    else if (Master.State != AIState.Attacking) {
                        var hit = Physics2D.OverlapCircle(Master.MyTransform.position,
                            _chasingRange, _targetLayerMask);

                        if (hit != null) {
                            Master.State = AIState.Chasing;
                            Master.SetTarget(hit.transform);
                        }
                        else {
                            Master.SetTarget(null);
                            if (Master.IsWandering) {
                                Master.State = AIState.Wandering;
                                Master.StartWandering();
                            }
                            else {

                            }
                        }
                    }


                        var distance = hit.transform.position.x - Master.MyTransform.position.x;
                        Master.StartRun(Mathf.Sign(distance));
                    }
                    else {
                        Master.EndChasing();
                    }

                }
                yield return new WaitForSeconds(_checkRate);
            }
        }
    }
}
