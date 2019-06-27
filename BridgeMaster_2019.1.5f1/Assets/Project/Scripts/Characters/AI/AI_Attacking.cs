using Assets.Project.Scripts.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Attacking : Base<AI> {

        [SerializeField] private float _checkRate;
        [SerializeField] private LayerMask _targetLayerMask;

        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackDamage;

        private void OnEnable() {
            StartCoroutine(ChaseIfTargetInRangeRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator ChaseIfTargetInRangeRoutine() {
            while (true) {
                if (Master.IsAttacking && Master.Player != null && !Master.Player.IsDead) {

                    if (Master.State == AIState.Attacking) {

                        var distance = Master.Player.MyTransform.position.x - Master.MyTransform.position.x;

                        if (Mathf.Abs(distance) > _attackRange) {
                            StopAttacking(true);
                        }
                    }
                    else {

                        var hit = Physics2D.OverlapCircle(Master.MyTransform.position + _attackRange * Master.MyTransform.forward,
                            _attackRange, _targetLayerMask);

                        if (hit != null) {
                            Master.State = AIState.Attacking;
                            Master.StartAttack(Master.Player);
                        }
                    }
                }
                yield return new WaitForSeconds(_checkRate);
            }
        }

        private void StopAttacking(bool isChasing) {
            Master.EndAttack(Master.Player);
            Master.SetTarget(null);

            if (isChasing)
                Master.EndChasing();

            if (Master.IsWandering) {
                Master.State = AIState.Wandering;
                Master.StartWandering();
            }
            else {
                Master.State = AIState.None;
                Master.EndRun();
            }
        }

        public void Attack() {
            Master.ChangeEndurance(-10);
            if (Master.Player) {
                Master.Player.ChangeHealth(-_attackDamage);
                if (Master.Player.IsDead)
                    StopAttacking(true);
            }
        }
    }
}