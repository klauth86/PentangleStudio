using System;
using Dicts;
using UnityEngine;

namespace Base {
    public abstract class MobBase: CharacterWithAnimation {
        [Header(Header.Stats)]
        [SerializeField] protected float _velocity;
        [SerializeField] protected bool _attackWithMovement;

        [SerializeField] protected Damage _attackDamage;

        protected bool _isAttacking;
        protected Player _player;

        public Transform Target;

        #region Ctor

        public MobBase() {
            OnUpdate += Walk;
        }

        #endregion

        #region Walk

        private void Walk() {
            if (Target && !_isAttacking) {
                var direction = Mathf.Sign(Target.position.x - transform.position.x);
                transform.position = new Vector3(transform.position.x + _velocity * direction * Time.deltaTime, transform.position.y, transform.position.z);
                _animator.SetBool(AnimatorKey.IsWalking, true);

                if (direction * transform.localScale.x <0) {
                    Swap();
                }
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

        private void OnTriggerStay2D(Collider2D collision) {
            if (collision.gameObject.GetComponent<Player>()) {
                if (_player.HitPoints < 0) {
                    _isAttacking = false;
                    _animator.SetBool(AnimatorKey.IsAttacking, false);
                    _animator.SetBool(AnimatorKey.IsWalking, false);
                    Target = null;
                }
            }
        }
    }
}
