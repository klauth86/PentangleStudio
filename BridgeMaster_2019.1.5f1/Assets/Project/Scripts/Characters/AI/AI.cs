using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI : Master {

        public bool IsChasing;
        public bool IsWandering;

        public event GameEventHandler StartWanderingEvent;
        public event GameEventHandler EndWanderingEvent;

        public event GameEventHandler<Transform> StartChasingTargetEvent;
        public event GameEventHandler EndChasingTargetEvent;

        public event GameEventHandler EnterAttackRangeEvent;
        public event GameEventHandler LeftAttackRangeEvent;

        public void StartWandering() {
            StartWanderingEvent?.Invoke();
        }

        public void StopWandering() {
            EndWanderingEvent?.Invoke();
        }

        public void FindTarget(Player.Player player) {
            StartChasingTargetEvent?.Invoke(player);
        }

        public void LostTarget() {
            EndChasingTargetEvent?.Invoke();
        }

        public void EnterAttackRange() {
            EnterAttackRangeEvent?.Invoke();
        }

        public void LeftAttackRange() {
            LeftAttackRangeEvent?.Invoke();
        }
    }
}