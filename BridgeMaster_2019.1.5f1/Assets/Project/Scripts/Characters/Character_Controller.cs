using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Controller : Base<Master> {
        [SerializeField] private float _velocity;
        [SerializeField] private float _jump;
        private bool _isJumping;

        private void OnEnable() {
            Master.StartRunEvent += StartRun;
            Master.StartJumpEvent += StartJump;
        }

        private void OnDisable() {
            Master.StartRunEvent -= StartRun;
            Master.StartJumpEvent -= StartJump;
        }

        private void StartRun(float axis) {
            if (!Master.IsFreezed) {
                Master.Rigidbody.velocity = new Vector2(Master.Endurance * _velocity * axis, Master.Rigidbody.velocity.y);
                Swap(axis);
            }
        }

        void Swap(float axis) {
            if (Mathf.Abs(axis) > float.Epsilon)
                transform.localScale = new Vector3(Mathf.Sign(axis), 1, 1);
        }

        private void StartJump() {
            if (!Master.IsFreezed) {
                if (!_isJumping) {
                    _isJumping = true;
                    Master.Rigidbody.velocity = new Vector2(Master.Rigidbody.velocity.x, Master.Endurance * _jump);
                }
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