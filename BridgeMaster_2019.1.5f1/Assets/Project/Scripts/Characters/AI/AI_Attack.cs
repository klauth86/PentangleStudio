using Assets.Project.Scripts.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Attack: Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private float _attackRange;
        [SerializeField] private LayerMask _targetLayerMask;

        private void OnEnable() {
            StartCoroutine(AttackIfTargetInRangeRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator AttackIfTargetInRangeRoutine() {
            while (true) {
                if (Master.IsAttacking) {
                    var hit = Physics2D.OverlapCircle(Master.MyTransform.position,
                        _chasingRange, _targetLayerMask);

                    if (hit != null) {
                        Master.State = AIState.Chasing;
                        Master.StartChasing();
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
