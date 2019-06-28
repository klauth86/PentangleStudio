using System.Collections;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_StatEndurance : Base<Master> {
        [SerializeField] private float _max;
        [SerializeField] private float _endurance;

        [SerializeField] private float _recoveryRate;
        [SerializeField] private float _recoveryAmount;

        private Coroutine _recoveryCoroutine;

        private void OnEnable() {
            Master.ChangeEnduranceEvent += ChangeEndurance;
        }

        private void OnDisable() {
            Master.ChangeEnduranceEvent -= ChangeEndurance;
            StopAllCoroutines();
            _recoveryCoroutine = null;
        }

        private IEnumerator RecoveryRoutine() {
            while (_endurance < _max && !Master.IsFreezed) {
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