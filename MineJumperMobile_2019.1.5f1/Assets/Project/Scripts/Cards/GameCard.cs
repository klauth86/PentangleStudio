using System.Collections;
using UnityEngine;

namespace MineJumperMobile_2019.Cards {
    [RequireComponent(typeof(Rigidbody))]
    public class GameCard : RotatingCard {

        [SerializeField] private Material _unrevealedMaterial;
        [SerializeField] private Material _markedMaterial;
        [SerializeField] private Material[] _indexMaterials;

        [SerializeField] private float _coroutineTimeStep;
        [SerializeField] private float _collapseTime;

        private Rigidbody _rigidbody;
        private Rigidbody Rigidbody {
            get {
                return _rigidbody ?? (_rigidbody = GetComponent<Rigidbody>());
            }
        }

        public void Mark(bool isMarked) {
            MeshRenderer.material = isMarked ? _markedMaterial : _unrevealedMaterial;
        }

        public void Reveal(int bombIndex) {
            MeshRenderer.material = _indexMaterials[bombIndex];
            if (bombIndex == 0) {
                StartCoroutine(CollapseRoutine());
            }
        }

        private IEnumerator CollapseRoutine() {
            var n = _collapseTime / _coroutineTimeStep;
            for (int i = 1; i <= n; i++) {
                transform.localScale = (transform.localScale * (n - i)) / n;
                yield return new WaitForSeconds(_coroutineTimeStep);
            }
            Destroy(gameObject);
        }
    }
}