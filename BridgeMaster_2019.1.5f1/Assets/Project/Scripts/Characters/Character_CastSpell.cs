using BridgeMaster.Dicts;
using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_CastSpell : Character_Base {
        private void OnEnable() {
            Master.StartCastSpellEvent += StartCastSpell;
            Master.EndCastSpellEvent += EndCastSpell;
        }

        private void OnDisable() {
            Master.StartCastSpellEvent -= StartCastSpell;
            Master.EndCastSpellEvent -= EndCastSpell;
        }

        private void StartCastSpell() {
            Animator.SetBool(AnimatorKey.IsCastingSpell, true);
        }

        private void EndCastSpell() {
            Animator.SetBool(AnimatorKey.IsCastingSpell, false);
        }
    }
}