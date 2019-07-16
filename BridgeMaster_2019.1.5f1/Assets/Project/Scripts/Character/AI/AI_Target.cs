using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Target : COM_Subscriber<AI> {

        protected override void Subscribe() {
            Master.SetTargetEvent += SetTarget;
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.SetTargetEvent -= SetTarget;
            base.Unsubscribe();
        }

        private void SetTarget(Transform transform) {
            Master.Target = transform;
        }
    }
}