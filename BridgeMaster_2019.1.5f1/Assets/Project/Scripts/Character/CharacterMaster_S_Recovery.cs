using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters {
    class CharacterMaster_S_Recovery : CharacterMaster_Subscriber<CharacterMaster> {

        [SerializeField] private float _checkRate = 0.25f;

        #region EVENTS

        protected override void Subscribe() {
            StartCoroutine(AttackRoutine());
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            StopAllCoroutines();
            base.Unsubscribe();
        }

        private IEnumerator AttackRoutine() {

            yield return new WaitForSeconds(Random.Range(0, _checkRate));

            while (true) {
                Master.CharacterState.Recover();
                yield return new WaitForSeconds(_checkRate);
            }
        }

        #endregion
    }
}