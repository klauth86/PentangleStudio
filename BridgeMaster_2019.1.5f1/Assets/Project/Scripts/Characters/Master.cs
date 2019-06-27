using UnityEngine;

namespace BridgeMaster.Characters {
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
    public class Master : GameObjectSubscriber<Game.Master> {
        public bool IsDead;
        public bool IsFreezed;
        public float Speed = 1;

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
        public event GameEventHandler<float> ChangeManaEvent;
        public event GameEventHandler<float> ChangeIronEvent;
        public event GameEventHandler<float> ChangeSpeedEvent;

        public event GameEventHandler<float> StartRunEvent;
        public event GameEventHandler EndRunEvent;

        public event GameEventHandler StartJumpEvent;
        public event GameEventHandler EndJumpEvent;

        public event GameEventHandler<Master> StartAttackEvent;
        public event GameEventHandler<Master> EndAttackEvent;

        public event GameEventHandler StartCastSpellEvent;
        public event GameEventHandler EndCastSpellEvent;

        public event GameEventHandler FreezeEvent;
        public event GameEventHandler UnfreezeEvent;

        public void Die() {
            DieEvent?.Invoke();
            IsDead = true;
            enabled = false;
        }

        public void ChangeHealth(float value) {
            ChangeHealthEvent?.Invoke(value);
        }

        public void ChangeMana(float value) {
            ChangeManaEvent?.Invoke(value);
        }

        public void ChangeIron(float value) {
            ChangeIronEvent?.Invoke(value);
        }

        public void ChangeSpeed(float value) {
            Speed *= value;
            ChangeSpeedEvent?.Invoke(Speed);
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

        public void Freeze() {
            FreezeEvent?.Invoke();
        }

        public void Unfreeze() {
            UnfreezeEvent?.Invoke();
        }
    }
}