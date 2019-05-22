using UnityEngine;

namespace Base {
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class CharacterWithPhysics : CharacterWithAnimation {
        protected Collider2D _collider;
        protected Rigidbody2D _rigidbody;

        #region Ctor

        public CharacterWithPhysics() {
            OnStart += () => {
                _collider = GetComponentInChildren<Collider2D>();
                _rigidbody = GetComponent<Rigidbody2D>();
            };

            OnPause += () => {
                FreezePosition();
            };

            OnUnpause += () => {
                UnfreezePosition();
            };
        }

        #endregion

        protected void FreezePosition() {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        protected void UnfreezePosition() {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}