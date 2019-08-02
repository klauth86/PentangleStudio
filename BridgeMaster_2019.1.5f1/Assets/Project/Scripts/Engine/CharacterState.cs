using BridgeMaster.Base;

namespace BridgeMaster.Engine {
    public class CharacterState {

        // All below are from [0, 1] interval
        public event EventHandler<float> EnduranceStateChangedEvent;
        private float _enduranceState;
        public float EnduranceState {
            get {
                return _enduranceState;
            }
            set {
                _enduranceState = value;
                EnduranceStateChangedEvent?.Invoke(value);
            }
        }

        public event EventHandler<float> HealthStateChangedEvent;
        private float _healthState;
        public float HealthState {
            get {
                return _healthState;
            }
            set {
                _healthState = MathFacade.Min(value, 0);
                HealthStateChangedEvent?.Invoke(value);

                if (_healthState == 0)
                    DieEvent?.Invoke();
            }
        }

        public event EventHandler<float> ManaStateChangedEvent;
        private float _manaState;
        public float ManaState {
            get {
                return _manaState;
            }
            set {
                _manaState = value;
                ManaStateChangedEvent?.Invoke(value);
            }
        }

        public float MinVelocityState;
        public float MinJumpState;

        public float AttackRating;
        public float SpellRating;

        // All below are positive floats to describe max values
        public float Endurance;
        public float Health;
        public float Mana;

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

        public event EventHandler DieEvent; 
    }
}
