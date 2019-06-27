using BridgeMaster.Dicts;
using System.Collections;
using UnityEngine;

namespace BridgeMaster.Game {

    public class Game_LocationExit : GameObjectSubscriber<Master> {

        [SerializeField] private Location _nextLocation;
        [SerializeField] private GameObject _exitVfx;
        [SerializeField] private float _exitVfxDuration;

        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private float _checkRate;

        private Transform _transform;

        private void Start() {
            _transform = transform;
            StartCoroutine(CheckIfPlayerIsInLocationExit());
        }

        private IEnumerator CheckIfPlayerIsInLocationExit() {
            while (isActiveAndEnabled) {
                var hit = Physics2D.CapsuleCast(new Vector2(_transform.position.x, _transform.position.y), new Vector2(2, 7), CapsuleDirection2D.Vertical, 0, Vector2.zero, 0, _playerLayer);
                if (hit) {
                    if (_exitVfx) {
                        var vfx = Instantiate(_exitVfx);
                        yield return new WaitForSeconds(_exitVfxDuration);
                        Destroy(vfx);
                    }
                    foreach (var character in FindObjectsOfType<Characters.Character_Freeze>()) {
                        character.ToggleFreeze();
                    }
                    Target.ExitLocation(_nextLocation);
                    Destroy(gameObject);
                }
                else {
                    yield return new WaitForSeconds(_checkRate);
                }
            }
        }
    }
}
