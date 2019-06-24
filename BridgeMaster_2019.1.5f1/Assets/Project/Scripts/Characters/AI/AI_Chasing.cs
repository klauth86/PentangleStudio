using System;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Chasing: Character_Base<AI> {
        [SerializeField] private float _checkRate;
        [SerializeField] private float _chaseRange;

        private void OnEnable() {
            Master.StartChasingTargetEvent += OnStartChasingTargetEvent;
            Master.EndChasingTargetEvent += OnEndChasingTargetEvent;
        }

        private void OnDisable() {
            Master.StartChasingTargetEvent -= OnStartChasingTargetEvent;
            Master.EndChasingTargetEvent -= OnEndChasingTargetEvent;
        }

        private void OnStartChasingTargetEvent(Transform target) {
            StartCoroutine(ChasingRoutine(target))
        }

        private void OnEndChasingTargetEvent() {
            StopAllCoroutines();
        }

        private IEnumerator ChasingRoutine(Transform target) {
            throw new NotImplementedException();
        }
    }
}
