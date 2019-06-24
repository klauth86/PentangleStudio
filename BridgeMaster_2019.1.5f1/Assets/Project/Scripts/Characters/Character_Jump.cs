using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Jump : Character_Base<Master> {
        [SerializeField] private float _jump;
        private bool _isJumping;

        private void OnEnable() {
            Master.StartJumpEvent += StartJump;
            Master.EndJumpEvent += EndJump;
        }

        private void OnDisable() {
            Master.StartJumpEvent -= StartJump;
            Master.EndJumpEvent -= EndJump;
        }

        private void StartJump() {
            _isJumping = true;
            Master.Rigidbody.velocity = new Vector2(Master.Rigidbody.velocity.x, _jump);
            Master.Animator.SetBool(AnimatorKey.IsJumping, true);
        }

        private void EndJump() {
            //throw new NotImplementedException();
        }

        private void OnCollisionEnter2D(Collision2D collision) {
            if (_isJumping) {
                _isJumping = false;
                Master.Animator.SetBool(AnimatorKey.IsJumping, false);
            }
        }
    }
}