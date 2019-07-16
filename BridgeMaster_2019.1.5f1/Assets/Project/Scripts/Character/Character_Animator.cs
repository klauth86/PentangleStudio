using BridgeMaster.Base;
using BridgeMaster.Characters.Stat;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Animator : COM_Subscriber<Character_Master> {

        private float _prevSpeed = -1;

        protected override void Subscribe() {
            Master.StartRunEvent += StartRun;
            Master.EndRunEvent += EndRun;

            Master.StartJumpEvent += StartJump;
            Master.EndJumpEvent += EndJump;

            Master.StartCastSpellEvent += StartCastSpell;
            Master.EndCastSpellEvent += EndCastSpell;

            Master.StartAttackEvent += StartAttack;
            Master.EndAttackEvent += EndAttack;

            Master.ChangeHealthEvent += ChangeHealthEvent;

            Master.EnduranceChangedEvent += EnduranceChanged;

            Master.DieEvent += Die;

            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.StartRunEvent -= StartRun;
            Master.EndRunEvent -= EndRun;

            Master.StartJumpEvent -= StartJump;
            Master.EndJumpEvent -= EndJump;

            Master.StartCastSpellEvent -= StartCastSpell;
            Master.EndCastSpellEvent -= EndCastSpell;

            Master.StartAttackEvent -= StartAttack;
            Master.EndAttackEvent -= EndAttack;

            Master.ChangeHealthEvent -= ChangeHealthEvent;
            Master.EnduranceChangedEvent -= EnduranceChanged;

            Master.DieEvent -= Die;

            base.Unsubscribe();
        }

        private void EnduranceChanged(float value, float max) {
            Master.Animator.speed = Character_StatEndurance.EnduranceKoefficient(value, max);
        }

        private void StartJump() {
            Master.Animator.SetBool(AnimatorKeys.IsJumping, true);
        }

        private void EndJump() {
            Master.Animator.SetBool(AnimatorKeys.IsJumping, false);
        }

        private void StartRun(float axis) {
            Master.Animator.SetBool(AnimatorKeys.IsRunning, axis != 0);
        }

        private void EndRun() {
            Master.Animator.SetBool(AnimatorKeys.IsRunning, false);
        }

        private void StartCastSpell() {
            Master.Animator.SetBool(AnimatorKeys.IsCastingSpell, true);
        }

        private void EndCastSpell() {
            Master.Animator.SetBool(AnimatorKeys.IsCastingSpell, false);
        }

        private void StartAttack(Character_Master target) {
            Master.Animator.SetBool(AnimatorKeys.IsAttacking, true);
        }

        private void EndAttack(Character_Master target) {
            Master.Animator.SetBool(AnimatorKeys.IsAttacking, false);
        }

        private void Die() {
            Master.Animator.SetTrigger(AnimatorKeys.IsDead);
        }

        private void ChangeHealthEvent(float amount) {
            if (amount < 0)
                Debug.Log("Play hurt animation");
        }
    }
}