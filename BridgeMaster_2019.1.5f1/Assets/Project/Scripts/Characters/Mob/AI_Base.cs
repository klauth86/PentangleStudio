using UnityEngine;

namespace BridgeMaster.Characters.Mob {
    [RequireComponent(typeof(Master), typeof(AI_Master))]
    public abstract class AI_Base : MonoBehaviour {
        private AI_Master _ai;
        protected AI_Master AI {
            get {
                return _ai ?? (_ai = GetComponent<AI_Master>());
            }
        }

        private Master _master;
        protected Master Master {
            get {
                return _master ?? (_master = GetComponent<Master>());
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
