namespace BridgeMaster.Engine {
    public static class CharacterStateFactory {

        public static CharacterState GetPlayerState() {
            return new CharacterState() {
                EnduranceState = 1,
                HealthState = 1,
                ManaState = 1,

                MinVelocityState = 0.35f,
                MinJumpState = 0.35f,

                AttackRating = 1,
                SpellRating = 1,

                Health = 100,
                Mana = 100,
                Endurance = 200,

                Velocity = 7,
                VelocityCost = 0.01f,

                Jump = 5,
                JumpCost = 1,
            };
        }

        public static CharacterState GetMob01State() {
            return new CharacterState();
        }
    }
}