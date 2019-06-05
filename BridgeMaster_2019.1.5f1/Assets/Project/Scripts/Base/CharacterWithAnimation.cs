using UnityEngine;

namespace Base {
    [RequireComponent(typeof(Animator))]
    public abstract class CharacterWithAnimation : CharacterBase {
        protected Animator _animator;

        #region Ctor

        public CharacterWithAnimation() {
            OnStart += () => {
                _animator = GetComponent<Animator>();
            };

            OnPause += () => {
                SetAnimationSpeed(0);
            };

            OnUnpause += () => {
                SetAnimationSpeed(1);
            };
        }

        #endregion

        protected void SetAnimationSpeed(float speed) {
            _animator.speed = speed;
        }
    }
}