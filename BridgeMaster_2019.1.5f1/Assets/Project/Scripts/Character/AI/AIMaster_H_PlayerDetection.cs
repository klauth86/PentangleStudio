﻿using BridgeMaster.Base;
using BridgeMaster.Characters.Player;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AIMaster_H_PlayerDetection : COM_Handler<AIMaster> {

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate = 0.125f;
        [SerializeField] private float _detectionRadius = 7;

        protected override void CallOnEnableEvent() {
            StartCoroutine(PlayerDetectionRoutine());
            base.CallOnEnableEvent();
        }

        protected override void CallOnDisableEvent() {
            StopAllCoroutines();
            base.CallOnDisableEvent();
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

#if UNITY_EDITOR
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(Master.Transform.position, _detectionRadius);
        }
#endif
    }
}
