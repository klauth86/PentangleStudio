using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Characters {
    class CharacterMaster_S_Controller : COM_Subscriber<CharacterMaster> {

        private bool _isJumping;

        protected override void Subscribe() {
            Master.StartRunEvent += StartRun;
            Master.StartJumpEvent += StartJump;

            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.StartRunEvent -= StartRun;
            Master.StartJumpEvent -= StartJump;

            base.Unsubscribe();
        }

        private void StartRun(float axis) {
            Master.Rigidbody.velocity = new Vector2(Master.CharacterState.GetVelocity() * axis, Master.Rigidbody.velocity.y);
            Swap(axis);
        }

        void Swap(float axis) {
            if (Mathf.Abs(axis) > float.Epsilon)
                transform.localScale = new Vector3(Mathf.Sign(axis), 1, 1);
        }

        private void StartJump() {
            if (!_isJumping) {
                _isJumping = true;
                Master.Rigidbody.velocity = new Vector2(Master.Rigidbody.velocity.x, Master.CharacterState.GetJump());
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