using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Characters {
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Character_Master : EventRoot {

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

        public event EventHandler<float> ChangeManaEvent;
        public event EventHandler<float, float> ManaChangedEvent;

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

        public event EventHandler<Character_Master> StartAttackEvent;
        public event EventHandler<Character_Master> EndAttackEvent;

        public event EventHandler StartCastSpellEvent;
        public event EventHandler EndCastSpellEvent;

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

        public void StartAttack(Character_Master target) {
            StartAttackEvent?.Invoke(target);
        }
        public void EndAttack(Character_Master target) {
            EndAttackEvent?.Invoke(target);
        }

        public void StartCastSpell() {
            StartCastSpellEvent?.Invoke();
        }
        public void EndCastSpell() {
            EndCastSpellEvent?.Invoke();
        }

        #endregion


        public event EventHandler DieEvent;

        public void Die() {
            DieEvent?.Invoke();
        }
    }
}