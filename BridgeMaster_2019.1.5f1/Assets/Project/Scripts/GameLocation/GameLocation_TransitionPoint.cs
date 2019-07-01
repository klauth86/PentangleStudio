using System.Collections;
using UnityEngine;

namespace BridgeMaster.GameLocation {

    public class GameLocation_TransitionPoint : MonoBehaviour {

        public Dicts.Location NextLocation;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate = 0.125f;

        private bool _hasPrevHit;

        private void OnEnable() {
            StartCoroutine(CheckIfPlayerIsInLocationExit());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator CheckIfPlayerIsInLocationExit() {
            yield return new WaitForSeconds(Random.Range(0, _checkRate));

            var myTransform = transform;

            while (isActiveAndEnabled) {

                var hit = Physics2D.CapsuleCast(new Vector2(myTransform.position.x, myTransform.position.y),
                    new Vector2(2, 7), CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, _playerLayer);

                if (hit && !_hasPrevHit) {
                    _hasPrevHit = true;
                    Master.LocationSession.ExitLocation(NextLocation);
                }
                else {
                    if (!hit && _hasPrevHit)
                        _hasPrevHit = false;

                    yield return new WaitForSeconds(_checkRate);
                }
            }
        }

        public void SetPreviousHit() {
            _hasPrevHit = true;
        }
    }
}