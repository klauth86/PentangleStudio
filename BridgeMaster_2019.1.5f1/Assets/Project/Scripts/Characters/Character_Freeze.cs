using UnityEngine;

namespace BridgeMaster.Characters {
    class Character_Freeze : Character_Base {
        private void OnEnable() {
            Master.FreezeEvent += Freeze;
            Master.UnfreezeEvent += Unfreeze;
        }

        private void OnDisable() {
            Master.FreezeEvent -= Freeze;
            Master.UnfreezeEvent -= Unfreeze;
        }

        private void Unfreeze() {
            Rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            Animator.speed = 1;
        }

        private void Freeze() {
            Rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            Animator.speed = 0;
        }
    }
}