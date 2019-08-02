using BridgeMaster.Base;
using BridgeMaster.Engine;
using UnityEngine;

namespace BridgeMaster.Characters {
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Collider2D))]
    public abstract class CharacterMaster : EventRoot {

        public readonly CharacterState CharacterState;

        #region CTOR

        public CharacterMaster(CharacterState state) {
            CharacterState = state;
        }

        #endregion

        private Transform _myTransform;
        public Transform MyTransform {
            get {
                return _myTransform ?? (_myTransform = GetComponent<Transform>());
            }
        }

        private Rigidbody2D _rigidbody;
        public Rigidbody2D Rigidbody {
            get {
                return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>());
            }
        }

        private Animator _animator;
        public Animator Animator {
            get {
                return _animator ?? (_animator = GetComponent<Animator>());
            }
        }

        #region CHARACTER EVENTS

        public event EventHandler<float> StartRunEvent;
        public event EventHandler EndRunEvent;

        public event EventHandler StartJumpEvent;
        public event EventHandler EndJumpEvent;

        public event EventHandler<CharacterMaster> StartAttackEvent;
        public event EventHandler<CharacterMaster> EndAttackEvent;

        public event EventHandler StartCastSpellEvent;
        public event EventHandler EndCastSpellEvent;

        public event EventHandler GetHurtEvent;

        public void StartRun(float axis) {
            StartRunEvent?.Invoke(axis);
        }
        public void EndRun() {
            EndRunEvent?.Invoke();
        }

        public void StartJump() {
            StartJumpEvent?.Invoke();
        }
        public void EndJump() {
            EndJumpEvent?.Invoke();
        }

        public void StartAttack(CharacterMaster target) {
            StartAttackEvent?.Invoke(target);
        }
        public void EndAttack(CharacterMaster target) {
            EndAttackEvent?.Invoke(target);
        }

        public void StartCastSpell() {
            StartCastSpellEvent?.Invoke();
        }
        public void EndCastSpell() {
            EndCastSpellEvent?.Invoke();
        }

        public void GetHurt() {
            GetHurtEvent?.Invoke();
        }

        #endregion
    }
}