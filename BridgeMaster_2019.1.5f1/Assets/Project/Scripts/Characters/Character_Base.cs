using UnityEngine;

namespace BridgeMaster.Characters {
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public abstract class Character_Base : ComponentSubscriber<Master> {
        private Rigidbody2D _rigidbody;
        protected Rigidbody2D Rigidbody {
            get {
                return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>());
            }
        }

        private Animator _animator;
        protected Animator Animator {
            get {
                return _animator ?? (_animator = GetComponent<Animator>());
            }
        }

        private void Awake() {
            Master.DieEvent += DisableIfDie;
        }

        void DisableIfDie() {
            Master.DieEvent -= DisableIfDie;
            enabled = false;
        }
    }
}
