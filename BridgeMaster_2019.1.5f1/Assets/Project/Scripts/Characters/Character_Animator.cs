using BridgeMaster.Characters.Stat;
using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Animator : Base<Character_Master> {

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
            Master.ToggleFreezeEvent += ToggleFreeze;

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
            Master.ToggleFreezeEvent -= ToggleFreeze;

            Master.DieEvent -= Die;

            base.Unsubscribe();
        }

        private void ToggleFreeze() {
            if (Master.IsFreezed) {
                _prevSpeed = Master.Animator.speed;
            }
            else {
                Master.Animator.speed = _prevSpeed;
                _prevSpeed = -1;
            }
        }

        private void EnduranceChanged(float value, float max) {
            Master.Animator.speed = Character_StatEndurance.EnduranceKoefficient(value, max);
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

        private void StartAttack(Character_Master target) {
            Master.Animator.SetBool(AnimatorKey.IsAttacking, true);
        }

        private void EndAttack(Character_Master target) {
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