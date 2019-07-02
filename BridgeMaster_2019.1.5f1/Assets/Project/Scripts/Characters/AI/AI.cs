using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI : Character_Master {

        public event GameEventHandler<Transform> SetTargetEvent;
        public event GameEventHandler<Transform> ReachTargetEvent;

        public void SetTarget(Transform target) {
            SetTargetEvent?.Invoke(target);
        }

        public void ReachTarget(Transform target) {
            ReachTargetEvent?.Invoke(target);
        }

        private void Start() {
            SetTarget(null);
        }
    }
}