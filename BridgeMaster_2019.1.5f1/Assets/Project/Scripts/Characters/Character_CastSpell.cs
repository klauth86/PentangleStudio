using BridgeMaster.Dicts;

namespace BridgeMaster.Characters {
    class Character_CastSpell : Base<Master> {
        private void OnEnable() {
            Master.StartCastSpellEvent += StartCastSpell;
            Master.EndCastSpellEvent += EndCastSpell;
        }

        private void OnDisable() {
            Master.StartCastSpellEvent -= StartCastSpell;
            Master.EndCastSpellEvent -= EndCastSpell;
        }

        private void StartCastSpell() {
            Master.Animator.SetBool(AnimatorKey.IsCastingSpell, true);
        }

        private void EndCastSpell() {
            Master.Animator.SetBool(AnimatorKey.IsCastingSpell, false);
        }
    }
}