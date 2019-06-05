namespace BridgeMaster.Masters {
    public class Master_Reference : Base {
        public static Player Player;

        private void Start() {
            Player = FindObjectOfType<Player>();
        }
    }
}
