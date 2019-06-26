using UnityEngine;

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


        private void StartAttack(Transform target) {

        }

        private void EndAttack(Transform target) {

        }
    }
}