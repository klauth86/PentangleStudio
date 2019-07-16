using BridgeMaster.Base;
using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI : Character_Master {

        public Transform Target;
        
        public event EventHandler<Transform> SetTargetEvent;
        public event EventHandler ReachTargetEvent;

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