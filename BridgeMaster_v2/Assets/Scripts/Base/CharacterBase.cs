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
    }
}