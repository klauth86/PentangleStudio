using UnityEngine;

namespace Base {
    public abstract class CharacterBase : MonoBehaviour {
        private void Start() {
            Init();
        }

        protected abstract void Init();
    }
}