using BridgeMaster.Characters.Player;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AIMaster_S_PlayerDetection : CharacterMaster_Subscriber<AIMaster> {

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate = 0.125f;
        [SerializeField] private float _detectionRadius = 4;

        #region EVENTS

        protected override void Subscribe() {
            StartCoroutine(PlayerDetectionRoutine());
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            StopAllCoroutines();
            base.Unsubscribe();
        }

        private IEnumerator PlayerDetectionRoutine() {

            yield return new WaitForSeconds(Random.Range(0, _checkRate));

            while (true) {

                var hit = Physics2D.CircleCast(Master.Transform.position, _detectionRadius, Vector2.zero, 0, _playerLayer);
                Master.Target = hit
                    ? hit.transform.GetComponent<PlayerMaster>()
                    : null;

                yield return new WaitForSeconds(_checkRate);
            }
        }

        #endregion

        #region GIZMO
#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(Master.Transform.position, _detectionRadius);
        }
#endif
        #endregion
    }
}
