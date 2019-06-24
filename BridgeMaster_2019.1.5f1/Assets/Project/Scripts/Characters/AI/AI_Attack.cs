using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Attack : Character_Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private LayerMask _targetLayerMask;
        [SerializeField] private float _attackRange;

        private void OnEnable() {
            Master.EnterAttackRangeEvent += OnEnterAttackRangeEvent;
        }

        private void OnDisable() {
            Master.LeftAttackRangeEvent += OnLeftAttackRangeEvent;
        }

        private void OnEnterAttackRangeEvent() {
            StartCoroutine(AttackIfInRangeRoutine());
        }

        private void OnLeftAttackRangeEvent() {
            StopAllCoroutines();
        }

        private IEnumerator AttackIfInRangeRoutine() {
            while (true) {
                var hit = Physics2D.OverlapCapsule(Master.MyTransform.position + Master.MyTransform.forward * _attackRange / 2,
                    new Vector2(_attackRange, _attackRange / 2),
                    CapsuleDirection2D.Horizontal, 0, _targetLayerMask);

                if (hit != null) {
                    Master.StartAttack();
                }
                else {
                    Master.EndAttack();
                }

                yield return new WaitForSeconds(_checkRate);
            }
        }
    }
}
