namespace BridgeMaster.Characters {
    public abstract class Character_Base<T> : ComponentSubscriber<T> where T : Master {
        private void Awake() {
            Master.DieEvent += DisableIfDie;
        }

        void DisableIfDie() {
            Master.DieEvent -= DisableIfDie;
            enabled = false;
        }
    }
}
