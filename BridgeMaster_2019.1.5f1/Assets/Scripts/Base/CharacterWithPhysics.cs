using System;
using UnityEngine;

namespace Base {
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class CharacterWithPhysics : CharacterWithAnimation {
        protected Collider2D _collider;
        protected Rigidbody2D _rigidbody;

        protected event Action OnFixedUpdate = delegate { };

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

        private void FixedUpdate() {
            OnFixedUpdate();
        }

        protected void FreezePosition() {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        protected void UnfreezePosition() {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}