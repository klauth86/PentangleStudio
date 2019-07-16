using BridgeMaster.Base;
using BridgeMaster.Characters.Stat;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Controller : COM_Subscriber<Character_Master> {
        [SerializeField] private float _velocity;
        [SerializeField] private float _jump;

        [SerializeField] private float _runEnduranceCost = 5;
        [SerializeField] private float _jumpEnduranceCost = 10;

        private float _enduranceKoefficient = 1.0f;

        private bool _isJumping;

        protected override void Subscribe() {
            Master.StartRunEvent += StartRun;
            Master.StartJumpEvent += StartJump;
            Master.EnduranceChangedEvent += EnduranceChanged;

            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.StartRunEvent -= StartRun;
            Master.StartJumpEvent -= StartJump;
            Master.EnduranceChangedEvent -= EnduranceChanged;

            base.Unsubscribe();
        }

        private void EnduranceChanged(float value, float max) {
            _enduranceKoefficient = Character_StatEndurance.EnduranceKoefficient(value, max);
        }

        private void StartRun(float axis) {
            Master.ChangeEndurance(-_runEnduranceCost * Mathf.Abs(axis));
            Master.Rigidbody.velocity = new Vector2(_enduranceKoefficient * _velocity * axis, Master.Rigidbody.velocity.y);
            Swap(axis);
        }

        void Swap(float axis) {
            if (Mathf.Abs(axis) > float.Epsilon)
                transform.localScale = new Vector3(Mathf.Sign(axis), 1, 1);
        }

        private void StartJump() {
            if (!_isJumping) {
                _isJumping = true;
                Master.ChangeEndurance(-_jumpEnduranceCost);
                Master.Rigidbody.velocity = new Vector2(Master.Rigidbody.velocity.x, _enduranceKoefficient * _jump);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (_isJumping) {
                _isJumping = false;
                Master.EndJump();
            }
        }
    }
}