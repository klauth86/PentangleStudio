using BridgeMaster.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster {

    public class LocationExit : MonoBehaviour {
        [SerializeField] private Master _master;
        [SerializeField] private Location _nextLocation;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private GameObject _locationExitVfx;

        private Transform _transform;

        private void Start() {
            _transform = transform;
            StartCoroutine(CheckIfPlayerIsInLocationExit());
        }

        private IEnumerator CheckIfPlayerIsInLocationExit() {
            while (isActiveAndEnabled) {
                var hit = Physics2D.CapsuleCast(new Vector2(_transform.position.x, _transform.position.y), new Vector2(2, 7), CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, _playerLayer);
                if (hit) {
                    if (_locationExitVfx) {
                        Instantiate(_locationExitVfx);
                    }
                    _master.ExitLocation(_nextLocation);
                    Destroy(gameObject);
                }
                else {
                    yield return new WaitForSeconds(0.0625f);
                }
            }
        }
    }

}
