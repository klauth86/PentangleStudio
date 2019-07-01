namespace BridgeMaster.Characters {
    public abstract class Base<T> : ComponentSubscriber<T> where T : Character_Master {
        private void Awake() {
            Master.DieEvent += DisableIfDie;
        }

        void DisableIfDie() {
            Master.DieEvent -= DisableIfDie;
            enabled = false;
        }
    }
}
