using Dicts;
using UnityEngine;

namespace Base {
    [RequireComponent(typeof(Collider2D))]
    public abstract class MobBase: CharacterWithAnimation {
        [Header(Header.Stats)]
        [SerializeField] private float _velocity;
        [SerializeField] private float _health;

        public Transform Target;

        #region Ctor

        public MobBase() {
            OnUpdate += Walk;
        }

        #endregion

        #region Walk

        private void Walk() {
            if (Target) {
                var direction = Mathf.Sign(Target.position.x - transform.position.x);
                transform.position = new Vector3(transform.position.x + _velocity * direction * Time.deltaTime, transform.position.y, transform.position.z);
                _animator.SetBool(AnimatorKey.IsWalking, true);

                if (direction * transform.localScale.x <0) {
                    Swap();
                }
            }
            else {
                _animator.SetBool(AnimatorKey.IsWalking, false);
            }
        }

        #endregion
    }
}
