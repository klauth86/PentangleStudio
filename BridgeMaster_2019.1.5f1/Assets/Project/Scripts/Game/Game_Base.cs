using UnityEngine;

namespace BridgeMaster.Game {
    [RequireComponent(typeof(Game_Master))]
    public abstract class Game_Base : MonoBehaviour {
        private Game_Master _master;
        protected Game_Master Master {
            get {
                return _master ?? (_master = GetComponent<Game_Master>());
            }
        }
    }
}
