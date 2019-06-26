using Assets.Project.Scripts.Dicts;

namespace BridgeMaster.Characters.AI {
    public class AI : Master {

        public bool IsChasing;
        public bool IsAttacking;
        public AIState State;

        public event GameEventHandler StartWanderingEvent;
        public event GameEventHandler EndWanderingEvent;

        public event GameEventHandler StartChasingEvent;
        public event GameEventHandler EndChasingEvent;

        public void StartWandering() {
            StartWanderingEvent?.Invoke();
        }

        public void EndWandering() {
            EndWanderingEvent?.Invoke();
        }

        public void StartChasing() {
            StartChasingEvent?.Invoke();
        }

        public void EndChasing() {
            EndChasingEvent?.Invoke();
        }
    }
}