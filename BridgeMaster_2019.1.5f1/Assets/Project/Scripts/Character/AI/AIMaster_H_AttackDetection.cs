using BridgeMaster.Base;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AIMaster_H_AttackDetection : COM_Handler<AIMaster> {

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate = 0.125f;

        [SerializeField] private float _isReadyForAttackRadius = 7;

        protected override void CallOnEnableEvent() {
            StartCoroutine(AttackRoutine());
            base.CallOnEnableEvent();
        }

        protected override void CallOnDisableEvent() {
            StopAllCoroutines();
            base.CallOnDisableEvent();
        }

        private IEnumerator AttackRoutine() {

            yield return new WaitForSeconds(Random.Range(0, _checkRate));

            while (true) {
                if (Physics2D.Raycast(Master.Transform.position, -Master.Transform.right, _isReadyForAttackRadius / 2, _playerLayer)) {
                    Master.Flip();
                    Master.IsReadyForAttack = true;
                }
                else {
                    Master.IsReadyForAttack = Physics2D.Raycast(Master.Transform.position, Master.Transform.right, _isReadyForAttackRadius, _playerLayer);
                }
                yield return new WaitForSeconds(_checkRate);
            }
        }

        #region GIZMO
#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.DrawLine(Master.Transform.position, Master.Transform.position + Master.Transform.right * _isReadyForAttackRadius);
            Gizmos.DrawLine(Master.Transform.position, Master.Transform.position - Master.Transform.right * _isReadyForAttackRadius / 2);
        }
#endif
        #endregion
    }
}