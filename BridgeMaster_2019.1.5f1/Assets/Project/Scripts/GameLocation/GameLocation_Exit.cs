using System.Collections;
using UnityEngine;

namespace BridgeMaster.GameLocation {

    public class GameLocation_Exit : MonoBehaviour {
        [SerializeField] private Dicts.Location _nextLocation;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate;

        private void OnEnable() {
            StartCoroutine(CheckIfPlayerIsInLocationExit());
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator CheckIfPlayerIsInLocationExit() {
            yield return new WaitForSeconds(Random.Range(0, _checkRate));

            var myTransform = transform;
            var hasPrevHit = false;

            while (isActiveAndEnabled) {

                var hit = Physics2D.CapsuleCast(new Vector2(myTransform.position.x, myTransform.position.y),
                    new Vector2(2, 7), CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, _playerLayer);

                if (hit && !hasPrevHit) {
                    hasPrevHit = false;
                    Master.Session.ExitLocation(_nextLocation);                    
                }

                hasPrevHit = true;
                yield return new WaitForSeconds(_checkRate);
            }
        }
    }
}
