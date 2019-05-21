using UnityEngine;

namespace Base {
    [RequireComponent(typeof(Animator))]
    public abstract class CharacterWithAnimation : CharacterBase {
        protected Animator _animator;

        public CharacterWithAnimation() {
            OnStart += () => {
                _animator = GetComponent<Animator>();
            };
        }
    }
}