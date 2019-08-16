using BridgeMaster.Base;
using BridgeMaster.Dicts;

namespace BridgeMaster.Characters {
    class CharacterMaster_S_Animator : COM_Subscriber<CharacterMaster> {

        #region EVENTS

        protected override void Subscribe() {
            if (!_isSubscribed) {
                Master.StartRunEvent += StartRun;
                Master.EndRunEvent += EndRun;

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
                Master.StartRunEvent -= StartRun;
                Master.EndRunEvent -= EndRun;

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

        private void StartAttack() {
            Master.Animator.SetBool(AnimatorKeys.IsAttacking, true);
        }

        private void EndAttack() {
            Master.Animator.SetBool(AnimatorKeys.IsAttacking, false);
        }

        #endregion
    }
}