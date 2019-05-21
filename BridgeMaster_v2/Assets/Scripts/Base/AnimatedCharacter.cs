using UnityEngine;

namespace Base {
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatedCharacter : CharacterBase {
        protected Animator _animator;

        protected override void Init() {
            _animator = GetComponent<Animator>();
        }
    }
}