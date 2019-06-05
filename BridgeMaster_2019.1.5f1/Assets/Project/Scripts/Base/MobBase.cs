using BridgeMaster.Dicts;
using UnityEngine;

namespace Base {
    public abstract class MobBase: CharacterWithPhysics {
        [Header(Header.Stats)]
        [SerializeField] protected float _velocity;
        [SerializeField] protected bool _attackWithMovement;

        [SerializeField] protected Damage _attackDamage;

        protected bool _isAttacking;
        protected Player _player;

        public Transform Target;

        #region Ctor

        public MobBase() {
            OnStart += () => {
                OnUpdate += Walk;
            };

            OnPause += ()=>{
                OnUpdate -= Walk;
            };
            OnUnpause += () => {
                OnUpdate += Walk;
            };
        }

        #endregion

        #region Walk

        private void Walk() {
            var direction = 0.0f;
            if (Target) {
                direction = Mathf.Sign(Target.position.x - transform.position.x);
            }

            var isWalking = !_isAttacking && direction != 0.0f;
            _rigidbody.velocity = isWalking ? new Vector3(_velocity * direction, 0, 0) : Vector3.zero;
            _animator.SetBool(AnimatorKey.IsWalking, isWalking);

            if (direction * transform.localScale.x < 0) {
                Swap();
            }
        }

        #endregion

        private void OnTriggerEnter2D(Collider2D collision) {
            _player = collision.gameObject.GetComponent<Player>();
            if (_player) {
                if (!_attackWithMovement)
                    _isAttacking = true;
                _animator.SetBool(AnimatorKey.IsAttacking, true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<Player>()) {
                _isAttacking = false;
                _animator.SetBool(AnimatorKey.IsAttacking, false);
            }
        }
    }
}
