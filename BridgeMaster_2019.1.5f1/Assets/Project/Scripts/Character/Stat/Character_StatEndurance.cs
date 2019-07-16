using BridgeMaster.Base;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters.Stat {
    class Character_StatEndurance : COM_Subscriber<Character_Master> {
        [SerializeField] private float _max = 100;
        [SerializeField] private float _endurance = 100;

        [SerializeField] private float _recoveryRate = 0.1f;
        [SerializeField] private float _recoveryAmount= 1;

        private Coroutine _recoveryCoroutine;

        protected override void Subscribe() {
            Master.ChangeEnduranceEvent += ChangeEndurance;
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.ChangeEnduranceEvent -= ChangeEndurance;
            StopAllCoroutines();
            _recoveryCoroutine = null;
            base.Unsubscribe();
        }

        private IEnumerator RecoveryRoutine() {
            while (_endurance < _max) {
                yield return new WaitForSeconds(_recoveryRate);
                ChangeEndurance(_recoveryAmount);
            }

            _recoveryCoroutine = null;
        }

        private void ChangeEndurance(float amount) {
            _endurance += amount * (1 + (_max - _endurance) / _max);

            if (_endurance > _max)
                _endurance = _max;

            if (_endurance < 0)
                _endurance = 0;

            if (_endurance < _max && _recoveryCoroutine == null) {
                _recoveryCoroutine = StartCoroutine(RecoveryRoutine());
            }

            Master.EnduranceChanged(_endurance, _max);
        }

        public static float EnduranceKoefficient(float value, float max) {
            var x = value / max;
            return Mathf.Max(2 * x / (1 + x), 0.2f);
        }
    }
}