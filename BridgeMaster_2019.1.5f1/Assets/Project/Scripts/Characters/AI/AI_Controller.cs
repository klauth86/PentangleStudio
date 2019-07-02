using UnityEngine;

namespace BridgeMaster.Characters.AI {
    public class AI_Controller : Base<AI> {
        [SerializeField] private float _attackHRange = 0.5f;
        [SerializeField] private float _attackVRange = 0.35f;

        [SerializeField] private float _reachHRange = 0.5f;
        [SerializeField] private float _reachVRange = 2;

        private void Update() {
            var player = Player.Player.PlayerInstance;

            if (Master.Target) {                
                if (Master.Target == player.MyTransform) {
                    if (TargetInRange(Master.Target, Master.MyTransform, _attackHRange, _attackVRange)) {
                        Master.StartRun(0);
                        Master.StartAttack(Player.Player.PlayerInstance);
                    }
                    else {
                        Master.EndAttack(Player.Player.PlayerInstance);
                        Master.StartRun(Mathf.Sign(Master.Target.position.x - Master.MyTransform.position.x));
                    }
                }
                else {
                    if (TargetInRange(Master.Target, Master.MyTransform, _reachHRange, _reachVRange)) {
                        Master.StartRun(0);
                        Master.ReachTarget();
                    }
                    else {
                        Master.StartRun(Mathf.Sign(Master.Target.position.x - Master.MyTransform.position.x));
                    }
                }
            }
        }

        private bool TargetInRange(Transform target, Transform myTransform, float hRange, float vRange) {
            var hDistance = target.position.x - myTransform.position.x;
            var vDistance = target.position.y - myTransform.position.y;

            return
                Mathf.Abs(hDistance) < hRange &&
                Mathf.Abs(vDistance) < vRange;
        }
    }
}