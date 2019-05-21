using UnityEngine;

namespace Base {
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalCharacter : AnimatedCharacter {
        protected Collider2D _collider;
        protected Rigidbody2D _rigidbody;

        protected override void Init() {
            base.Init();
            _collider = GetComponentInChildren<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}