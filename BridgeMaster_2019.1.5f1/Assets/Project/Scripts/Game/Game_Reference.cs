using BridgeMaster.Characters.Player;

namespace BridgeMaster.Game {
    public class Game_Reference : Game_Base {
        public static Player PlayerRef;

        private void Awake() {
            PlayerRef = FindObjectOfType<Player>();
        }
    }
}