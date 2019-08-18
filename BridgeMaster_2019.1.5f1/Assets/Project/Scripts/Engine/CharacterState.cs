using BridgeMaster.Base;

namespace BridgeMaster.Engine {
    public class CharacterState {

        // All below are from [0, 1] interval
        public float EnduranceStateRecovery;
        public event EventHandler<float> EnduranceStateChangedEvent;
        private float _enduranceState;
        public float EnduranceState {
            get {
                return _enduranceState;
            }
            set {
                value = Norm(value);

                _enduranceState = value;
                EnduranceStateChangedEvent?.Invoke(value);
            }
        }

        public float HealthStateRecovery;
        public event EventHandler<float> HealthStateChangedEvent;
        private float _healthState;
        public float HealthState {
            get {
                return _healthState;
            }
            set {
                value = Norm(value);

                _healthState = value;
                HealthStateChangedEvent?.Invoke(value);

                if (_healthState == 0)
                    DieEvent?.Invoke();
            }
        }

        public float ManaStateRecovery;
        public event EventHandler<float> ManaStateChangedEvent;
        private float _manaState;
        public float ManaState {
            get {
                return _manaState;
            }
            set {
                value = Norm(value);

                _manaState = value;
                ManaStateChangedEvent?.Invoke(value);
            }
        }

        public float MinVelocityState;

        public float MinJumpState;

        public float MinAttackState;

        public float MinSpellState;

        // All below are positive floats to describe max values
        public float Velocity;
        public float VelocityCost;
        public float GetVelocityState() {
            EnduranceState = (Endurance * EnduranceState - VelocityCost) / Endurance;
            return MathFacade.Max(MinVelocityState, 2 * EnduranceState / (EnduranceState + 1));
        }
        public float GetVelocity() {
            return Velocity * GetVelocityState();
        }

        public float Jump;
        public float JumpCost;
        public float GetJumpState() {
            EnduranceState = (Endurance * EnduranceState - VelocityCost) / Endurance;
            return MathFacade.Max(MinJumpState, 2 * EnduranceState / (EnduranceState + 1));
        }
        public float GetJump() {
            return Jump * GetJumpState();
        }

        public float Attack;
        public float AttackCost;
        public float GetAttackState() {
            EnduranceState = (Endurance * EnduranceState - AttackCost) / Endurance;
            return MathFacade.Max(MinAttackState, 2 * EnduranceState / (EnduranceState + 1));
        }
        public float GetAttack() {
            return Attack * GetAttackState();
        }

        public float Endurance;
        public void ChangeEndurance(float amount) {
            EnduranceState = (Endurance * EnduranceState + amount) / Endurance;
        }

        public float Health;
        public void ChangeHealth(float amount) {
            HealthState = (Health * HealthState + amount) / Health;
        }

        public float Mana;
        public void ChangeMana(float amount) {
            ManaState = (Mana * ManaState + amount) / Mana;
        }

        public event EventHandler DieEvent;

        public void Recover() {
            EnduranceState += EnduranceStateRecovery;
            HealthState += HealthStateRecovery;
            ManaState += ManaStateRecovery;
        }

        private float Norm(float value) {
            if (value < 0)
                return 0;

            if (value > 1)
                return 1;

            return value;
        }
    }
}
