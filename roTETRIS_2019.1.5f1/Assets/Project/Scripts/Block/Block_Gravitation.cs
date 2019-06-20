using roTETRIS.Planet;
using System.Collections;
using UnityEngine;

namespace roTETRIS.Block {
    public class Block_Gravitation : SubBase<Block_Master> {
        [SerializeField] private float _gravitationRate;
        [SerializeField] private float _gravitationSpeed;

        private Planet_Master _planet;
        public Planet_Master Planet {
            get {
                return _planet ?? (_planet = FindObjectOfType<Planet_Master>());
            }
        }

        private Transform _myTransform;
        public Transform MyTransform {
            get {
                return _myTransform ?? (_myTransform = transform);
            }
        }

        private void OnEnable() {
            Master.FindGroundEvent += OnFindGround;
            Master.LostGroundEvent += OnLostGround;
        }

        private void OnDisable() {
            Master.FindGroundEvent -= OnFindGround;
            Master.LostGroundEvent -= OnLostGround;
        }

        private void OnFindGround() {
            StopAllCoroutines();
            MyTransform.SetParent(Planet.MyTransform);
        }

        private void OnLostGround() {
            StartCoroutine(GravitationRoutine());
        }

        private IEnumerator GravitationRoutine() {
            int r = 0, alpha = 0;
            do {
                MyTransform.localScale -= new Vector3(_gravitationSpeed, _gravitationSpeed, _gravitationSpeed);
                r = Mathf.CeilToInt(MyTransform.localScale.x * Constant.UnitCount);
                alpha = Mathf.CeilToInt(MyTransform.rotation.eulerAngles.z/Constant.UnitAngle);                
                yield return new WaitForSeconds(_gravitationRate);
            } while (!Master.CheckForCollision(r, alpha, MyTransform));
        }
    }
}