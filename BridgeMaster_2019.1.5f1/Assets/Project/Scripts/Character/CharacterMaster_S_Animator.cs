using BridgeMaster.Dicts;

namespace BridgeMaster.Characters {
    class CharacterMaster_S_Animator : CharacterMaster_Subscriber<CharacterMaster> {

        #region EVENTS

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.SetRunEvent += SetRun;

                Master.StartJumpEvent += StartJump;
                Master.EndJumpEvent += EndJump;

                Master.StartCastSpellEvent += StartCastSpell;
                Master.EndCastSpellEvent += EndCastSpell;

                Master.StartAttackEvent += StartAttack;
                Master.EndAttackEvent += EndAttack;

                base.Subscribe();
            }
        }

        protected override void Unsubscribe() {
            if (_isSubscribed) {
                Master.SetRunEvent -= SetRun;

                Master.StartJumpEvent -= StartJump;
                Master.EndJumpEvent -= EndJump;

                Master.StartCastSpellEvent -= StartCastSpell;
                Master.EndCastSpellEvent -= EndCastSpell;

                Master.StartAttackEvent -= StartAttack;
                Master.EndAttackEvent -= EndAttack;

                base.Unsubscribe();
            }
        }

        private void StartJump() {
            Master.Animator.SetBool(AnimatorKeys.IsJumping, true);
        }

        private void EndJump() {
            Master.Animator.SetBool(AnimatorKeys.IsJumping, false);
        }

        private void SetRun(float axis) {
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

        private void StartAttack() {
            Master.Animator.SetBool(AnimatorKeys.IsAttacking, true);
        }

        private void EndAttack() {
            Master.Animator.SetBool(AnimatorKeys.IsAttacking, false);
        }

        protected override void IsDead() {
            Master.Animator.SetTrigger(AnimatorKeys.IsDying);
            base.IsDead();
        }

        #endregion
    }
}