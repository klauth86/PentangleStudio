using UnityEngine;

namespace BridgeMaster.Characters.Mob {
    [RequireComponent(typeof(Master))]
    public class AI_Master:MonoBehaviour {
        public event GameEventHandler<Player.Player> FindTargetEvent;
        public event GameEventHandler LostTargetEvent;

        public event GameEventHandler EnterAttackRangeEvent;
        public event GameEventHandler LeftAttackRangeEvent;

        public void FindTarget(Player.Player player) {
            FindTargetEvent?.Invoke(player);
        }

        public void LostTarget() {
            LostTargetEvent?.Invoke();
        }

        public void EnterAttackRange() {
            EnterAttackRangeEvent?.Invoke();
        }

        public void LeftAttackRange() {
            LeftAttackRangeEvent?.Invoke();
        }
    }
}
