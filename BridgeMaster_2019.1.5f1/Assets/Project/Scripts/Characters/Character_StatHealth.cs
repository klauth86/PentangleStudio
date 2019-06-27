using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_StatHealth : Base<Master> {
        [SerializeField] private float _max;
        [SerializeField] private float _health;

        [SerializeField] private float _recoveryRate;
        [SerializeField] private float _recoveryAmount;

        private Coroutine _recoveryCoroutine;

        private void OnEnable() {
            Master.ChangeHealthEvent += ChangeHealth;
        }

        private void OnDisable() {
            Master.ChangeHealthEvent -= ChangeHealth;
            StopAllCoroutines();
            _recoveryCoroutine = null;
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

            else if (_health < 0) {
                _health = 0;
                Master.Die();
            }
            else if (_health < _max && _recoveryCoroutine == null) {
                _recoveryCoroutine = StartCoroutine(RecoveryRoutine());
            }

            Master.HealthChanged(_health, _max);
        }
    }
}