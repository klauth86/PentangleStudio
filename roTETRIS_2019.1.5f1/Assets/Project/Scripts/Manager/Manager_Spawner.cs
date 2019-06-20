using System.Collections;
using UnityEngine;

namespace roTETRIS.Manager {
    class Manager_Spawner : SubBase<Manager_Master> {

        [SerializeField] private float _spawnRate;
        [SerializeField] private GameObject[] _blocks;

        private Random _random;
        protected Random Random {
            get {
                return _random ?? (_random = new Random());
            }
        }

        private void OnEnable() {
            StartCoroutine(SpawnRoutine());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator SpawnRoutine() {
            while(true) {
                var block = Instantiate(GetRandomBlock());
                block.transform.Rotate(Vector3.back, Random.Range(0, 2) * Constant.UnitAngle);
                yield return new WaitForSeconds(_spawnRate);
            }
        }

        private GameObject GetRandomBlock() {
            return _blocks[Random.Range(0, _blocks.Length)];
        }
    }
}
