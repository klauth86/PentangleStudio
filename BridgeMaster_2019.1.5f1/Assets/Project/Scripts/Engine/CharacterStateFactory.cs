﻿namespace BridgeMaster.Engine {
    public static class CharacterStateFactory {

        public static CharacterState GetPlayerState() {
            return new CharacterState() {
                EnduranceStateRecovery = 1f/100,
                EnduranceState = 1,

                HealthStateRecovery = 1f/1200,
                HealthState = 1,

                ManaStateRecovery = 1f / 600,
                ManaState = 1,

                MinVelocityState = 0.35f,
                MinJumpState = 0.35f,
                MinAttackState = 0.35f,
                MinSpellState = 0.35f,

                Velocity = 7,
                VelocityCost = 1f,

                Jump = 10,
                JumpCost = 20,

                Attack = 10,
                AttackCost = 50,            

                Health = 100,
                Mana = 100,
                Endurance = 200,
            };
        }

        public static CharacterState GetMob01State() {
            return new CharacterState() {
                EnduranceState = 1,
                HealthState = 1,
                ManaState = 1,

                MinVelocityState = 0.35f,
                MinJumpState = 0.35f,
                MinAttackState = 0.35f,
                MinSpellState = 0.35f,

                Velocity = 7,
                VelocityCost = 0.01f,

                Jump = 10,
                JumpCost = 1,

                Attack = 40,
                AttackCost = 5,

                Health = 150,
                Endurance = 200,
            };
        }
    }
}