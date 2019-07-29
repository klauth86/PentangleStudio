using BridgeMaster.Base;

namespace BridgeMaster.Engine {
    public class CharacterState {

        // All below are from [0, 1] interval
        public float HealthState;
        public float ManaState;

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

        public float MinVelocityState;
        public float MinJumpState;

        public float AttackRating;
        public float SpellRating;

        // All below are positive floats
        public float Health;
        public float Mana;
        public float Endurance;

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
    }
}
