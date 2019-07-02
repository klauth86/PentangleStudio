using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI : Character_Master {

        public Transform Target;
        
        public event GameEventHandler<Transform> SetTargetEvent;
        public event GameEventHandler ReachTargetEvent;

        public void SetTarget(Transform target) {
            SetTargetEvent?.Invoke(target);
        }

        public void ReachTarget() {
            ReachTargetEvent?.Invoke();
        }

        private void Start() {
            SetTarget(null);
        }
    }
}