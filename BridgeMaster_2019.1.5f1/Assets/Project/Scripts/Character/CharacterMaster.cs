using BridgeMaster.Base;
using BridgeMaster.Engine;
using UnityEngine;

namespace BridgeMaster.Characters {
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public abstract class CharacterMaster : EventRoot {

        public readonly CharacterState CharacterState;

        public CharacterMaster(CharacterState state) {
            CharacterState = state;
        }

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

        #region STATS EVENTS

        public event EventHandler<float> ChangeHealthEvent;
        public event EventHandler<float, float> HealthChangedEvent;

        public event EventHandler<float> ChangeEnduranceEvent;
        public event EventHandler<float, float> EnduranceChangedEvent;

        public void ChangeHealth(float value) {
            ChangeHealthEvent?.Invoke(value);
        }
        public void HealthChanged(float value, float max) {
            HealthChangedEvent?.Invoke(value, max);
        }

        public void ChangeEndurance(float amount) {
            ChangeEnduranceEvent?.Invoke(amount);
        }
        public void EnduranceChanged(float value, float max) {
            EnduranceChangedEvent?.Invoke(value, max);
        }

        #endregion

        #region CHARACTER EVENTS

        public event EventHandler<float> StartRunEvent;
        public event EventHandler EndRunEvent;

        public event EventHandler StartJumpEvent;
        public event EventHandler EndJumpEvent;

        public event EventHandler<CharacterMaster> StartAttackEvent;
        public event EventHandler<CharacterMaster> EndAttackEvent;

        public event EventHandler StartCastSpellEvent;
        public event EventHandler EndCastSpellEvent;

        public event EventHandler DieEvent;

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

        public void Die() {
            DieEvent?.Invoke();
        }

        #endregion
    }
}