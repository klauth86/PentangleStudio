using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.Stat {
    class Character_StatHealth : Base<Character_Master> {
        [SerializeField] private float _max = 100;
        [SerializeField] private float _health = 100;

        [SerializeField] private float _recoveryRate = 1;
        [SerializeField] private float _recoveryAmount = 2;

        private Coroutine _recoveryCoroutine;

        protected override void Subscribe() {
            Master.ChangeHealthEvent += ChangeHealth;
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.ChangeHealthEvent -= ChangeHealth;
            StopAllCoroutines();
            _recoveryCoroutine = null;
            base.Unsubscribe();
        }

        private IEnumerator RecoveryRoutine() {
            while (_health < _max && !Master.IsFreezed) {
                yield return new WaitForSeconds(_recoveryRate);
                ChangeHealth(_recoveryAmount);
            }

            _recoveryCoroutine = null;
        }

        private void ChangeHealth(float amount) {
            _health += amount;

            if (_health > _max)
                _health = _max;

            else if (_health <= 0) {
                _health = 0;
            }
            else if (_health < _max && _recoveryCoroutine == null) {
                _recoveryCoroutine = StartCoroutine(RecoveryRoutine());
            }

            Master.HealthChanged(_health, _max);
            if (_health == 0)
                Master.Die();
        }
    }
}