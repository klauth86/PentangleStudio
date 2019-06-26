using BridgeMaster.Dicts;

namespace BridgeMaster.Characters {
    class Character_Attack : Base<Master> {
        private void OnEnable() {
            Master.StartAttackEvent += StartAttack;
            Master.EndAttackEvent += EndAttack;
        }

        private void OnDisable() {
            Master.StartAttackEvent -= StartAttack;
            Master.EndAttackEvent -= EndAttack;
        }


        private void StartAttack() {
            Master.Animator.SetBool(AnimatorKey.IsAttacking, true);
        }

        private void EndAttack() {
            Master.Animator.SetBool(AnimatorKey.IsAttacking, false);
        }
    }
}