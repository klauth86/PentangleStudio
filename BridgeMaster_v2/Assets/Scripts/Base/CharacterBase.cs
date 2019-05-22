using System;
using UnityEngine;

namespace Base {
    public abstract class CharacterBase : MonoBehaviour {
        protected event Action OnStart = delegate { };
        protected event Action OnUpdate = delegate { };

        private void Start() {
            OnStart();
        }

        private void Update() {
            OnUpdate();
        }

        protected void Swap() {
            transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        }

        protected event Action OnPause = delegate { };
        protected event Action OnUnpause = delegate { };

        public void PauseCharacter() {

        }

        public void UnpauseCharacter() {

        }
    }
}