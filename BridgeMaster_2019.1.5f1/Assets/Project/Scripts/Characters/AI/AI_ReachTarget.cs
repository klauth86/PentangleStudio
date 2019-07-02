using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_ReachTarget : Base<AI> {

        protected override void Subscribe() {
            Master.ReachTargetEvent += ReachTarget;
            base.Subscribe();
        }

        protected override void Unsubscribe() {
            Master.ReachTargetEvent -= ReachTarget;
            base.Unsubscribe();
        }

        private void ReachTarget(Transform transform) {
            Master.EndRun();
        }
    }
}