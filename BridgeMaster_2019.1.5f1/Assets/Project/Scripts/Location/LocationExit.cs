using BridgeMaster.Characters;
using BridgeMaster.Dicts;
using BridgeMaster.Game;
using System.Collections;
using UnityEngine;

namespace BridgeMaster {

    public class LocationExit : MonoBehaviour {
        [SerializeField] private Game_Master _master;
        [SerializeField] private Location _nextLocation;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private GameObject _locationExitVfx;
        [SerializeField] private float _checkInterval;

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
                    foreach (var character in FindObjectsOfType<Character_Master>()) {
                        character.Freeze();
                    }
                    _master.ExitLocation(_nextLocation);
                    Destroy(gameObject);
                }
                else {
                    yield return new WaitForSeconds(_checkInterval);
                }
            }
        }
    }

}
