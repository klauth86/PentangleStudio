using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Characters {
    class CharacterMaster_S_Controller : COM_Subscriber<CharacterMaster> {

        private bool _isJumping;

        #region EVENTS

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.StartRunEvent += StartRun;
                Master.StartJumpEvent += StartJump;
                Master.FlipEvent += Flip;

                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.StartRunEvent -= StartRun;
                Master.StartJumpEvent -= StartJump;
                Master.FlipEvent -= Flip;

                base.Unsubscribe();
            }
        }

        private void StartRun(float axis) {
            Master.Rigidbody.velocity = new Vector2(Master.CharacterState.GetVelocity() * axis, Master.Rigidbody.velocity.y);
            if (axis * Master.Transform.right.x < 0)
                Flip();
        }

        private void Flip() {
            Master.Transform.Rotate(Vector3.up, 180);
        }

        private void StartJump() {
            if (!_isJumping) {
                _isJumping = true;
                Master.Rigidbody.velocity = new Vector2(Master.Rigidbody.velocity.x, Master.CharacterState.GetJump());
            }
        }

        #endregion

        private void OnCollisionEnter2D(Collision2D collision) {
            if (_isJumping) {
                _isJumping = false;
                Master.EndJump();
            }
        }
    }
}