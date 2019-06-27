﻿using Assets.Project.Scripts.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Chasing: Base<AI> {

        [SerializeField] private float _checkRate;
        [SerializeField] private LayerMask _targetLayerMask;

        [SerializeField] private float _chasingRange;

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

                        var distance = Master.Player.MyTransform.position.x - Master.MyTransform.position.x;

                        if (Mathf.Abs(distance) > _chasingRange) {
                            StopChasing(true);
                        }
                    }
                    else if (Master.State != AIState.Attacking) {

                        var hit = Physics2D.OverlapCircle(Master.MyTransform.position,
                            _chasingRange, _targetLayerMask);

                        if (hit != null) {
                            StartChasing(hit.transform);
                        }
                    }
                }
                yield return new WaitForSeconds(_checkRate);
            }
        }

        private void StartChasing(Transform target) {
            Master.SetTarget(target);
            Master.StartChasing();
            Master.State = AIState.Chasing;
        }

        private void StopChasing(bool isChasing) {
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
    }
}
