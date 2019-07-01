using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.Stat {
    class Character_StatMana : Base<Character_Master> {
        [SerializeField] private float _max;
        [SerializeField] private float _mana;

        [SerializeField] private float _recoveryRate;
        [SerializeField] private float _recoveryAmount;

        private Coroutine _recoveryCoroutine;

        private void OnEnable() {
            Master.ChangeHealthEvent += ChangeMana;
        }

        private void OnDisable() {
            Master.ChangeHealthEvent -= ChangeMana;
            StopAllCoroutines();
            _recoveryCoroutine = null;
        }

        private IEnumerator RecoveryRoutine() {
            while (_mana < _max && !Master.IsFreezed) {
                yield return new WaitForSeconds(_recoveryRate);
                ChangeMana(_recoveryAmount);
            }

            _recoveryCoroutine = null;
        }

        private void ChangeMana(float amount) {
            _mana += amount;

            if (_mana > _max)
                _mana = _max;

            else if (_mana <= 0) {
                _mana = 0;
            }
            else if (_mana < _max && _recoveryCoroutine == null) {
                _recoveryCoroutine = StartCoroutine(RecoveryRoutine());
            }

            Master.HealthChanged(_mana, _max);
        }
    }
}