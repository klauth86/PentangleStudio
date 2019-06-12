using UnityEngine;

namespace BridgeMaster.Characters {
    [RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(Character_Master))]
    public abstract class Character_Base : MonoBehaviour {
        private Rigidbody2D _rigidbody;
        protected Rigidbody2D Rigidbody {
            get {
                return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody2D>());
            }
        }

        private Animator _animator;
        protected Animator Animator {
            get {
                return _animator ?? (_animator = GetComponent<Animator>());
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
