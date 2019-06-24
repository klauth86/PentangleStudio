using UnityEngine;

namespace BridgeMaster.Characters.Mob {
    [RequireComponent(typeof(Character_Master), typeof(AI_Master))]
    public abstract class AI_Base : MonoBehaviour {
        private AI_Master _ai;
        protected AI_Master AI {
            get {
                return _ai ?? (_ai = GetComponent<AI_Master>());
            }
        }

        private Character_Master _master;
        protected Character_Master Master {
            get {
                return _master ?? (_master = GetComponent<Character_Master>());
            }
        }

        private void Awake() {
            Master.DieEvent += DisableIfDie;
        }

        void DisableIfDie() {
            enabled = false;
            Master.DieEvent -= DisableIfDie;
        }
    }
}
