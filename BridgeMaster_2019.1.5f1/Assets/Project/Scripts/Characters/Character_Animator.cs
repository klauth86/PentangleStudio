using System;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Animator : Base<Master> {
        private void OnEnable() {
            Master.StartRunEvent += StartRun;
            Master.EndRunEvent += EndRun;

            Master.StartJumpEvent += StartJump;
            Master.EndJumpEvent += EndJump;

            Master.StartCastSpellEvent += StartCastSpell;
            Master.EndCastSpellEvent += EndCastSpell;

            Master.StartAttackEvent += StartAttack;
            Master.EndAttackEvent += EndAttack;

            Master.ChangeHealthEvent += ChangeHealthEvent;

            Master.DieEvent += Die;

            Master.ChangeSpeedEvent += ChangeSpeed;
        }

        private void OnDisable() {
            Master.StartRunEvent -= StartRun;
            Master.EndRunEvent -= EndRun;

            Master.StartJumpEvent -= StartJump;
            Master.EndJumpEvent -= EndJump;

            Master.StartCastSpellEvent -= StartCastSpell;
            Master.EndCastSpellEvent -= EndCastSpell;

            Master.StartAttackEvent -= StartAttack;
            Master.EndAttackEvent -= EndAttack;

            Master.ChangeHealthEvent -= ChangeHealthEvent;

            Master.DieEvent -= Die;

            Master.ChangeSpeedEvent -= ChangeSpeed;
        }

        private void ChangeSpeed(float speed) {
            Master.Animator.speed *= speed;
        }

        private void StartJump() {
            Master.Animator.SetBool(AnimatorKey.IsJumping, true);
        }

        private void EndJump() {
            Master.Animator.SetBool(AnimatorKey.IsJumping, false);
        }

        private void StartRun(float axis) {
            Master.Animator.SetBool(AnimatorKey.IsRunning, axis != 0);
        }

        private void EndRun() {
            Master.Animator.SetBool(AnimatorKey.IsRunning, false);
        }

        private void StartCastSpell() {
            Master.Animator.SetBool(AnimatorKey.IsCastingSpell, true);
        }

        private void EndCastSpell() {
            Master.Animator.SetBool(AnimatorKey.IsCastingSpell, false);
        }

        private void StartAttack(Master target) {
            Master.Animator.SetBool(AnimatorKey.IsAttacking, true);
        }

        private void EndAttack(Master target) {
            Master.Animator.SetBool(AnimatorKey.IsAttacking, false);
        }

        private void Die() {
            Master.Animator.SetTrigger(AnimatorKey.IsDead);
        }

        private void ChangeHealthEvent(float amount) {
            if (amount < 0)
                Debug.Log("Play hurt animation");
        }
    }
}