using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Chasing: Character_Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private float _chasingRange;

        private void OnEnable() {
            Master.StartChasingEvent += OnStartChasingEvent;
        }

        private void OnDisable() {
            Master.EndChasingEvent += OnEndChasingEvent;
        }

        private void OnStartChasingEvent() {
            StartCoroutine(AttackIfInRangeRoutine());
        }

        private void OnEndChasingEvent() {
            StopAllCoroutines();
        }

        private IEnumerator AttackIfInRangeRoutine() {
            while (true) {
                var hit = Physics2D.OverlapCircle(Master.MyTransform.position,
                    _chasingRange, _targetLayerMask);

                if (hit != null) {
                    Master.StopWandering();

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
