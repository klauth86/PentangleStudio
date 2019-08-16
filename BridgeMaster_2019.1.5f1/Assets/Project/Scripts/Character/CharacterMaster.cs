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

        private Transform _transform;
        public Transform Transform {
            get {
                return _transform ?? (_transform = GetComponent<Transform>());
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

        #region MASTER EVENTS

        public event EventHandler<float> StartRunEvent;
        public void StartRun(float axis) {
            StartRunEvent?.Invoke(axis);
        }

        public event EventHandler EndRunEvent;
        public void EndRun() {
            EndRunEvent?.Invoke();
        }



        public event EventHandler StartJumpEvent;
        public void StartJump() {
            StartJumpEvent?.Invoke();
        }

        public event EventHandler EndJumpEvent;
        public void EndJump() {
            EndJumpEvent?.Invoke();
        }



        public event EventHandler StartAttackEvent;
        public void StartAttack() {
            StartAttackEvent?.Invoke();
        }

        public event EventHandler EndAttackEvent;
        public void EndAttack() {
            EndAttackEvent?.Invoke();
        }



        public event EventHandler StartCastSpellEvent;
        public void StartCastSpell() {
            StartCastSpellEvent?.Invoke();
        }

        public event EventHandler EndCastSpellEvent;
        public void EndCastSpell() {
            EndCastSpellEvent?.Invoke();
        }



        public event EventHandler GetHurtEvent;
        public void GetHurt() {
            GetHurtEvent?.Invoke();
        }

        public event EventHandler FlipEvent;
        public void Flip() {
            FlipEvent?.Invoke();
        }

        #endregion
    }
}