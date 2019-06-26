using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Chasing: Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private float _chasingRange;

        private void OnEnable() {
            StartCoroutine(AttackIfInRangeRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator AttackIfInRangeRoutine() {
            while (true) {
                var hit = Physics2D.OverlapCircle(Master.MyTransform.position,
                    _chasingRange, _targetLayerMask);

                if (hit != null) {
                    Master.StartChasing();
                    var distance = hit.transform.position.x - Master.MyTransform.position.x;
                    Master.StartRun(Mathf.Sign(distance));
                }
                else {
                    Master.StartWandering();
                }
                yield return new WaitForSeconds(_checkRate);
            }
        }
    }
}
