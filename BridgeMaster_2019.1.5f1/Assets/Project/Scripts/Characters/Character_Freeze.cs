using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Freeze : Base<Master> {
        private void OnEnable() {
            Master.FreezeEvent += Freeze;
            Master.UnfreezeEvent += Unfreeze;
        }

        private void OnDisable() {
            Master.FreezeEvent -= Freeze;
            Master.UnfreezeEvent -= Unfreeze;
        }

        private void Unfreeze() {
            Master.Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            Master.Animator.speed = 1;
        }

        private void Freeze() {
            Master.Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Master.Animator.speed = 0;
        }
    }
}