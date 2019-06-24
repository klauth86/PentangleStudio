namespace BridgeMaster.Characters.AI {
    public class AI : Master {

        public bool IsChasing;
        public bool IsWandering;

        public event GameEventHandler StartWanderingEvent;
        public event GameEventHandler EndWanderingEvent;

        public event GameEventHandler StartChasingEvent;
        public event GameEventHandler EndChasingEvent;

        public event GameEventHandler EnterAttackRangeEvent;
        public event GameEventHandler LeftAttackRangeEvent;

        public void StartWandering() {
            IsWandering = true;
            StartWanderingEvent?.Invoke();
        }

        public void StopWandering() {
            EndWanderingEvent?.Invoke();
        }

        public void StartChasing() {
            StartChasingEvent?.Invoke();
        }

        public void EndChasing() {
            EndChasingEvent?.Invoke();
        }

        public void EnterAttackRange() {
            EnterAttackRangeEvent?.Invoke();
        }

        public void LeftAttackRange() {
            LeftAttackRangeEvent?.Invoke();
        }

        private void Start() {
            if (IsWandering)
                StartWandering();

            if (IsChasing)
                StartChasing();
        }
    }
}