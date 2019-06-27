using UnityEngine;

namespace BridgeMaster.Characters {
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Master : GameObjectSubscriber<Game.Master> {

        public bool IsDead;
        public bool IsFreezed;

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

        public event GameEventHandler DieEvent;

        public event GameEventHandler<float> ChangeHealthEvent;
        public event GameEventHandler<float, float> HealthChangedEvent;

        public event GameEventHandler<float> ChangeManaEvent;
        public event GameEventHandler<float, float> ManaChangedEvent;

        public event GameEventHandler<float> ChangeIronEvent;
        public event GameEventHandler<float, float> IronChangedEvent;

        public event GameEventHandler<float> ChangeEnduranceEvent;
        public event GameEventHandler<float, float> EnduranceChangedEvent;

        public event GameEventHandler<float> StartRunEvent;
        public event GameEventHandler EndRunEvent;

        public event GameEventHandler StartJumpEvent;
        public event GameEventHandler EndJumpEvent;

        public event GameEventHandler<Master> StartAttackEvent;
        public event GameEventHandler<Master> EndAttackEvent;

        public event GameEventHandler StartCastSpellEvent;
        public event GameEventHandler EndCastSpellEvent;

        public event GameEventHandler ToggleFreezeEvent;

        public void Die() {
            IsDead = true;
            DieEvent?.Invoke();
        }

        public void ChangeHealth(float value) {
            ChangeHealthEvent?.Invoke(value);
        }

        public void HealthChanged(float value, float max) {
            HealthChangedEvent?.Invoke(value, max);
        }

        public void ChangeMana(float value) {
            ChangeManaEvent?.Invoke(value);
        }

        public void ManaChanged(float value, float max) {
            ManaChangedEvent?.Invoke(value, max);
        }

        public void ChangeIron(float value) {
            ChangeIronEvent?.Invoke(value);
        }

        public void IronChanged(float value, float max) {
            IronChangedEvent?.Invoke(value, max);
        }

        public void ChangeEndurance(float amount) {
            ChangeEnduranceEvent?.Invoke(amount);
        }

        public void EnduranceChanged(float value, float max) {
            EnduranceChangedEvent?.Invoke(value, max);
        }

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

        public void StartAttack(Master target) {
            StartAttackEvent?.Invoke(target);
        }

        public void EndAttack(Master target) {
            EndAttackEvent?.Invoke(target);
        }

        public void StartCastSpell() {
            StartCastSpellEvent?.Invoke();
        }

        public void EndCastSpell() {
            EndCastSpellEvent?.Invoke();
        }

        public void ToggleFreeze() {
            ToggleFreezeEvent?.Invoke();
        }
    }
}